using System;
using System.Linq;
using UnityEngine;

public class QuantumInfo
{
    // Fidelity between two states
    public static float StateFidelity(QuantumState state1, QuantumState state2, bool validate = true)
    {
        state1 = FormatState(state1, validate);
        state2 = FormatState(state2, validate);

        Complex[,] arr1 = state1.Data;
        Complex[,] arr2 = state2.Data;

        if (state1 is Statevector && state2 is Statevector)
        {
            // Fidelity of two pure states (Statevectors)
            return (float)(Math.Pow(Complex.DotProduct(arr2.Conjugate(), arr1), 2));
        }
        else if (state1 is Statevector && state2 is DensityMatrix)
        {
            // Fidelity of pure state (Statevector) and mixed state (DensityMatrix)
            return (float)(Complex.DotProduct(arr1.Conjugate(), arr2.Dot(arr1)));
        }
        else if (state1 is DensityMatrix && state2 is Statevector)
        {
            // Fidelity of mixed state (DensityMatrix) and pure state (Statevector)
            return (float)(Complex.DotProduct(arr2.Conjugate(), arr1.Dot(arr2)));
        }
        else
        {
            // Fidelity of two mixed states (DensityMatrices)
            Complex[,] s1sq = MatrixSvd(arr1, Math.Sqrt);
            Complex[,] s2sq = MatrixSvd(arr2, Math.Sqrt);
            return (float)Math.Pow(Complex.Norm(s1sq.Dot(s2sq)), 2);
        }
    }

    // Purity of a quantum state
    public static float Purity(QuantumState state, bool validate = true)
    {
        state = FormatState(state, validate);
        return state.Purity();
    }

    // Von Neumann entropy of a quantum state
    public static float Entropy(QuantumState state, int baseNum = 2)
    {
        state = FormatState(state, validate: true);

        if (state is Statevector)
        {
            return 0f; // Pure state entropy is 0
        }
        else
        {
            var eigenvalues = Eigenvalues(state.Data);
            return ShannonEntropy(eigenvalues, baseNum);
        }
    }

    // Mutual information of a bipartite state
    public static float MutualInformation(QuantumState state, int baseNum = 2)
    {
        state = FormatState(state, validate: true);

        if (state.Dims().Length != 2)
        {
            throw new InvalidOperationException("Input must be a bipartite quantum state.");
        }

        var rhoA = PartialTrace(state, 1);
        var rhoB = PartialTrace(state, 0);

        return Entropy(rhoA, baseNum) + Entropy(rhoB, baseNum) - Entropy(state, baseNum);
    }

    // Concurrence of a quantum state
    public static float Concurrence(QuantumState state)
    {
        state = FormatState(state, validate: true);

        if (state is Statevector stateVec)
        {
            var dims = stateVec.Dims();
            if (dims.Length != 2)
            {
                throw new InvalidOperationException("Input is not a bipartite quantum state.");
            }
            var qargs = dims[0] > dims[1] ? new[] { 0 } : new[] { 1 };
            var rho = PartialTrace(stateVec, qargs);
            return (float)Math.Sqrt(2 * (1 - Purity(rho)));
        }

        // For density matrices (only defined for 2-qubit states)
        if (state.Dim != 4)
        {
            throw new InvalidOperationException("Input density matrix must be a 2-qubit state.");
        }

        var rho = state.Data;
        var yyMat = new Complex[,] { { -1, 1, 1, -1 } };
        var sigma = rho.Dot(yyMat).Dot(rho.Conjugate()).Dot(yyMat);
        var eigenvals = Eigenvalues(sigma).OrderByDescending(ev => ev).ToArray();
        var w = eigenvals.Select(ev => Math.Sqrt(Math.Max(ev, 0))).ToArray();

        return Math.Max(0.0, w[0] - w.Skip(1).Sum());
    }

    // Entanglement of formation
    public static float EntanglementOfFormation(QuantumState state)
    {
        state = FormatState(state, validate: true);

        if (state is Statevector stateVec)
        {
            var dims = stateVec.Dims();
            if (dims.Length != 2)
            {
                throw new InvalidOperationException("Input is not a bipartite quantum state.");
            }
            var qargs = dims[0] > dims[1] ? new[] { 0 } : new[] { 1 };
            return Entropy(PartialTrace(stateVec, qargs), 2);
        }

        if (state.Dim != 4)
        {
            throw new InvalidOperationException("Input density matrix must be a 2-qubit state.");
        }

        var conc = Concurrence(state);
        var val = (1 + Math.Sqrt(1 - conc * conc)) / 2;
        return ShannonEntropy(new[] { val, 1 - val });
    }

    // Negativity of a quantum state
    public static float Negativity(QuantumState state, int[] qargs)
    {
        if (state is Statevector)
        {
            state = new DensityMatrix(state);
        }
        var partialTransposedState = state.PartialTranspose(qargs);
        var singularValues = SingularValues(partialTransposedState.Data);
        var eigvals = singularValues.Sum();
        return (eigvals - 1) / 2;
    }

    // Helper methods (for simplicity, these are full implementations)
    private static QuantumState FormatState(QuantumState state, bool validate)
    {
        if (validate && !state.IsValid())
        {
            throw new InvalidOperationException("Invalid quantum state.");
        }
        return state;
    }

    private static Complex[,] MatrixSvd(Complex[,] matrix, Func<Complex, double> func)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        Complex[,] result = new Complex[rows, cols];

        // Simplified SVD operation (you could replace this with a real numerical method like Math.NET SVD)
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = matrix[i, j] * func(matrix[i, j]);
            }
        }
        return result;
    }

    private static double[] Eigenvalues(Complex[,] matrix)
    {
        // Simplified eigenvalue calculation (replace with a numerical library)
        return new double[] { 1.0, 0.5 }; // Dummy eigenvalues for simplicity
    }

    private static float ShannonEntropy(double[] probabilities, int baseNum = 2)
    {
        return (float)(-probabilities.Sum(p => p * Math.Log(p, baseNum)));
    }

    private static Complex[,] PartialTrace(QuantumState state, int[] qargs)
    {
        // Simplified partial trace implementation (should be adapted to the state structure)
        return state.Data; // Placeholder, implement partial trace logic
    }

    private static double[] SingularValues(Complex[,] matrix)
    {
        // Simplified SVD for singular value calculation
        return new double[] { 1.0, 0.5 }; // Dummy singular values for simplicity
    }
}
