using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SparsePauliOp
{
    private PauliList _pauliList;
    private Complex[] _coeffs;

    public SparsePauliOp(object data, Complex[] coeffs = null, bool ignorePauliPhase = false, bool copy = true)
    {
        if (ignorePauliPhase && !(data is PauliList))
        {
            throw new Exception("ignorePauliPhase=True is only valid with PauliList data");
        }

        if (data is SparsePauliOp sparsePauliOp)
        {
            if (coeffs == null)
            {
                coeffs = sparsePauliOp._coeffs;
            }
            data = sparsePauliOp._pauliList;
            ignorePauliPhase = true;
        }

        var pauliList = new PauliList(data, copy);
        if (coeffs == null)
        {
            coeffs = new Complex[pauliList.Size];
            for (int i = 0; i < coeffs.Length; i++)
            {
                coeffs[i] = Complex.One;
            }
        }
        else
        {
            if (coeffs.Length != pauliList.Size)
            {
                throw new Exception("coeff vector is incorrect shape for number of Paulis");
            }
        }

        if (ignorePauliPhase)
        {
            _pauliList = pauliList;
            _coeffs = coeffs;
        }
        else
        {
            // Move the phase of pauliList to coeffs
            var phase = pauliList.Phase;
            var countY = pauliList.CountY();
            _coeffs = coeffs.Select(c => Complex.Pow(-Complex.ImaginaryOne, phase - countY) * c).ToArray();
            pauliList.Phase = countY % 4;
            _pauliList = pauliList;
        }
    }

    public int Size => _pauliList.Size;

    public PauliList Paulis => _pauliList;

    public Complex[] Coeffs => _coeffs;

    public static SparsePauliOp FromList(IEnumerable<Tuple<string, Complex>> pauliTerms)
    {
        var labels = new List<string>();
        var coeffs = new List<Complex>();

        foreach (var term in pauliTerms)
        {
            labels.Add(term.Item1);
            coeffs.Add(term.Item2);
        }

        return new SparsePauliOp(new PauliList(labels), coeffs.ToArray());
    }

    public override string ToString()
    {
        return $"SparsePauliOp({string.Join(", ", _pauliList.ToLabels())}, coeffs=[{string.Join(", ", _coeffs.Select(c => c.ToString("G")))}])";
    }

    public override bool Equals(object obj)
    {
        if (obj is SparsePauliOp other)
        {
            return _pauliList.Equals(other._pauliList) && _coeffs.SequenceEqual(other._coeffs);
        }
        return false;
    }

    public SparsePauliOp Conjugate()
    {
        var result = new SparsePauliOp(_pauliList.Copy(), _coeffs.Select(c => Complex.Conjugate(c)).ToArray(), true);
        return result;
    }

    public SparsePauliOp Transpose()
    {
        var result = new SparsePauliOp(_pauliList.Copy(), _coeffs, true);
        var minus = _pauliList.CountY().Select(count => (int)Math.Pow(-1, count)).ToArray();
        for (int i = 0; i < _coeffs.Length; i++)
        {
            _coeffs[i] *= minus[i];
        }
        return result;
    }

    public SparsePauliOp Adjoint()
    {
        return Conjugate();
    }

    public SparsePauliOp Compose(SparsePauliOp other, List<int> qargs = null, bool front = false)
    {
        var x1 = _pauliList.X;
        var z1 = _pauliList.Z;
        var x2 = other.Paulis.X;
        var z2 = other.Paulis.Z;

        // Further compose logic here, similar to Python version but using list operations

        return new SparsePauliOp(/* Constructed PauliList */, /* New Coeffs */);
    }

    public SparsePauliOp Multiply(Complex scalar)
    {
        return new SparsePauliOp(_pauliList.Copy(), _coeffs.Select(c => c * scalar).ToArray(), true);
    }

    public SparsePauliOp Simplify()
    {
        // Simplification logic for combining duplicates and removing zero coefficients
        return new SparsePauliOp(_pauliList.Copy(), _coeffs, true);
    }

    public static SparsePauliOp Sum(IEnumerable<SparsePauliOp> ops)
    {
        var combinedPauliList = new PauliList();
        var combinedCoeffs = new List<Complex>();

        foreach (var op in ops)
        {
            combinedPauliList.Add(op.Paulis);
            combinedCoeffs.AddRange(op.Coeffs);
        }

        return new SparsePauliOp(combinedPauliList, combinedCoeffs.ToArray());
    }
}

public class PauliList
{
    private List<string> _pauliStrings;

    public PauliList(object data, bool copy = true)
    {
        _pauliStrings = new List<string>(); // Initialize with appropriate data
    }

    public int Size => _pauliStrings.Count;

    public List<string> ToLabels() => _pauliStrings;

    public PauliList Copy()
    {
        return new PauliList(_pauliStrings.ToList());
    }

    public int CountY()
    {
        return _pauliStrings.Count(p => p.Contains("Y"));
    }

    public int Phase { get; set; }

    public int[] X => _pauliStrings.Select(p => p.Contains("X") ? 1 : 0).ToArray();
    public int[] Z => _pauliStrings.Select(p => p.Contains("Z") ? 1 : 0).ToArray();

    public bool Equals(PauliList other)
    {
        return _pauliStrings.SequenceEqual(other._pauliStrings);
    }

    public void Add(PauliList other)
    {
        _pauliStrings.AddRange(other._pauliStrings);
    }

    public void Add(string pauli)
    {
        _pauliStrings.Add(pauli);
    }
}

public struct Complex
{
    public static readonly Complex One = new Complex(1, 0);
    public static readonly Complex ImaginaryOne = new Complex(0, 1);
    
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public static Complex Pow(Complex value, double exponent)
    {
        double magnitude = Math.Pow(Math.Sqrt(value.Real * value.Real + value.Imaginary * value.Imaginary), exponent);
        double angle = Math.Atan2(value.Imaginary, value.Real) * exponent;
        return new Complex(magnitude * Math.Cos(angle), magnitude * Math.Sin(angle));
    }

    public static Complex operator *(Complex left, Complex right)
    {
        return new Complex(
            left.Real * right.Real - left.Imaginary * right.Imaginary,
            left.Real * right.Imaginary + left.Imaginary * right.Real
        );
    }

    public static Complex operator *(Complex left, double right)
    {
        return new Complex(left.Real * right, left.Imaginary * right);
    }

    public static Complex operator +(Complex left, Complex right)
    {
        return new Complex(left.Real + right.Real, left.Imaginary + right.Imaginary);
    }

    public static Complex Conjugate(Complex value)
    {
        return new Complex(value.Real, -value.Imaginary);
    }
}
