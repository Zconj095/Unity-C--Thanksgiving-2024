using System;
using System.Linq;

public class SuperOp
{
    private Complex[,] _data;
    private OpShape _opShape;
    private int _inputDim;
    private int _outputDim;

    public SuperOp(object data, Tuple<int, int>? inputDims = null, Tuple<int, int>? outputDims = null)
    {
        if (data is Complex[,] matrix)
        {
            _data = matrix;
            int dout = matrix.GetLength(0), din = matrix.GetLength(1);
            _inputDim = (int)Math.Sqrt(din);
            _outputDim = (int)Math.Sqrt(dout);

            if (_outputDim * _outputDim != dout || _inputDim * _inputDim != din)
            {
                throw new Exception("Invalid shape for SuperOp matrix.");
            }

            _opShape = new OpShape(inputDims, outputDims);
        }
        else
        {
            throw new NotImplementedException("Conversion from other types is not implemented.");
        }
    }

    public Complex[,] Data
    {
        get { return _data; }
    }

    public Tuple<int, int> Dimensions
    {
        get { return new Tuple<int, int>(_inputDim, _outputDim); }
    }

    public SuperOp Conjugate()
    {
        var newData = new Complex[_data.GetLength(0), _data.GetLength(1)];
        for (int i = 0; i < _data.GetLength(0); i++)
        {
            for (int j = 0; j < _data.GetLength(1); j++)
            {
                newData[i, j] = Complex.Conjugate(_data[i, j]);
            }
        }

        var ret = new SuperOp(newData);
        return ret;
    }

    public SuperOp Transpose()
    {
        var newData = new Complex[_data.GetLength(1), _data.GetLength(0)];
        for (int i = 0; i < _data.GetLength(0); i++)
        {
            for (int j = 0; j < _data.GetLength(1); j++)
            {
                newData[j, i] = _data[i, j];
            }
        }

        var ret = new SuperOp(newData);
        ret._opShape = _opShape.Transpose();
        return ret;
    }

    public SuperOp Adjoint()
    {
        var newData = new Complex[_data.GetLength(1), _data.GetLength(0)];
        for (int i = 0; i < _data.GetLength(0); i++)
        {
            for (int j = 0; j < _data.GetLength(1); j++)
            {
                newData[j, i] = Complex.Conjugate(_data[i, j]);
            }
        }

        var ret = new SuperOp(newData);
        ret._opShape = _opShape.Transpose();
        return ret;
    }

    public SuperOp Tensor(SuperOp other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        var newShape = _opShape.Tensor(other._opShape);
        var newData = Matrix.Kron(_data, other.Data);
        return new SuperOp(newData, newShape.Dimensions, newShape.Dimensions);
    }

    public SuperOp Compose(SuperOp other, bool front = false)
    {
        var newShape = _opShape.Compose(other._opShape, front);
        var newData = front 
            ? Matrix.Multiply(_data, other.Data)
            : Matrix.Multiply(other.Data, _data);

        return new SuperOp(newData, newShape.Dimensions, newShape.Dimensions);
    }

    public static SuperOp FromMatrix(Complex[,] matrix)
    {
        return new SuperOp(matrix);
    }

    public class OpShape
    {
        private Tuple<int, int> _inputDims;
        private Tuple<int, int> _outputDims;

        public OpShape(Tuple<int, int>? inputDims = null, Tuple<int, int>? outputDims = null)
        {
            _inputDims = inputDims ?? new Tuple<int, int>(1, 1);
            _outputDims = outputDims ?? new Tuple<int, int>(1, 1);
        }

        public OpShape Transpose()
        {
            return new OpShape(new Tuple<int, int>(_outputDims.Item2, _outputDims.Item1));
        }

        public OpShape Tensor(OpShape other)
        {
            return new OpShape(
                new Tuple<int, int>(_outputDims.Item1 * other._outputDims.Item1, _inputDims.Item1 * other._inputDims.Item1),
                new Tuple<int, int>(_outputDims.Item2 * other._outputDims.Item2, _inputDims.Item2 * other._inputDims.Item2)
            );
        }

        public OpShape Compose(OpShape other, bool front)
        {
            // For simplicity, assuming same structure when composing
            return new OpShape(_inputDims, other._outputDims);
        }

        public Tuple<int, int> Dimensions
        {
            get { return _outputDims; }
        }
    }

    public static class Matrix
    {
        public static Complex[,] Multiply(Complex[,] a, Complex[,] b)
        {
            int aRows = a.GetLength(0), aCols = a.GetLength(1);
            int bRows = b.GetLength(0), bCols = b.GetLength(1);

            if (aCols != bRows)
                throw new ArgumentException("Incompatible matrix dimensions for multiplication.");

            Complex[,] result = new Complex[aRows, bCols];
            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < bCols; j++)
                {
                    result[i, j] = Complex.Zero;
                    for (int k = 0; k < aCols; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return result;
        }

        public static Complex[,] Kron(Complex[,] a, Complex[,] b)
        {
            int aRows = a.GetLength(0), aCols = a.GetLength(1);
            int bRows = b.GetLength(0), bCols = b.GetLength(1);

            Complex[,] result = new Complex[aRows * bRows, aCols * bCols];
            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < aCols; j++)
                {
                    Complex scalar = a[i, j];
                    for (int k = 0; k < bRows; k++)
                    {
                        for (int l = 0; l < bCols; l++)
                        {
                            result[i * bRows + k, j * bCols + l] = scalar * b[k, l];
                        }
                    }
                }
            }
            return result;
        }
    }

    public struct Complex
    {
        public double Real;
        public double Imaginary;

        public static Complex Zero => new Complex(0, 0);
        public static Complex One => new Complex(1, 0);

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public double Magnitude => Math.Sqrt(Real * Real + Imaginary * Imaginary);
        
        public static Complex operator +(Complex a, Complex b) => new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        public static Complex operator -(Complex a, Complex b) => new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        public static Complex operator *(Complex a, Complex b) => new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
        public static Complex operator *(Complex a, double b) => new Complex(a.Real * b, a.Imaginary * b);
        public static Complex operator *(double b, Complex a) => new Complex(a.Real * b, a.Imaginary * b);
        
        public static Complex Conjugate(Complex c) => new Complex(c.Real, -c.Imaginary);
    }
}
