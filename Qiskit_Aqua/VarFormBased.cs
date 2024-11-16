using System;
using System.Collections.Generic;

public class VarFormBased:
{
    // Fields for the variational form and its parameters
    private QuantumCircuit _varForm;
    private List<double> _parameters;

    /// <summary>
    /// Initializes a new instance of the <see cref="VarFormBased"/> class.
    /// </summary>
    /// <param name="varForm">A parameterized quantum circuit representing the variational form.</param>
    /// <param name="parameters">A list of parameters for the variational form.</param>
    public VarFormBased(QuantumCircuit varForm, List<double> parameters)
    {
        if (varForm.ParameterCount != parameters.Count)
        {
            throw new ArgumentException($"Parameter count mismatch: Expected {varForm.ParameterCount}, got {parameters.Count}.");
        }

        _varForm = varForm;
        _parameters = parameters;
    }

    /// <summary>
    /// Constructs the quantum circuit representing the initial state.
    /// </summary>
    /// <returns>A quantum circuit with the parameters applied.</returns>
    public QuantumCircuit ConstructCircuit()
    {
        QuantumCircuit circuit = new QuantumCircuit(_varForm.QubitCount);

        // Apply the parameters to the variational form
        QuantumCircuit parameterizedCircuit = _varForm.AssignParameters(_parameters);

        // Compose the parameterized circuit into the main circuit
        circuit.Compose(parameterizedCircuit);
        return circuit;
    }

    /// <summary>
    /// Gets the variational form.
    /// </summary>
    public QuantumCircuit VariationalForm
    {
        get { return _varForm; }
    }

    /// <summary>
    /// Gets the parameters for the variational form.
    /// </summary>
    public List<double> Parameters
    {
        get { return _parameters; }
    }
}
