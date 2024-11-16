using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class QuantumState
{
    // Define the quantum state shape (op shape)
    protected OpShape _opShape;
    private System.Random _rngGenerator;

    // Constructor initializing the quantum state with its shape and random number generator
    public QuantumState(OpShape opShape = null)
    {
        _opShape = opShape;
        _rngGenerator = new System.Random();
    }

    // Property to return the total dimension of the quantum state
    public int Dim => _opShape.Shape[0];

    // Property to return the number of qubits (log2 of the dimension)
    public int NumQubits => _opShape.NumQubits;

    // Property to access the RNG generator
    public System.Random Rng => _rngGenerator;

    // Method to get the dimensions of the state
    public int[] Dims()
    {
        return _opShape.DimsL();
    }

    // Method to set the seed for random number generation
    public void Seed(int seed)
    {
        _rngGenerator = new System.Random(seed);
    }

    // Abstract method to validate the quantum state (to be implemented in subclasses)
    public abstract bool IsValid(double atol = 1e-8, double rtol = 1e-5);

    // Abstract method to convert state to operator (to be implemented in subclasses)
    public abstract QuantumState ToOperator();

    // Abstract method to return the conjugate of the quantum state
    public abstract QuantumState Conjugate();

    // Abstract method to calculate the trace of the quantum state
    public abstract double Trace();

    // Abstract method to calculate the purity of the quantum state
    public abstract double Purity();

    // Abstract method to compute the tensor product with another quantum state
    public abstract QuantumState Tensor(QuantumState other);

    // Abstract method to expand the quantum state with another state
    public abstract QuantumState Expand(QuantumState other);

    // Method to evolve the quantum state by applying an operator
    public virtual QuantumState Evolve(Operator operatorObj, List<int> qargs = null)
    {
        // Apply operator evolution (this can be extended depending on the operator type)
        return this; // Placeholder for evolution logic
    }

    // Abstract method to compute the expectation value of an operator
    public abstract double ExpectationValue(BaseOperator oper, List<int> qargs = null);

    // Abstract method to calculate measurement probabilities
    public abstract double[] Probabilities(List<int> qargs = null, int? decimals = null);

    // Method to return probabilities in a dictionary-like format
    public virtual double[] ProbabilitiesDict(List<int> qargs = null, int? decimals = null)
    {
        return _VectorToDict(Probabilities(qargs, decimals));
    }

    // Method to simulate measurement and sample from memory
    public string SampleMemory(int shots, List<int> qargs = null)
    {
        // Sample measurements
        var probs = Probabilities(qargs);
        var labels = _IndexToKetArray(Enumerable.Range(0, probs.Length).ToArray(), Dims(), true);
        return string.Join(", ", labels.Take(shots)); // Simplified example
    }

    // Method to simulate sampled counts from measurement outcomes
    public string SampleCounts(int shots, List<int> qargs = null)
    {
        var samples = SampleMemory(shots, qargs);
        var counts = samples.Split(", ").GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        return string.Join(", ", counts.Select(kv => $"{kv.Key}: {kv.Value}"));
    }

    // Method to simulate measurement of the quantum state
    public string Measure(List<int> qargs = null)
    {
        var probs = Probabilities(qargs);
        var sampledIndex = _rngGenerator.Next(probs.Length);
        var outcome = _IndexToKetArray(new[] { sampledIndex }, Dims(), true)[0];

        // Update state (collapsed state after measurement)
        var ret = Evolve(new Operator(), qargs); // Evolve with project operator for state collapse
        return outcome;
    }

    // Helper method to convert indices to ket array
    protected string[] _IndexToKetArray(int[] inds, int[] dims, bool stringLabels = false)
    {
        var shifts = new List<int> { 1 };
        foreach (var dim in dims.SkipLast(1))
            shifts.Add(shifts.Last() * dim);

        var kets = inds.Select(ind =>
        {
            return dims.Select((dim, i) => (ind / shifts[i]) % dim).ToArray();
        }).ToArray();

        if (stringLabels)
        {
            return kets.Select(ket => string.Join("|", ket)).ToArray();
        }

        return kets.Select(ket => string.Join(",", ket)).ToArray();
    }

    // Helper method to convert vector to dictionary-like format
    protected double[] _VectorToDict(double[] vec)
    {
        // Convert the vector to a dictionary-like format
        var nonZeroIndices = vec.Select((v, i) => new { v, i }).Where(x => x.v != 0).ToArray();
        return nonZeroIndices.Select(x => x.v).ToArray();
    }
}

// Define the operator class (minimal version for evolution and expectation value calculations)
public class Operator
{
    public Complex[,] Data { get; }

    public Operator(Complex[,] data)
    {
        Data = data;
    }
}

// Define the base operator class (can be expanded)
public class BaseOperator
{
    public Complex[,] Data { get; }

    public BaseOperator(Complex[,] data)
    {
        Data = data;
    }
}

// Define a complex number structure (if you don't use the built-in complex type)
public struct Complex
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // Conjugate of a complex number
    public static Complex Conjugate(Complex complex)
    {
        return new Complex(complex.Real, -complex.Imaginary);
    }

    // Multiply two complex numbers
    public static Complex operator *(Complex a, Complex b)
    {
        return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
    }

    // Addition of two complex numbers
    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    // Multiply a complex number by a scalar
    public static Complex operator *(Complex a, double scalar)
    {
        return new Complex(a.Real * scalar, a.Imaginary * scalar);
    }

    // Convert a complex number to a string
    public override string ToString()
    {
        return $"{Real} + {Imaginary}i";
    }
}

// Define the OpShape class (for dimensionality of the quantum state)
public class OpShape
{
    public int[] Shape { get; set; }
    public int NumQubits => (int)Math.Log2(Shape[0]);

    public OpShape(int[] shape)
    {
        Shape = shape;
    }

    public int[] DimsL()
    {
        return Shape;
    }
}
