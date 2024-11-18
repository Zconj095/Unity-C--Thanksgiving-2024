using System;

public class QuantumQubit
{
    // The qubit's state: 0 or 1 (classical representation)
    public int State { get; set; }

    // Constructor to initialize a qubit with a given state
    public QuantumQubit(int initialState = 0)
    {
        if (initialState != 0 && initialState != 1)
        {
            throw new ArgumentException("QuantumQubit state must be 0 or 1.");
        }

        State = initialState;
    }

    // Method to apply a NOT gate (X gate) to the qubit, flipping its state
    public void ApplyXGate()
    {
        State = 1 - State; // Flip the state (0 becomes 1, 1 becomes 0)
    }

    // Method to apply a Hadamard gate (H gate) to the qubit, creating superposition
    public void ApplyHadamardGate()
    {
        // The Hadamard gate transforms the qubit to an equal superposition of 0 and 1
        // We'll simulate this by setting it to 50% probability of being measured as 0 or 1.
        // In a real quantum system, you would apply probability distributions and use quantum libraries
        Random rand = new Random();
        State = rand.Next(0, 2); // Randomly set it to 0 or 1
    }

    // Method to get the qubit's state (in classical terms: 0 or 1)
    public int Measure()
    {
        // In a real quantum system, measurement collapses the qubit's state
        // We'll just return the current state for simplicity
        return State;
    }

    // Reset the qubit to state 0
    public void Reset()
    {
        State = 0;
    }
}
