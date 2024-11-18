using System;
using UnityEngine;
using System.Numerics;

public class GroversAlgorithmWithOracle : QuantumAlgorithm
{
    private Func<int, bool> oracle;

    public GroversAlgorithmWithOracle(Func<int, bool> oracle) : base("Grover's Algorithm with Oracle")
    {
        this.oracle = oracle;
    }

    public override void Execute(QuantumCircuit circuit, QuantumSimulator simulator)
    {
        Debug.Log("Executing Grover's Algorithm with Custom Oracle...");

        // Apply Hadamard to all qubits
        for (int i = 0; i < circuit.NumQubits; i++)
        {
            circuit.AddGate(QuantumGate.Hadamard(i));
        }

        // Apply user-defined oracle
        circuit.AddGate(new QuantumGate("Oracle", new[] { 0, 1 }, state =>
        {
            for (int i = 0; i < state.Length; i++)
            {
                if (oracle(i))
                {
                    state[i] = new Complex(
                    state[i].Real * -1,
                    state[i].Imaginary * -1
                    ); // Apply multiplication on both parts
 // Marking solution
                }
            }
            return state;
        }));

        // Apply Diffusion Operator
        circuit.AddGate(new QuantumGate("Diffusion", new[] { 0, 1 }, state => ApplyDiffusion(state)));

        simulator.Simulate();
    }

    private Complex[] ApplyDiffusion(Complex[] state)
    {
        // Placeholder for diffusion operator logic
        return state;
    }
}
