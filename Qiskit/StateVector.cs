using System;
using System.Linq;
using System.Collections.Generic;

public class Statevector : QuantumState
{
    private ComplexNumber[] _data;
    private OpShape _opShape;

    public Statevector(ComplexNumber[] data, int[] dims = null)
    {
        if (data == null || data.Length == 0)
            throw new ArgumentException("Invalid input data format for Statevector");

        _data = data;
        _opShape = new OpShape(dims);

        if (_data.Length == 1) return; // It's already a vector
        if (_data.Length > 1 && dims != null && dims.Length == 1)
            _data = _data.Take(dims[0]).ToArray();
    }

    public ComplexNumber[] Data => _data;

    public int Dim => _opShape.Shape[0];

    public int NumQubits => _opShape.NumQubits;

    public override bool IsValid(double atol = 1e-8, double rtol = 1e-5)
    {
        double norm = _data.Select(d => d.Magnitude * d.Magnitude).Sum();
        return Math.Abs(norm - 1.0) < atol;
    }

    public override QuantumState ToOperator()
    {
        var mat = new ComplexNumber[Dim, Dim];
        for (int i = 0; i < Dim; i++)
        {
            mat[i, i] = _data[i];
        }
        return new Operator(mat, dims: _opShape.Dims());
    }

    public override QuantumState Conjugate()
    {
        return new Statevector(_data.Select(d => ComplexNumber.Conjugate(d)).ToArray(), _opShape.Dims());
    }

    public override double Trace()
    {
        return _data.Select(d => d.Magnitude * d.Magnitude).Sum();
    }

    public override double Purity()
    {
        return Trace() * Trace();
    }

    public override QuantumState Tensor(QuantumState other)
    {
        var otherState = other as Statevector;
        if (otherState == null)
            throw new InvalidOperationException("Tensor product can only be performed with another Statevector");

        var resultData = new ComplexNumber[_data.Length * otherState.Data.Length];
        for (int i = 0; i < _data.Length; i++)
        {
            for (int j = 0; j < otherState.Data.Length; j++)
            {
                resultData[i * otherState.Data.Length + j] = _data[i] * otherState.Data[j];
            }
        }

        return new Statevector(resultData, _opShape.Tensor(other._opShape).Dims());
    }

    public override QuantumState Expand(QuantumState other)
    {
        return Tensor(other);
    }

    public QuantumState Evolve(Operator oper, List<int> qargs = null)
    {
        var resultData = new ComplexNumber[oper.Dims[0]];
        for (int i = 0; i < oper.Dims[0]; i++)
        {
            resultData[i] = _data.Select((data, index) => data * oper.Data[i, index]).Sum();
        }
        return new Statevector(resultData, _opShape.Dims());
    }

    public override double ExpectationValue(BaseOperator oper, List<int> qargs = null)
    {
        // Ensure the operator and quantum state dimensions are compatible
        if (oper.Dims.Length != this.Dim)
        {
            throw new InvalidOperationException("Operator dimensions do not match the state vector dimensions.");
        }

        double expectationValue = 0.0;
        for (int i = 0; i < this.Dim; i++)
        {
            for (int j = 0; j < this.Dim; j++)
            {
                expectationValue += _data[i].Real * oper.Data[i, j].Real - _data[i].Imaginary * oper.Data[i, j].Imaginary; // Real part
                expectationValue += _data[i].Real * oper.Data[i, j].Imaginary + _data[i].Imaginary * oper.Data[i, j].Real; // Imaginary part
            }
        }

        return expectationValue;
    }

    public override double[] Probabilities(List<int> qargs = null, int? decimals = null)
    {
        var probs = _data.Select(d => d.Magnitude * d.Magnitude).ToArray();
        if (decimals.HasValue)
        {
            for (int i = 0; i < probs.Length; i++)
            {
                probs[i] = Math.Round(probs[i], decimals.Value);
            }
        }
        return probs;
    }

    public string Measure(List<int> qargs = null)
    {
        var probs = Probabilities(qargs);
        var index = new Random().Next(probs.Length);
        var outcome = Convert.ToString(index, 2).PadLeft(Dim, '0');
        return outcome;
    }

    public override string ToString()
    {
        return $"Statevector({string.Join(", ", _data.Select(d => d.ToString()))}), dims={string.Join(", ", _opShape.Dims())}";
    }
}