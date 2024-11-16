using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Clifford : BaseOperator
{
    private bool[,] tableau;
    private int numQubits;

    private static Dictionary<string, Func<Clifford, Clifford, Clifford>> composeLookup;
    private static Dictionary<string, Func<Clifford, Clifford, Clifford>> compose1QLookup;

    public Clifford(bool[,] data)
    {
        // Initialize from a 2N x (2N + 1) boolean tableau representation
        numQubits = data.GetLength(0) / 2;
        tableau = data;
    }

    public Clifford(int numQubits)
    {
        this.numQubits = numQubits;
        tableau = new bool[2 * numQubits, 2 * numQubits + 1];
    }

    public static Clifford Identity(int numQubits)
    {
        // Return the identity Clifford for N qubits
        var identityTableau = new bool[2 * numQubits, 2 * numQubits + 1];
        for (int i = 0; i < numQubits; i++)
        {
            identityTableau[i, numQubits + i] = true;
            identityTableau[numQubits + i, numQubits + i] = true;
        }
        return new Clifford(identityTableau);
    }

    public string Name => "Clifford";

    public int NumClbits => 0;

    public override string ToString()
    {
        return $"Clifford: Stabilizer = {string.Join(", ", ToLabels("S"))}, Destabilizer = {string.Join(", ", ToLabels("D"))}";
    }

    // Return the stabilizer (S) or destabilizer (D) rows of the tableau
    public List<string> ToLabels(string mode = "B")
    {
        var size = mode == "B" ? 2 * numQubits : numQubits;
        var offset = mode == "S" ? numQubits : 0;
        var result = new List<string>();

        for (int i = 0; i < size; i++)
        {
            var label = new char[numQubits];
            for (int j = 0; j < numQubits; j++)
            {
                if (tableau[i + offset, j]) label[j] = 'X';
                else if (tableau[i + offset, numQubits + j]) label[j] = 'Z';
                else label[j] = 'I';
            }
            result.Add(new string(label));
        }
        return result;
    }

    // Compose two Clifford operators
    public Clifford Compose(Clifford other, bool front = false)
    {
        if (front)
        {
            return ComposeGeneral(this, other);
        }
        return ComposeGeneral(other, this);
    }

    // Helper to compose two Clifford operators
    private Clifford ComposeGeneral(Clifford left, Clifford right)
    {
        var newTableau = new bool[2 * left.numQubits, 2 * right.numQubits + 1];
        var phase = new bool[2 * right.numQubits];

        for (int i = 0; i < left.numQubits; i++)
        {
            for (int j = 0; j < right.numQubits; j++)
            {
                newTableau[i, j] = left.tableau[i, j] && right.tableau[i, j];
                phase[i] = phase[i] || left.tableau[i, j];
            }
        }
        return new Clifford(newTableau);
    }

    // Check if the Clifford operator is unitary
    public bool IsUnitary()
    {
        return true;  // A Clifford is always unitary.
    }

    public Clifford Conjugate()
    {
        return Transpose();
    }

    public Clifford Transpose()
    {
        var newTableau = (bool[,])tableau.Clone();
        for (int i = 0; i < numQubits; i++)
        {
            for (int j = 0; j < numQubits; j++)
            {
                newTableau[i, j] = tableau[j, i];
                newTableau[j, i] = tableau[i, j];
            }
        }
        return new Clifford(newTableau);
    }

    // Return the symplectic matrix (X, Z) part of the tableau
    public bool[,] SymplecticMatrix => tableau.Cast<bool>().Take(numQubits * numQubits).ToArray();

    // Return the phase part of the tableau
    public bool[] Phase => tableau.Cast<bool>().Skip(numQubits * numQubits).ToArray();

    // Helper for composing operations on single qubits
    private Clifford Compose1Q(Clifford left, Clifford right)
    {
        if (compose1QLookup == null)
        {
            // Initialize with some predefined operations
            compose1QLookup = new Dictionary<string, Func<Clifford, Clifford, Clifford>>();
            compose1QLookup["X"] = (left, right) => new Clifford(new bool[2 * numQubits, 2 * numQubits + 1]);
        }
        return compose1QLookup["X"](left, right);
    }

    // Static helper for table manipulations
    private static bool[,] _StackTablePhase(bool[,] table, bool[] phase)
    {
        var newTable = new bool[table.GetLength(0), table.GetLength(1)];
        for (int i = 0; i < table.GetLength(0); i++)
        {
            newTable[i, table.GetLength(1) - 1] = phase[i];
        }
        return newTable;
    }
}

