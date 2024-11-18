using System;

public class QuantumOperator
{
    // A simple matrix representation for the operator (for example, a 2x2 matrix for single qubit operators)
    public double[,] Matrix { get; set; }

    // Constructor for a quantum operator (2x2 matrix as an example for single qubit operators)
    public QuantumOperator(double[,] matrix)
    {
        if (matrix.GetLength(0) != matrix.GetLength(1))
        {
            throw new ArgumentException("Matrix must be square.");
        }

        Matrix = matrix;
    }

    // Method to apply the operator to a quantum state (for simplicity, this assumes a 2x2 matrix and a 2D state vector)
    public double[] Apply(double[] state)
    {
        if (state.Length != Matrix.GetLength(0))
        {
            throw new ArgumentException("State vector size must match matrix dimensions.");
        }

        double[] result = new double[state.Length];
        for (int i = 0; i < state.Length; i++)
        {
            result[i] = 0;
            for (int j = 0; j < state.Length; j++)
            {
                result[i] += Matrix[i, j] * state[j];
            }
        }

        return result;
    }

    // Example of a Pauli-X operator
    public static QuantumOperator PauliX()
    {
        return new QuantumOperator(new double[,] { { 0, 1 }, { 1, 0 } });
    }

    // Example of a Pauli-Z operator
    public static QuantumOperator PauliZ()
    {
        return new QuantumOperator(new double[,] { { 1, 0 }, { 0, -1 } });
    }

    // Example of an identity operator (2x2 identity matrix)
    public static QuantumOperator Identity()
    {
        return new QuantumOperator(new double[,] { { 1, 0 }, { 0, 1 } });
    }

    // Example of a Hadamard gate (for a single qubit)
    public static QuantumOperator Hadamard()
    {
        return new QuantumOperator(new double[,] { { 1 / Math.Sqrt(2), 1 / Math.Sqrt(2) }, { 1 / Math.Sqrt(2), -1 / Math.Sqrt(2) } });
    }
}
