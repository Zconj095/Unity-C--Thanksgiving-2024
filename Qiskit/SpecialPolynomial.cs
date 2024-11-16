using System;
using System.Collections.Generic;
using System.Linq;

public class SpecialPolynomial
{
    public int NumQubits { get; set; }
    public List<int> Weight_0 { get; set; } = new List<int>();
    public List<int> Weight_1 { get; set; } = new List<int>();
    public List<int> Weight_2 { get; set; } = new List<int>();
    public List<int> Weight_3 { get; set; } = new List<int>();

    public SpecialPolynomial(int numQubits)
    {
        NumQubits = numQubits;
    }

    public void SetPj(IEnumerable<int> support)
    {
        // Placeholder method to simulate the setting of terms
    }

    public void SetTerm(List<int> term, int value)
    {
        // Placeholder method to simulate setting terms in the polynomial
    }

    public int GetTerm(IEnumerable<int> term)
    {
        // Placeholder method to simulate getting a term's value
        return 0;
    }

    public SpecialPolynomial Evaluate(List<SpecialPolynomial> newVars)
    {
        // Placeholder method to evaluate and return the polynomial
        return new SpecialPolynomial(NumQubits);
    }
}

public class CNOTDihedral
{
    public int NumQubits { get; set; }
    public SpecialPolynomial Poly { get; set; }
    public int[,] Linear { get; set; }
    public int[] Shift { get; set; }

    private const double ATOL_DEFAULT = 1e-8;
    private const double RTOL_DEFAULT = 1e-5;

    public CNOTDihedral(int numQubits)
    {
        NumQubits = numQubits;
        Poly = new SpecialPolynomial(numQubits);
        Linear = new int[numQubits, numQubits];
        Shift = new int[numQubits];
        InitializeIdentity();
    }

    public CNOTDihedral(CNOTDihedral data)
    {
        NumQubits = data.NumQubits;
        Poly = data.Poly;
        Linear = data.Linear.Clone() as int[,];
        Shift = (int[])data.Shift.Clone();
    }

    public string Name => "cnotdihedral";
    public int NumClbits => 0;

    public void InitializeIdentity()
    {
        for (int i = 0; i < NumQubits; i++)
        {
            Shift[i] = 0;
            for (int j = 0; j < NumQubits; j++)
            {
                Linear[i, j] = i == j ? 1 : 0;
            }
        }
    }

    public CNOTDihedral Dot(CNOTDihedral other)
    {
        if (NumQubits != other.NumQubits)
            throw new InvalidOperationException("Multiplication on different number of qubits.");

        var result = new CNOTDihedral(NumQubits);
        result.Shift = Shift.Zip(other.Shift, (s, o) => (s + o) % 2).ToArray();
        result.Linear = Z2Matmul(Linear, other.Linear);

        // Handle polynomial combination
        var newVars = new List<SpecialPolynomial>();
        for (int i = 0; i < NumQubits; i++)
        {
            var support = Enumerable.Range(0, NumQubits).Where(idx => other.Linear[i, idx] != 0).ToArray();
            var poly = new SpecialPolynomial(NumQubits);
            poly.SetPj(support);
            newVars.Add(poly);
        }

        result.Poly = other.Poly.Evaluate(newVars) + Poly;
        return result;
    }

    public CNOTDihedral Compose(CNOTDihedral other)
    {
        return other.Dot(this);
    }

    public CNOTDihedral Tensor(CNOTDihedral other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        var result = new CNOTDihedral(NumQubits + other.NumQubits);
        // Compute tensor product of Linear and Shift
        result.Linear = new int[NumQubits + other.NumQubits, NumQubits + other.NumQubits];
        for (int i = 0; i < NumQubits; i++)
        {
            for (int j = 0; j < NumQubits; j++)
            {
                result.Linear[i, j] = Linear[i, j];
            }
        }

        for (int i = 0; i < other.NumQubits; i++)
        {
            for (int j = 0; j < other.NumQubits; j++)
            {
                result.Linear[NumQubits + i, NumQubits + j] = other.Linear[i, j];
            }
        }

        // Tensor Shift
        result.Shift = Shift.Concat(other.Shift).ToArray();
        return result;
    }

    private int[,] Z2Matmul(int[,] left, int[,] right)
    {
        int size = left.GetLength(0);
        var prod = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                prod[i, j] = (left.Cast<int>().Skip(i * size).Take(size).Zip(right.Cast<int>().Skip(j * size).Take(size), (a, b) => a * b).Sum()) % 2;
            }
        }
        return prod;
    }

    public override bool Equals(object obj)
    {
        return obj is CNOTDihedral other &&
               NumQubits == other.NumQubits &&
               Poly.Equals(other.Poly) &&
               Linear.Cast<int>().SequenceEqual(other.Linear.Cast<int>()) &&
               Shift.SequenceEqual(other.Shift);
    }

    public override string ToString()
    {
        return $"Phase polynomial = \n{Poly}\nAffine function = ({string.Join(",", Shift)})";
    }
}

