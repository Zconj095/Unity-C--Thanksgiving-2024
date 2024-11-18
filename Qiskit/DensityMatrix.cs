using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DensityMatrix
{
    private Complex[,] _data;
    private int[] _dims;

    public DensityMatrix(object data, object dims = null)
    {
        if (data is Complex[,] matrix)
        {
            _data = matrix;
        }
        else if (data is List<List<Complex>> list)
        {
            int size = list.Count;
            _data = new Complex[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _data[i, j] = list[i][j];
                }
            }
        }
        else if (data is QuantumCircuit qc)
        {
            // Handle circuit or instruction input (simplified example)
            _data = DensityMatrixFromCircuit(qc);
        }
        else
        {
            throw new InvalidOperationException("Invalid input data format for DensityMatrix");
        }

        if (dims != null)
        {
            _dims = dims is int[] ? (int[])dims : new int[] { (int)dims };
        }
        else
        {
            _dims = new int[] { _data.GetLength(0) };
        }
    }

    private Complex[,] DensityMatrixFromCircuit(QuantumCircuit qc)
    {
        // Convert quantum circuit to density matrix (simplified)
        return new Complex[2, 2] { { new Complex(1, 0), new Complex(0, 0) }, { new Complex(0, 0), new Complex(0, 1) } };
    }

    public Complex[,] Data => _data;

    public bool IsValid(double atol = 1e-8, double rtol = 1e-5)
    {
        // Check for trace 1 and positive semidefiniteness
        if (Math.Abs(Trace().Real - 1) > atol)
        {
            return false;
        }
        if (!IsHermitian())
        {
            return false;
        }
        return IsPositiveSemidefinite();
    }

    public Complex Trace()
    {
        Complex trace = new Complex(0, 0);
        for (int i = 0; i < _data.GetLength(0); i++)
        {
            trace += _data[i, i];
        }
        return trace;
    }

    private bool IsHermitian()
    {
        for (int i = 0; i < _data.GetLength(0); i++)
        {
            for (int j = i + 1; j < _data.GetLength(0); j++)
            {
                if (_data[i, j] != Complex.Conjugate(_data[j, i]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool IsPositiveSemidefinite()
    {
        // Check if the eigenvalues are non-negative (positive semidefinite check)
        var eigenvalues = GetEigenvalues();
        return eigenvalues.All(ev => ev.Real >= 0);
    }

    private IEnumerable<Complex> GetEigenvalues()
    {
        // For simplicity, assuming this returns eigenvalues of the matrix
        // Use a numerical library to compute eigenvalues in practice (e.g., Math.NET)
        return new List<Complex> { new Complex(1, 0), new Complex(0, 1) }; // Dummy eigenvalues for demonstration
    }

    public virtual DensityMatrix Conjugate()
    {
        var conjugatedData = new Complex[_data.GetLength(0), _data.GetLength(1)];
        for (int i = 0; i < _data.GetLength(0); i++)
        {
            for (int j = 0; j < _data.GetLength(1); j++)
            {
                conjugatedData[i, j] = Complex.Conjugate(_data[i, j]);
            }
        }
        return new DensityMatrix(conjugatedData);
    }

    public DensityMatrix Multiply(Complex scalar)
    {
        var resultData = new Complex[_data.GetLength(0), _data.GetLength(1)];
        for (int i = 0; i < _data.GetLength(0); i++)
        {
            for (int j = 0; j < _data.GetLength(1); j++)
            {
                resultData[i, j] = scalar * _data[i, j];
            }
        }
        return new DensityMatrix(resultData);
    }

    public DensityMatrix TensorProduct(DensityMatrix other)
    {
        var resultData = new Complex[this._data.GetLength(0) * other._data.GetLength(0), 
                                      this._data.GetLength(1) * other._data.GetLength(1)];
        
        // Perform tensor product
        for (int i = 0; i < this._data.GetLength(0); i++)
        {
            for (int j = 0; j < this._data.GetLength(1); j++)
            {
                for (int k = 0; k < other._data.GetLength(0); k++)
                {
                    for (int l = 0; l < other._data.GetLength(1); l++)
                    {
                        resultData[i * other._data.GetLength(0) + k, j * other._data.GetLength(1) + l] =
                            _data[i, j] * other._data[k, l];
                    }
                }
            }
        }
        return new DensityMatrix(resultData);
    }

    public static DensityMatrix FromLabel(string label)
    {
        // Create a density matrix from a label like "0", "1", "+" etc.
        if (label == "0")
        {
            return new DensityMatrix(new Complex[,] { { new Complex(1, 0), new Complex(0, 0) }, { new Complex(0, 0), new Complex(0, 0) } });
        }
        if (label == "1")
        {
            return new DensityMatrix(new Complex[,] { { new Complex(0, 0), new Complex(0, 0) }, { new Complex(0, 0), new Complex(1, 0) } });
        }
        if (label == "+")
        {
            var half = new Complex(0.5, 0);
            return new DensityMatrix(new Complex[,] { { half, half }, { half, half } });
        }
        throw new ArgumentException("Invalid label");
    }
}