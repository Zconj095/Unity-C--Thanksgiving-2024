using System;
using System.Collections.Generic;

public class SetMatrixProductState : QuantumInstruction
{
    /// <summary>
    /// Instruction to set the matrix product state of the simulator.
    /// </summary>
    /// <param name="state">Matrix product state consisting of pairs of complex matrices and double vectors.</param>
    public SetMatrixProductState(Tuple<List<Tuple<Complex[,]>>, List<List<double>>> state) 
        : base("set_matrix_product_state", state.Item1.Count, 0)
    {
        if (!IsValidMatrixProductState(state))
        {
            throw new ArgumentException("The input matrix product state is not valid.", nameof(state));
        }

        Parameters.Add(state);
    }

    /// <summary>
    /// Validates the structure of the matrix product state.
    /// </summary>
    private bool IsValidMatrixProductState(Tuple<List<Tuple<Complex[,]>>, List<List<double>>> state)
    {
        if (state.Item1.Count != state.Item2.Count + 1)
        {
            return false;
        }

        foreach (var elem in state.Item1)
        {
            if (elem == null || elem.Item1 == null || elem.Item2 == null)
            {
                return false;
            }
        }

        return true;
    }
}
