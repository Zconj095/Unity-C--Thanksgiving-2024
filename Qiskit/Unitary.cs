public class Unitary
{
    public Complex[,] Data { get; private set; }

    public Unitary(Complex[,] matrix)
    {
        Data = matrix;

        if (!IsUnitary())
        {
            throw new ArgumentException("The matrix is not unitary.");
        }
    }

    // Method to check if the matrix is unitary: U† * U = I
    public bool IsUnitary()
    {
        var conjugateTranspose = ConjugateTranspose(Data);
        var identity = Multiply(conjugateTranspose, Data);
        return IsIdentity(identity);
    }

    // Conjugate transpose of a matrix
    private Complex[,] ConjugateTranspose(Complex[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        var result = new Complex[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[j, i] = matrix[i, j].Conjugate();
            }
        }

        return result;
    }

    // Multiply two matrices
    private Complex[,] Multiply(Complex[,] matrix1, Complex[,] matrix2)
    {
        int rows = matrix1.GetLength(0);
        int cols = matrix2.GetLength(1);
        int common = matrix1.GetLength(1);
        var result = new Complex[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < common; k++)
                {
                    result[i, j] += matrix1[i, k] * matrix2[k, j];
                }
            }
        }

        return result;
    }

    // Check if a matrix is the identity matrix
    private bool IsIdentity(Complex[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        if (rows != cols) return false;  // Identity matrix must be square

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i == j && matrix[i, j] != Complex.One)
                    return false;
                else if (i != j && matrix[i, j] != Complex.Zero)
                    return false;
            }
        }

        return true;
    }
}
