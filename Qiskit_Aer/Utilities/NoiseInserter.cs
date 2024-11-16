using System;
using System.Collections.Generic;
using System.Linq;

public class NoiseInserter
{
    private NoiseModel noiseModel;
    private bool transpile;

    public NoiseInserter(NoiseModel model, bool transpileCircuits = false)
    {
        noiseModel = model;
        transpile = transpileCircuits;
    }

    public List<QuantumCircuit> InsertNoise(List<QuantumCircuit> circuits)
    {
        List<QuantumCircuit> resultCircuits = new List<QuantumCircuit>();
        var localErrors = noiseModel.LocalQuantumErrors;
        var defaultErrors = noiseModel.DefaultQuantumErrors;

        foreach (var circuit in circuits)
        {
            QuantumCircuit transpiledCircuit = transpile
                ? TranspileCircuit(circuit, noiseModel.BasisGates)
                : circuit;

            Dictionary<Qubit, int> qubitIndices = transpiledCircuit.Qubits
                .Select((q, index) => new { Qubit = q, Index = index })
                .ToDictionary(item => item.Qubit, item => item.Index);

            QuantumCircuit resultCircuit = transpiledCircuit.Clone();
            resultCircuit.Data = new List<Instruction>();

            foreach (var inst in transpiledCircuit.Data)
            {
                resultCircuit.Data.Add(inst);

                var qubits = inst.Qubits.Select(q => qubitIndices[q]).ToArray();
                var name = inst.Operation.Name;

                if (localErrors.ContainsKey(name) && localErrors[name].ContainsKey(qubits))
                {
                    var error = localErrors[name][qubits];
                    resultCircuit.AddInstruction(error.ToInstruction(), inst.Qubits);
                }
                else if (defaultErrors.ContainsKey(name))
                {
                    var error = defaultErrors[name];
                    resultCircuit.AddInstruction(error.ToInstruction(), inst.Qubits);
                }
            }

            resultCircuits.Add(resultCircuit);
        }

        return resultCircuits;
    }

    private QuantumCircuit TranspileCircuit(QuantumCircuit circuit, List<string> basisGates)
    {
        // This is a stub for circuit transpilation
        // In real implementation, this would transpile the circuit into the specified basis gates
        QuantumCircuit transpiledCircuit = circuit.Clone();
        transpiledCircuit.BasisGates = basisGates;
        return transpiledCircuit;
    }
}
