using System;
using System.Linq;

public class OpShape
{
    private int _numQargsL = 0; // Left (output) subsystems count
    private int _numQargsR = 0; // Right (input) subsystems count
    private int[] _dimsL = null; // Left (output) subsystem dimensions
    private int[] _dimsR = null; // Right (input) subsystem dimensions

    // Constructor
    public OpShape(int[] dimsL = null, int[] dimsR = null, int numQargsL = 0, int numQargsR = 0)
    {
        if (numQargsL > 0) _numQargsL = numQargsL;
        if (dimsL != null) _dimsL = dimsL;
        
        if (numQargsR > 0) _numQargsR = numQargsR;
        if (dimsR != null) _dimsR = dimsR;
    }

    // Properties for getting settings
    public object Settings
    {
        get
        {
            return new
            {
                dimsL = _dimsL,
                dimsR = _dimsR,
                numQargsL = _numQargsL,
                numQargsR = _numQargsR
            };
        }
    }

    // Display as string
    public override string ToString()
    {
        string left = _dimsL != null ? $"dimsL={string.Join(", ", _dimsL)}" : $"numQargsL={_numQargsL}";
        string right = _dimsR != null ? $"dimsR={string.Join(", ", _dimsR)}" : $"numQargsR={_numQargsR}";
        return $"OpShape({left}, {right})";
    }

    // Equality check
    public override bool Equals(object obj)
    {
        if (obj is OpShape other)
        {
            return _numQargsL == other._numQargsL &&
                   _numQargsR == other._numQargsR &&
                   (_dimsL.SequenceEqual(other._dimsL) || (_dimsL == null && other._dimsL == null)) &&
                   (_dimsR.SequenceEqual(other._dimsR) || (_dimsR == null && other._dimsR == null));
        }
        return false;
    }

    // Get size (multiplication of dimensions)
    public int Size
    {
        get
        {
            return _dimL * _dimR;
        }
    }

    // Number of qubits
    public int? NumQubits
    {
        get
        {
            if (_dimsL == null && _dimsR == null)
                return null;

            if (_dimsL != null && _dimsR == null && _numQargsL == _numQargsR)
                return _numQargsL;

            return null;
        }
    }

    // Return tuple of left and right qubit counts
    public (int numQargsL, int numQargsR) NumQargs
    {
        get
        {
            return (_numQargsL, _numQargsR);
        }
    }

    // Shape of the operator (rows, columns)
    public (int, int) Shape
    {
        get
        {
            if (_numQargsL == 0 && _numQargsR == 0)
                return (1, 1);
            if (_numQargsR == 0)
                return (_dimsL.Length, 1);
            return (_dimsL.Length, _dimsR.Length);
        }
    }

    // Tensor shape (reverse order of subsystems)
    public int[] TensorShape
    {
        get
        {
            return _dimsL.Concat(_dimsR).ToArray();
        }
    }

    // Square matrix check
    public bool IsSquare
    {
        get
        {
            return _numQargsL == _numQargsR && _dimsL.SequenceEqual(_dimsR);
        }
    }

    // Get dimensions of right subsystems (input)
    public int[] DimsR(int[] qargs = null)
    {
        if (_dimsR != null)
        {
            return qargs != null ? qargs.Select(i => _dimsR[i]).ToArray() : _dimsR;
        }
        return new int[_numQargsR];
    }

    // Get dimensions of left subsystems (output)
    public int[] DimsL(int[] qargs = null)
    {
        if (_dimsL != null)
        {
            return qargs != null ? qargs.Select(i => _dimsL[i]).ToArray() : _dimsL;
        }
        return new int[_numQargsL];
    }

    // Total input dimension
    private int _dimR
    {
        get
        {
            return _dimsR != null ? _dimsR.Aggregate(1, (prod, dim) => prod * dim) : (int)Math.Pow(2, _numQargsR);
        }
    }

    // Total output dimension
    private int _dimL
    {
        get
        {
            return _dimsL != null ? _dimsL.Aggregate(1, (prod, dim) => prod * dim) : (int)Math.Pow(2, _numQargsL);
        }
    }

    // Validate shape
    public void ValidateShape(int[] shape)
    {
        if (shape.Length > 2)
        {
            throw new InvalidOperationException("Shape must be 1 or 2-dimensional.");
        }

        if (_dimsL != null && _dimsL.Aggregate(1, (prod, dim) => prod * dim) != shape[0])
        {
            throw new InvalidOperationException("Left dimensions do not match matrix shape.");
        }

        if (_dimsR != null && _dimsR.Aggregate(1, (prod, dim) => prod * dim) != shape[1])
        {
            throw new InvalidOperationException("Right dimensions do not match matrix shape.");
        }
    }

    // Copy this OpShape
    public OpShape Copy()
    {
        return new OpShape(_dimsL, _dimsR, _numQargsL, _numQargsR);
    }

    // Get a subset of OpShape
    public OpShape Subset(int[] qargsL = null, int[] qargsR = null)
    {
        int[] newDimsL = qargsL != null ? qargsL.Select(i => _dimsL[i]).ToArray() : _dimsL;
        int[] newDimsR = qargsR != null ? qargsR.Select(i => _dimsR[i]).ToArray() : _dimsR;
        return new OpShape(newDimsL, newDimsR, qargsL?.Length ?? _numQargsL, qargsR?.Length ?? _numQargsR);
    }

    // Reverse the order of subsystems
    public OpShape Reverse()
    {
        var reversedDimsL = _dimsL?.Reverse().ToArray();
        var reversedDimsR = _dimsR?.Reverse().ToArray();
        return new OpShape(reversedDimsL, reversedDimsR, _numQargsR, _numQargsL);
    }

    // Transpose the operator (swap left and right dimensions)
    public OpShape Transpose()
    {
        return new OpShape(_dimsR, _dimsL, _numQargsR, _numQargsL);
    }

    // Compute the tensor product of two OpShapes
    public static OpShape Tensor(OpShape a, OpShape b)
    {
        var dimsL = a.DimsL().Concat(b.DimsL()).ToArray();
        var dimsR = a.DimsR().Concat(b.DimsR()).ToArray();
        return new OpShape(dimsL, dimsR, a._numQargsL + b._numQargsL, a._numQargsR + b._numQargsR);
    }

    // Compose two OpShapes
    public OpShape Compose(OpShape other)
    {
        if (_dimsR.Length != other._dimsL.Length)
        {
            throw new InvalidOperationException("Dimensions do not match for composition.");
        }

        var newDimsL = _dimsL;
        var newDimsR = other._dimsR;

        return new OpShape(newDimsL, newDimsR, _numQargsL, other._numQargsR);
    }
}
