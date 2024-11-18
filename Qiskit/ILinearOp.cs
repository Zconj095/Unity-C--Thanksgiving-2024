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

    // Overload the multiplication operator (use * for matrix multiplication)
    public static MatrixOperator operator *(MatrixOperator left, MatrixOperator right)
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

        // Ensure ab and ba are MatrixOperator objects
        if (ab is MatrixOperator abMatrix && ba is MatrixOperator baMatrix)
        {
            // Perform the matrix addition after multiplying with identity matrix
            var identityMatrix = new MatrixOperator(Matrix4x4.identity);
            var resultAB = abMatrix.Multiply(identityMatrix);
            var resultBA = baMatrix.Multiply(identityMatrix);
            return new MatrixOperator(resultAB.Matrix + resultBA.Matrix);
        }

        throw new InvalidOperationException("Invalid matrix types in anti-commutator.");
    }
}
