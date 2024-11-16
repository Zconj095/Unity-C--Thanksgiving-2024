using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AerCompiler : MonoBehaviour
{
    private int _lastFlowId;

    public AerCompiler()
    {
        _lastFlowId = -1;
    }

    public List<QuantumCircuit> Compile(List<QuantumCircuit> circuits, List<HashSet<string>> optypes = null)
    {
        var compiledCircuits = new List<QuantumCircuit>();
        var compiledOptypes = optypes != null ? new List<HashSet<string>>(optypes) : null;

        foreach (var circuit in circuits)
        {
            var optype = optypes?[circuits.IndexOf(circuit)];
            var inlineCircuit = InlineInitialize(circuit, optype);

            if (IsDynamic(inlineCircuit, optype))
            {
                var compiledCircuit = DecomposeControlFlow(inlineCircuit);
                compiledCircuits.Add(compiledCircuit);

                if (compiledOptypes != null)
                    compiledOptypes[circuits.IndexOf(circuit)] = ComputeOptypes(compiledCircuit);
            }
            else
            {
                compiledCircuits.Add(inlineCircuit);
            }
        }

        return compiledCircuits;
    }

    private QuantumCircuit InlineInitialize(QuantumCircuit circuit, HashSet<string> optype)
    {
        if (optype == null || !optype.Contains("Initialize"))
            return circuit;

        var newCircuit = circuit.Copy();
        foreach (var gate in newCircuit.Gates)
        {
            if (gate.Name == "Initialize")
            {
                var decomposedGates = DecomposeGate(gate);
                newCircuit.ReplaceGate(gate, decomposedGates);
            }
        }

        return newCircuit;
    }

    private bool IsDynamic(QuantumCircuit circuit, HashSet<string> optype)
    {
        var controlFlowTypes = new HashSet<string> { "WhileLoop", "ForLoop", "IfElse" };
        return optype?.Any(controlFlowTypes.Contains) ?? circuit.Gates.Any(g => controlFlowTypes.Contains(g.Name));
    }

    private QuantumCircuit DecomposeControlFlow(QuantumCircuit circuit)
    {
        var decomposedCircuit = circuit.Copy();

        foreach (var gate in circuit.Gates)
        {
            if (gate.Name == "WhileLoop" || gate.Name == "ForLoop" || gate.Name == "IfElse")
            {
                var inlineCircuit = InlineControlFlow(decomposedCircuit, gate);
                decomposedCircuit.ReplaceGate(gate, inlineCircuit.Gates);
            }
        }

        return decomposedCircuit;
    }

    private List<QuantumGate> DecomposeGate(QuantumGate gate)
    {
        return new List<QuantumGate>
        {
            new QuantumGate("DecomposedGate1", gate.Qubits, gate.Clbits, gate.Parameters),
            new QuantumGate("DecomposedGate2", gate.Qubits, gate.Clbits, gate.Parameters)
        };
    }

    private QuantumCircuit InlineControlFlow(QuantumCircuit circuit, QuantumGate gate)
    {
        var newCircuit = circuit.Copy();

        // Example: Replace control-flow gate with equivalent lower-level gates
        if (gate.Name == "WhileLoop")
        {
            newCircuit.AddGate(new QuantumGate("Mark", gate.Qubits, gate.Clbits, null));
            newCircuit.AddGate(new QuantumGate("JumpIf", gate.Qubits, gate.Clbits, gate.Parameters));
        }

        return newCircuit;
    }

    private HashSet<string> ComputeOptypes(QuantumCircuit circuit)
    {
        return new HashSet<string>(circuit.Gates.Select(g => g.Name));
    }
}
