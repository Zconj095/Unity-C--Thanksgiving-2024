using System;
using System.Collections.Generic;
using UnityEngine;

public class PauliList
{
    private List<Pauli> paulis;
    private static int __truncate__ = 2000; // Max number of qubits before truncating

    public PauliList()
    {
        paulis = new List<Pauli>();
    }

    // Initialize from a list of strings (Pauli operator labels)
    public PauliList(List<string> data)
    {
        paulis = new List<Pauli>();
        foreach (var pauliStr in data)
        {
            paulis.Add(new Pauli(pauliStr));
        }
    }

    // Initialize from a single Pauli operator
    public PauliList(Pauli pauli)
    {
        paulis = new List<Pauli> { pauli };
    }

    // Initialize from a list of Pauli objects
    public PauliList(List<Pauli> data)
    {
        paulis = new List<Pauli>(data);
    }

    // Access individual Pauli operator by index
    public Pauli this[int index] => paulis[index];

    // Return the number of Paulis in the list
    public int Count => paulis.Count;

    // Convert the PauliList to a string representation
    public override string ToString()
    {
        string result = "PauliList [ ";
        foreach (var pauli in paulis)
        {
            result += pauli.ToString() + " ";
        }
        return result + "]";
    }

    // Represent PauliList as a truncated string for displaying
    private string _truncatedStr(bool showClass)
    {
        int stop = paulis.Count;
        if (__truncate__ > 0 && paulis.Count > __truncate__)
        {
            stop = __truncate__;
        }
        string result = showClass ? "PauliList(" : "";
        result += "[";
        for (int i = 0; i < stop; i++)
        {
            result += paulis[i].ToString() + ", ";
        }
        result = result.TrimEnd(',', ' ') + "]";
        result += showClass ? ")" : "";
        return result;
    }

    // Equality check for PauliList
    public override bool Equals(object obj)
    {
        if (obj is PauliList other)
        {
            if (paulis.Count != other.paulis.Count)
                return false;

            for (int i = 0; i < paulis.Count; i++)
            {
                if (!paulis[i].Equals(other.paulis[i]))
                    return false;
            }
            return true;
        }
        return false;
    }

    // Adding two PauliLists together
    public static PauliList operator +(PauliList list1, PauliList list2)
    {
        List<Pauli> combinedList = new List<Pauli>(list1.paulis);
        combinedList.AddRange(list2.paulis);
        return new PauliList(combinedList);
    }

    // Sorting by weight or lexicographically
    public void Sort(bool weight = false, bool phase = false)
    {
        if (weight)
        {
            paulis.Sort((a, b) => a.Weight.CompareTo(b.Weight));
        }
        else
        {
            paulis.Sort((a, b) => a.CompareTo(b));
        }
    }

    // Remove a Pauli operator from the list by index
    public void RemoveAt(int index)
    {
        paulis.RemoveAt(index);
    }

    // Insert a Pauli operator at a specific index
    public void Insert(int index, Pauli pauli)
    {
        paulis.Insert(index, pauli);
    }

    // Returns a new PauliList with the Paulis in the list that commute with 'other'
    public PauliList CommutingWith(PauliList other)
    {
        List<Pauli> commutingPaulis = new List<Pauli>();
        foreach (var pauli in paulis)
        {
            foreach (var otherPauli in other.paulis)
            {
                if (pauli.CommutesWith(otherPauli))
                {
                    commutingPaulis.Add(pauli);
                }
            }
        }
        return new PauliList(commutingPaulis);
    }

    // Create a PauliList from symplectic representation
    public static PauliList FromSymplectic(int[,] z, int[,] x, int[] phase)
    {
        List<Pauli> paulis = new List<Pauli>();
        for (int i = 0; i < z.GetLength(0); i++)
        {
            paulis.Add(new Pauli(z.GetRow(i), x.GetRow(i), phase[i]));
        }
        return new PauliList(paulis);
    }
}

public static class ArrayExtensions
{
    public static int[] GetRow(this int[,] array, int row)
    {
        int colCount = array.GetLength(1);
        int[] result = new int[colCount];
        for (int col = 0; col < colCount; col++)
        {
            result[col] = array[row, col];
        }
        return result;
    }
}
