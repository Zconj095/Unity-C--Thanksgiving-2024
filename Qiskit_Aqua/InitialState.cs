using System;

public class InitialState
{
    private readonly int _numQubits;
    private readonly string _state;

    /// <summary>
    /// Constructor for InitialState.
    /// </summary>
    /// <param name="numQubits">Number of qubits, minimum 1.</param>
    /// <param name="state">Predefined state ("zero", "uniform", or "random").</param>
    public InitialState(int numQubits, string state = "zero")
    {
        if (numQubits < 1)
        {
            throw new ArgumentException("Number of qubits must be at least 1.");
        }

        if (state != "zero" && state != "uniform" && state != "random")
        {
            throw new ArgumentException($"Invalid state: {state}. Use 'zero', 'uniform', or 'random'.");
        }

        _numQubits = numQubits;
        _state = state;

        Console.WriteLine(
            $"Warning: The InitialState class is deprecated. Use plain QuantumCircuit for initialization."
        );
    }

    /// <summary>
    /// Constructs the quantum state as a vector or circuit.
    /// </summary>
    /// <param name="mode">"vector" to produce a state vector, or "circuit" to construct a circuit.</param>
    /// <returns>An object representing either the state vector or quantum circuit.</returns>
    public object ConstructState(string mode = "vector")
    {
        if (mode == "vector")
        {
            return GenerateStateVector();
        }
        else if (mode == "circuit")
        {
            return GenerateCircuit();
        }
        else
        {
            throw new ArgumentException("Mode must be either 'vector' or 'circuit'.");
        }
    }

    private double[] GenerateStateVector()
    {
        int size = (int)Math.Pow(2, _numQubits);
        double[] stateVector = new double[size];

        switch (_state)
        {
            case "zero":
                stateVector[0] = 1.0;
                break;

            case "uniform":
                double value = 1.0 / Math.Sqrt(size);
                for (int i = 0; i < size; i++)
                {
                    stateVector[i] = value;
                }
                break;

            case "random":
                Random random = new Random();
                double sum = 0.0;
                for (int i = 0; i < size; i++)
                {
                    stateVector[i] = random.NextDouble();
                    sum += stateVector[i] * stateVector[i];
                }

                double normalizationFactor = Math.Sqrt(sum);
                for (int i = 0; i < size; i++)
                {
                    stateVector[i] /= normalizationFactor;
                }
                break;

            default:
                throw new InvalidOperationException("Unexpected state.");
        }

        return stateVector;
    }

    private string GenerateCircuit()
    {
        return _state switch
        {
            "zero" => $"Circuit with {_numQubits} qubits, all initialized to |0⟩.",
            "uniform" => $"Circuit with {_numQubits} qubits, all in uniform superposition (H gates applied).",
            "random" => $"Circuit with {_numQubits} qubits, initialized to a random state.",
            _ => throw new InvalidOperationException("Unexpected state."),
        };
    }

    /// <summary>
    /// Provides the replacement message for this class.
    /// </summary>
    /// <returns>Replacement suggestion string.</returns>
    public string Replacement()
    {
        return "Use a QuantumCircuit object directly for initializing quantum states.";
    }

    /// <summary>
    /// Number of qubits for the state.
    /// </summary>
    public int NumQubits => _numQubits;

    /// <summary>
    /// Current state name.
    /// </summary>
    public string State => _state;
}
