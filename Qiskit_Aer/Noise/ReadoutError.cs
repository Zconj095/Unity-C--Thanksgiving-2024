using System;
using System.Collections.Generic;
using System.Linq;

public class ReadoutError
{
    private readonly double[,] _probabilities;
    private readonly int _numberOfQubits;

    public ReadoutError(double[,] probabilities, double atol = 1e-8)
    {
        CheckProbabilities(probabilities, atol);
        _probabilities = probabilities;
        _numberOfQubits = (int)Math.Log2(probabilities.GetLength(0));

        if (_probabilities.GetLength(0) != Math.Pow(2, _numberOfQubits) ||
            _probabilities.GetLength(1) != Math.Pow(2, _numberOfQubits))
        {
            throw new ArgumentException("Input readout error probabilities must be a 2^N by 2^N matrix.");
        }
    }

    public override string ToString()
    {
        string output = $"ReadoutError on {_numberOfQubits} qubits. Assignment probabilities:\n";
        for (int i = 0; i < _probabilities.GetLength(0); i++)
        {
            output += $"P(j|{i}) = [{string.Join(", ", Enumerable.Range(0, _probabilities.GetLength(1)).Select(j => _probabilities[i, j]))}]\n";
        }
        return output;
    }

    public bool IsIdeal()
    {
        int size = _probabilities.GetLength(0);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if ((i == j && Math.Abs(_probabilities[i, j] - 1) > 1e-8) ||
                    (i != j && Math.Abs(_probabilities[i, j]) > 1e-8))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public int NumberOfQubits => _numberOfQubits;

    public double[,] Probabilities => _probabilities;

    public ReadoutError Compose(ReadoutError other, bool front = false)
    {
        if (other.NumberOfQubits != _numberOfQubits)
        {
            throw new ArgumentException("Both readout errors must have the same number of qubits.");
        }

        int size = _probabilities.GetLength(0);
        var result = new double[size, size];

        if (front)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = Enumerable.Range(0, size).Sum(k => other._probabilities[i, k] * _probabilities[k, j]);
                }
            }
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = Enumerable.Range(0, size).Sum(k => _probabilities[i, k] * other._probabilities[k, j]);
                }
            }
        }

        return new ReadoutError(result);
    }

    public ReadoutError Tensor(ReadoutError other)
    {
        int thisSize = _probabilities.GetLength(0);
        int otherSize = other._probabilities.GetLength(0);
        int newSize = thisSize * otherSize;

        var result = new double[newSize, newSize];

        for (int i = 0; i < thisSize; i++)
        {
            for (int j = 0; j < thisSize; j++)
            {
                for (int k = 0; k < otherSize; k++)
                {
                    for (int l = 0; l < otherSize; l++)
                    {
                        result[i * otherSize + k, j * otherSize + l] = _probabilities[i, j] * other._probabilities[k, l];
                    }
                }
            }
        }

        return new ReadoutError(result);
    }

    public ReadoutError Expand(ReadoutError other)
    {
        return other.Tensor(this);
    }

    public Dictionary<string, object> ToDict()
    {
        return new Dictionary<string, object>
        {
            { "type", "roerror" },
            { "operations", new List<string> { "measure" } },
            { "probabilities", _probabilities }
        };
    }

    public static ReadoutError FromDict(Dictionary<string, object> dict)
    {
        if (!dict.ContainsKey("probabilities") || !(dict["probabilities"] is double[,] probabilities))
        {
            throw new ArgumentException("Invalid error dictionary.");
        }

        return new ReadoutError(probabilities);
    }

    private static void CheckProbabilities(double[,] probabilities, double atol)
    {
        if (probabilities == null || probabilities.GetLength(0) == 0 || probabilities.GetLength(1) == 0)
        {
            throw new ArgumentException("Input probabilities cannot be null or empty.");
        }

        int size = probabilities.GetLength(0);
        if (size != probabilities.GetLength(1))
        {
            throw new ArgumentException("Input probabilities must be a square matrix.");
        }

        for (int i = 0; i < size; i++)
        {
            double rowSum = 0;
            for (int j = 0; j < size; j++)
            {
                if (probabilities[i, j] < -atol)
                {
                    throw new ArgumentException($"Invalid probability value {probabilities[i, j]} at ({i}, {j}).");
                }
                rowSum += probabilities[i, j];
            }

            if (Math.Abs(rowSum - 1) > atol)
            {
                throw new ArgumentException($"Row {i} of probabilities does not sum to 1.");
            }
        }
    }
}
