using System;
using UnityEngine;

public interface ILinearOp
{
    // Define any matrix or operator-related methods here.
    ILinearOp Multiply(ILinearOp other);
}

public class MatrixOperator : ILinearOp
{
    private Matrix4x4 matrix;

    public MatrixOperator(Matrix4x4 matrix)
    {
        this.matrix = matrix;
    }

    public ILinearOp Multiply(ILinearOp other)
    {
        if (other is MatrixOperator otherMatrix)
        {
            return new MatrixOperator(matrix * otherMatrix.matrix); // Matrix multiplication
        }

        throw new InvalidOperationException("Unsupported operation");
    }

    // This can be expanded for other necessary operations
    public static MatrixOperator operator @(MatrixOperator left, MatrixOperator right)
    {
        return new MatrixOperator(left.matrix * right.matrix);
    }
}

public static class OperatorUtils
{
    public static ILinearOp AntiCommutator(ILinearOp a, ILinearOp b)
    {
        // Compute the anti-commutator ab + ba
        ILinearOp ab = a.Multiply(b);
        ILinearOp ba = b.Multiply(a);

        return ab.Multiply(new MatrixOperator(Matrix4x4.identity)) + ba.Multiply(new MatrixOperator(Matrix4x4.identity));
    }
}
