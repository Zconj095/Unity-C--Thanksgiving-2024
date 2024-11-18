using UnityEngine;

public class PulseSimulatorWithFidelity : EnhancedPulseSimulator
{
    public PulseSimulatorWithFidelity(QuantumCircuit circuit, QuantumVisualizer visualizer) 
        : base(circuit, visualizer) { }

    public void ApplyImperfectPulse(double amplitude, double frequency, double duration, double fidelity)
    {
        Debug.Log($"Applying Pulse with Fidelity {fidelity}...");

        if (UnityEngine.Random.value > fidelity)
        {
            Debug.Log("Pulse applied with imperfection.");
        }

        base.ApplyPulse(amplitude, frequency, duration);
    }
}
