using UnityEngine;

public class EnhancedPulseSimulator : QuantumSimulator
{
    private QuantumState state;

    public EnhancedPulseSimulator(QuantumCircuit circuit, QuantumVisualizer visualizer)
        : base(circuit, visualizer)
    {
        if (circuit == null || visualizer == null)
        {
            Debug.LogError("EnhancedPulseSimulator: Null arguments passed.");
            throw new System.ArgumentNullException("Null argument in EnhancedPulseSimulator");
        }

        state = new QuantumState(circuit.NumQubits);
        Debug.Log($"EnhancedPulseSimulator initialized with {circuit.NumQubits} qubits.");
    }

    public void ApplyPulse(double amplitude, double frequency, double duration)
    {
        Debug.Log($"Applying Pulse: Amplitude={amplitude}, Frequency={frequency}, Duration={duration}");
        visualizer.VisualizePulse(amplitude, frequency, duration);

        // Modify the quantum state with a rotation and phase shift
        double rotationAngle = amplitude * duration;
        double phaseShift = frequency * duration;

        ApplyRotationAndPhase(rotationAngle, phaseShift);
    }

    private void ApplyRotationAndPhase(double rotationAngle, double phaseShift)
    {
        for (int i = 0; i < state.StateVector.Length; i++)
        {
            Complex currentState = state.StateVector[i];

            // Apply rotation: update amplitude
            double newReal = currentState.Real * Mathf.Cos((float)rotationAngle) -
                             currentState.Imaginary * Mathf.Sin((float)rotationAngle);
            double newImaginary = currentState.Real * Mathf.Sin((float)rotationAngle) +
                                  currentState.Imaginary * Mathf.Cos((float)rotationAngle);

            // Apply phase shift
            double phaseFactor = Mathf.Exp((float)(phaseShift * Mathf.PI * 2));
            Complex newState = new Complex(newReal * phaseFactor, newImaginary * phaseFactor);

            // Update state
            state.StateVector[i] = newState;
        }
    }
}
