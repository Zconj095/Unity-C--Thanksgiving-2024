using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BasePauli
{
    private static readonly Complex[] _PARITY = Enumerable.Range(0, 256).Select(i => (bin(i).Count("1") % 2 == 0) ? 1 : -1).ToArray();
    private static readonly byte[] _TO_LABEL_CHARS = { (byte)'I', (byte)'X', (byte)'Z', (byte)'Y' };

    public int NumPaulis { get; private set; }
    public int NumQubits { get; private set; }
    private bool[,] _z;
    private bool[,] _x;
    private int[] _phase;

    public BasePauli(bool[,] z, bool[,] x, int[] phase)
    {
        _z = z;
        _x = x;
        _phase = phase;
        NumPaulis = z.GetLength(0);
        NumQubits = z.GetLength(1);
    }

    public BasePauli Copy()
    {
        var newZ = (bool[,])_z.Clone();
        var newX = (bool[,])_x.Clone();
        var newPhase = (int[])_phase.Clone();
        return new BasePauli(newZ, newX, newPhase);
    }

    public BasePauli Tensor(BasePauli other)
    {
        return _Tensor(this, other);
    }

    public BasePauli Compose(BasePauli other, List<int> qargs = null, bool front = false, bool inplace = false)
    {
        // Validation
        if (qargs == null && other.NumQubits != this.NumQubits)
            throw new Exception("Qubits mismatch between BasePauli objects");

        var x1 = this._x;
        var z1 = this._z;
        var x2 = other._x;
        var z2 = other._z;

        int[] newPhase = (int[])this._phase.Clone();
        if (front)
            newPhase = newPhase.Concat(_CountY(x1, z2)).ToArray();
        else
            newPhase = newPhase.Concat(_CountY(x2, z1)).ToArray();

        bool[,] resultX = new bool[NumPaulis, NumQubits];
        bool[,] resultZ = new bool[NumPaulis, NumQubits];
        for (int i = 0; i < NumPaulis; i++)
        {
            for (int j = 0; j < NumQubits; j++)
            {
                resultX[i, j] = x1[i, j] ^ x2[i, j];
                resultZ[i, j] = z1[i, j] ^ z2[i, j];
            }
        }

        return new BasePauli(resultZ, resultX, newPhase);
    }

    public BasePauli Multiply(int[] phase)
    {
        // Multiply with a set of phases (complex coefficients)
        int[] newPhase = new int[phase.Length];
        for (int i = 0; i < phase.Length; i++)
        {
            newPhase[i] = (this._phase[i] + phase[i]) % 4;
        }

        return new BasePauli(this._z, this._x, newPhase);
    }

    public BasePauli Conjugate()
    {
        // Complex conjugate with respect to Z basis
        var newPhase = this._phase.Select(p => (p % 2) == 0 ? p : (p + 2) % 4).ToArray();
        return new BasePauli(this._z, this._x, newPhase);
    }

    public BasePauli Transpose()
    {
        // Transpose effect on Y
        var newPhase = _CountY(this._x, this._z);
        for (int i = 0; i < newPhase.Length; i++)
        {
            this._phase[i] = (this._phase[i] + newPhase[i]) % 4;
        }

        return new BasePauli(this._z, this._x, this._phase);
    }

    // Helper methods
    private static int[] _CountY(bool[,] x, bool[,] z)
    {
        int[] result = new int[x.GetLength(0)];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = (x.Cast<bool>().Sum(val => Convert.ToInt32(val)) + z.Cast<bool>().Sum(val => Convert.ToInt32(val))) % 2;
        }
        return result;
    }

    private static BasePauli _Tensor(BasePauli a, BasePauli b)
    {
        bool[,] newX = new bool[a.NumPaulis * b.NumPaulis, a.NumQubits];
        bool[,] newZ = new bool[a.NumPaulis * b.NumPaulis, a.NumQubits];
        int[] newPhase = new int[a.NumPaulis * b.NumPaulis];

        // Perform tensor operation
        return new BasePauli(newZ, newX, newPhase);
    }

    // Convert to matrix and label helpers
    public static string ToLabel(bool[,] z, bool[,] x, int[] phase)
    {
        // Convert the Pauli to a string label
        var label = new List<char>();
        for (int i = 0; i < z.GetLength(1); i++)
        {
            int index = (Convert.ToInt32(z[i, i]) << 1) + Convert.ToInt32(x[i, i]);
            label.Add((char)_TO_LABEL_CHARS[index]);
        }

        return new string(label.ToArray());
    }
    
    // Helper to generate phase of complex numbers
    public static int _PhaseFromComplex(int coeff)
    {
        if (coeff == 1) return 0;
        if (coeff == -1) return 2;
        if (coeff == 1) return 3;
        if (coeff == -1) return 1;

        throw new Exception("Invalid phase");
    }
}

