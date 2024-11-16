using System;
using System.Collections.Generic;
using System.Linq;


// Define a module for pulse execution
public class SignalModule
{
    public List<PulseSequence> SequenceList;
    public Dictionary<string, object> Configuration;
    public Dictionary<string, object> HeaderInfo;

    public SignalModule(List<PulseSequence> sequences, Dictionary<string, object> config, Dictionary<string, object> header)
    {
        SequenceList = sequences;
        Configuration = config;
        HeaderInfo = header;
    }
}

// Main class to handle the disassembly of Qobj objects
public class ProgramProcessor
{
    public static object BreakDown(Qobj programObject)
    {
        if (programObject.Type == "PULSE")
        {
            return ProcessPulseProgram(programObject);
        }
        else
        {
            return ProcessCircuitProgram(programObject);
        }
    }

    private static ExecutionModule ProcessCircuitProgram(Qobj programObject)
    {
        var configData = programObject.Config.ToDictionary();

        if (configData.ContainsKey("qubit_lo_freq"))
        {
            configData["qubit_lo_freq"] = ((List<double>)configData["qubit_lo_freq"]).Select(freq => freq * 1e9).ToList();
        }

        if (configData.ContainsKey("meas_lo_freq"))
        {
            configData["meas_lo_freq"] = ((List<double>)configData["meas_lo_freq"]).Select(freq => freq * 1e9).ToList();
        }

        var headerData = programObject.Header.ToDictionary();
        var circuitCollection = ExtractCircuits(programObject);

        return new ExecutionModule(circuitCollection, configData, headerData);
    }

    private static List<QuantumCircuit> ExtractCircuits(Qobj programObject)
    {
        if (programObject.Experiments == null || programObject.Experiments.Count == 0)
        {
            return null;
        }

        var circuitCollection = new List<QuantumCircuit>();

        foreach (var experiment in programObject.Experiments)
        {
            var quantumRegs = experiment.Header.QregSizes.Select(q => new QuantumRegister(q.Item1, q.Item2)).ToList();
            var classicalRegs = experiment.Header.CregSizes.Select(c => new ClassicalRegister(c.Item1, c.Item2)).ToList();

            var quantumCircuit = new QuantumCircuit(quantumRegs, classicalRegs, experiment.Header.Name);

            var quantumRegMap = quantumRegs.ToDictionary(reg => reg.Name, reg => reg);
            var classicalRegMap = classicalRegs.ToDictionary(reg => reg.Name, reg => reg);

            foreach (var step in experiment.Instructions)
            {
                string instructionName = step.Name;
                var qubitTargets = new List<Qubit>();
                var clbitTargets = new List<Clbit>();
                var parameters = step.Params ?? new List<double>();

                if (step.Qubits != null)
                {
                    qubitTargets.AddRange(step.Qubits.Select(q => quantumRegMap[experiment.Header.QubitLabels[q][0]].Qubits[q]));
                }

                if (step.Memory != null)
                {
                    clbitTargets.AddRange(step.Memory.Select(c => classicalRegMap[experiment.Header.ClbitLabels[c][0]].Clbits[c]));
                }

                if (quantumCircuit.HasMethod(instructionName))
                {
                    var instructionMethod = quantumCircuit.GetMethod(instructionName);
                    instructionMethod.Invoke(quantumCircuit, new object[] { parameters, qubitTargets, clbitTargets });
                }
                else
                {
                    var customOperation = new Instruction(instructionName, qubitTargets.Count, clbitTargets.Count, parameters);
                    quantumCircuit.Append(customOperation, qubitTargets, clbitTargets);
                }
            }

            circuitCollection.Add(quantumCircuit);
        }

        return circuitCollection;
    }

    private static SignalModule ProcessPulseProgram(Qobj programObject)
    {
        var configData = programObject.Config.ToDictionary();
        configData.Remove("pulse_library");

        if (configData.ContainsKey("qubit_lo_freq"))
        {
            configData["qubit_lo_freq"] = ((List<double>)configData["qubit_lo_freq"]).Select(freq => freq * 1e9).ToList();
        }

        if (configData.ContainsKey("meas_lo_freq"))
        {
            configData["meas_lo_freq"] = ((List<double>)configData["meas_lo_freq"]).Select(freq => freq * 1e9).ToList();
        }

        var headerData = programObject.Header.ToDictionary();

        var signalLos = new List<Dictionary<PulseChannel, double>>();
        foreach (var program in programObject.Experiments)
        {
            var programLos = new Dictionary<PulseChannel, double>();

            if (program.Config?.QubitLoFreq != null)
            {
                for (int i = 0; i < program.Config.QubitLoFreq.Count; i++)
                {
                    programLos[new DriveChannel(i)] = program.Config.QubitLoFreq[i] * 1e9;
                }
            }

            if (program.Config?.MeasLoFreq != null)
            {
                for (int i = 0; i < program.Config.MeasLoFreq.Count; i++)
                {
                    programLos[new MeasureChannel(i)] = program.Config.MeasLoFreq[i] * 1e9;
                }
            }

            signalLos.Add(programLos);
        }

        if (signalLos.Any(los => los.Count > 0))
        {
            configData["schedule_los"] = signalLos;
        }

        var signalSequences = ExtractSignalSequences(programObject);
        return new SignalModule(signalSequences, configData, headerData);
    }

    private static List<PulseSequence> ExtractSignalSequences(Qobj programObject)
    {
        var instructionConverter = new InstructionConverter(programObject.Config.PulseLibrary);

        return programObject.Experiments.Select(program =>
        {
            var sequenceSteps = program.Instructions.Select(inst => instructionConverter.Convert(inst)).ToList();
            return new PulseSequence(sequenceSteps);
        }).ToList();
    }
}
