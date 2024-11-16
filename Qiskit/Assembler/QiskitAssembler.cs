using System;
using System.Collections.Generic;
using System.Linq;

public class QiskitAssembler
{
    public (QasmQobjExperiment, List<PulseLibraryEntry>?) AssembleCircuit(QuantumCircuit circuit, RunConfig runConfig)
    {
        if (circuit.Unit != "dt")
        {
            throw new Exception($"Unable to assemble circuit with unit '{circuit.Unit}', which must be 'dt'.");
        }

        // Header data
        int numQubits = 0;
        int memorySlots = 0;
        var qubitLabels = new List<Tuple<string, int>>();
        var clbitLabels = new List<Tuple<string, int>>();
        var qregSizes = new List<Tuple<string, int>>();
        var cregSizes = new List<Tuple<string, int>>();

        foreach (var qreg in circuit.Qregs)
        {
            qregSizes.Add(Tuple.Create(qreg.Name, qreg.Size));
            for (int j = 0; j < qreg.Size; j++)
            {
                qubitLabels.Add(Tuple.Create(qreg.Name, j));
            }
            numQubits += qreg.Size;
        }

        foreach (var creg in circuit.Cregs)
        {
            cregSizes.Add(Tuple.Create(creg.Name, creg.Size));
            for (int j = 0; j < creg.Size; j++)
            {
                clbitLabels.Add(Tuple.Create(creg.Name, j));
            }
            memorySlots += creg.Size;
        }

        var qubitIndices = circuit.Qubits.Select((qubit, idx) => new { qubit, idx }).ToDictionary(x => x.qubit, x => x.idx);
        var clbitIndices = circuit.Clbits.Select((clbit, idx) => new { clbit, idx }).ToDictionary(x => x.clbit, x => x.idx);

        // Metadata
        var metadata = circuit.Metadata ?? new Dictionary<string, object>();

        var header = new ExperimentHeader
        {
            QubitLabels = qubitLabels,
            NQubits = numQubits,
            QregSizes = qregSizes,
            ClbitLabels = clbitLabels,
            MemorySlots = memorySlots,
            CregSizes = cregSizes,
            Name = circuit.Name,
            GlobalPhase = circuit.GlobalPhase,
            Metadata = metadata
        };

        var config = new ExperimentConfig
        {
            NQubits = numQubits,
            MemorySlots = memorySlots
        };

        var (calibrations, pulseLibrary) = AssemblePulseGates(circuit, runConfig);
        if (calibrations != null)
        {
            config.Calibrations = calibrations;
        }

        // Convert conditionals
        bool isConditionalExperiment = circuit.Data.Any(instruction => instruction.Operation.Condition.HasValue);
        int maxConditionalIdx = 0;

        var instructions = new List<Operation>();
        foreach (var opContext in circuit.Data)
        {
            var instruction = opContext.Operation.Assemble();

            // Add register attributes to the instruction
            var qargs = opContext.Qubits;
            var cargs = opContext.Clbits;
            if (qargs != null)
            {
                instruction.Qubits = qargs.Select(qubit => qubitIndices[qubit]).ToList();
            }

            if (cargs != null)
            {
                instruction.Memory = cargs.Select(clbit => clbitIndices[clbit]).ToList();
                if (instruction.Name == "measure" && isConditionalExperiment)
                {
                    instruction.Register = cargs.Select(clbit => clbitIndices[clbit]).ToList();
                }
            }

            if (instruction.Operation.Condition.HasValue)
            {
                var (ctrlReg, ctrlVal) = instruction.Operation.Condition.Value;
                int mask = 0;
                int val = 0;

                if (ctrlReg is Clbit)
                {
                    mask = 1 << clbitIndices[(Clbit)ctrlReg];
                    val = (ctrlVal & 1) << clbitIndices[(Clbit)ctrlReg];
                }
                else
                {
                    foreach (var clbit in clbitIndices)
                    {
                        if (ctrlReg.Contains(clbit.Key))
                        {
                            mask |= 1 << clbitIndices[clbit.Key];
                            val |= ((ctrlVal >> ctrlReg.ToList().IndexOf(clbit.Key)) & 1) << clbitIndices[clbit.Key];
                        }
                    }
                }

                int conditionalRegIdx = memorySlots + maxConditionalIdx;

                var conversionBFunc = new Operation("bfunc")
                {
                    Mask = mask,
                    Relation = "==",
                    Val = val,
                    Register = conditionalRegIdx
                };

                instructions.Add(conversionBFunc);
                instruction.Conditional = conditionalRegIdx;
                maxConditionalIdx++;
                instruction.Operation.Condition = null;
            }

            instructions.Add(instruction);
        }

        var experiment = new QasmQobjExperiment
        {
            Header = header,
            Instructions = instructions,
            Config = config
        };

        return (experiment, pulseLibrary);
    }

    private (QasmExperimentCalibrations?, List<PulseLibraryEntry>?) AssemblePulseGates(QuantumCircuit circuit, RunConfig runConfig)
    {
        if (circuit.Calibrations == null || circuit.Calibrations.Count == 0)
        {
            return (null, null);
        }

        var calibrations = new List<GateCalibration>();
        var pulseLibrary = new Dictionary<string, List<Tuple<double, double>>>();

        foreach (var gate in circuit.Calibrations)
        {
            foreach (var (qubits, paramsSchedule) in gate.Value)
            {
                var schedule = AssembleSchedule(paramsSchedule, runConfig, pulseLibrary);
                calibrations.Add(new GateCalibration(gate.Key, qubits.ToList(), paramsSchedule, schedule));
            }
        }

        return (new QasmExperimentCalibrations(calibrations), pulseLibrary);
    }

    private List<string> AssembleSchedule(List<double> paramsSchedule, RunConfig runConfig, Dictionary<string, List<Tuple<double, double>>> pulseLibrary)
    {
        // Placeholder implementation, should match the exact logic needed for scheduling.
        return new List<string>();
    }
}
