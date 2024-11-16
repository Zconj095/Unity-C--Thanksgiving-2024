using System;
using System.Collections.Generic;
using System.Linq;

public class Z2Symmetries
{
    public List<int[]> Symmetries { get; private set; }
    public List<int[]> SqPaulis { get; private set; }
    public List<int> SqList { get; private set; }
    public List<int> TaperingValues { get; private set; }
    public double Tolerance { get; private set; }

    public Z2Symmetries(
        IEnumerable<int[]> symmetries,
        IEnumerable<int[]> sqPaulis,
        IEnumerable<int> sqList,
        IEnumerable<int> taperingValues = null,
        double tol = 1e-14)
    {
        Symmetries = symmetries.ToList();
        SqPaulis = sqPaulis.ToList();
        SqList = sqList.ToList();
        TaperingValues = taperingValues?.ToList();
        Tolerance = tol;

        // Validate lengths
        if (Symmetries.Count != SqPaulis.Count)
            throw new ArgumentException("Number of Z2 symmetries must match number of single-qubit paulis.");
        if (SqPaulis.Count != SqList.Count)
            throw new ArgumentException("Number of single-qubit paulis must match length of the sq_list.");
        if (TaperingValues != null && TaperingValues.Count != SqList.Count)
            throw new ArgumentException("Length of the single-qubit list must match the tapering values.");
    }

    // Generate Clifford operators based on symmetries and single-qubit Pauli operators.
    public List<string> Cliffords
    {
        get
        {
            var cliffords = new List<string>();
            for (int i = 0; i < Symmetries.Count; i++)
            {
                string pauliSymm = string.Join(",", Symmetries[i]);
                string sqPauli = string.Join(",", SqPaulis[i]);
                cliffords.Add($"Clifford: ({pauliSymm} + {sqPauli}) / sqrt(2)");
            }
            return cliffords;
        }
    }

    // Check if the symmetries object is empty
    public bool IsEmpty()
    {
        return !Symmetries.Any() || !SqPaulis.Any() || !SqList.Any();
    }

    // Find Z2 symmetries of an operator (simplified to generate dummy values for the example)
    public static Z2Symmetries FindZ2Symmetries()
    {
        var symmetries = new List<int[]> { new int[] { 1, 0 }, new int[] { 0, 1 } };
        var sqPaulis = new List<int[]> { new int[] { 1, 1 }, new int[] { 0, 0 } };
        var sqList = new List<int> { 0, 1 };

        return new Z2Symmetries(symmetries, sqPaulis, sqList);
    }

    // Convert operator using Clifford operators
    public List<string> ConvertClifford(List<string> operatorOp)
    {
        if (!IsEmpty())
        {
            foreach (var clifford in Cliffords)
            {
                operatorOp = operatorOp.Select(op => $"{clifford} @ {op} @ {clifford}").ToList();
            }
        }
        return operatorOp;
    }

    // Taper the operator by reducing qubits based on symmetries
    public object TaperClifford(List<string> operatorOp)
    {
        if (IsEmpty())
        {
            return operatorOp;
        }

        if (TaperingValues == null)
        {
            var taperedOps = Enumerable.Range(0, (int)Math.Pow(2, SqList.Count))
                .Select(i => Taper(operatorOp, GetTaperingValues(i)))
                .ToList();
            return taperedOps;
        }
        else
        {
            return Taper(operatorOp, TaperingValues);
        }
    }

    // Apply tapering to an operator
    private List<string> Taper(List<string> operatorOp, List<int> taperingValues)
    {
        var taperedOps = new List<string>();
        foreach (var op in operatorOp)
        {
            var newOp = op;
            for (int idx = 0; idx < SqList.Count; idx++)
            {
                newOp = $"{newOp} * {taperingValues[idx]}"; // Apply tapering values (simplified logic)
            }
            taperedOps.Add(newOp);
        }
        return taperedOps;
    }

    // Generate tapering values from a number (simplified)
    private List<int> GetTaperingValues(int i)
    {
        return Enumerable.Range(0, SqList.Count)
            .Select(j => (i >> j) & 1 == 0 ? 1 : -1)
            .ToList();
    }

    // Equality comparison for Z2Symmetries objects
    public bool Equals(Z2Symmetries other)
    {
        if (other == null) return false;

        return Symmetries.SequenceEqual(other.Symmetries) &&
               SqPaulis.SequenceEqual(other.SqPaulis) &&
               SqList.SequenceEqual(other.SqList) &&
               EqualityComparer<List<int>>.Default.Equals(TaperingValues, other.TaperingValues);
    }

    public override string ToString()
    {
        var ret = new List<string> { "Z2 Symmetries:" };
        ret.Add("Symmetries:");
        foreach (var symmetry in Symmetries)
            ret.Add(string.Join(",", symmetry));

        ret.Add("Single-Qubit Pauli X:");
        foreach (var sqPauli in SqPaulis)
            ret.Add(string.Join(",", sqPauli));

        ret.Add("Cliffords:");
        foreach (var clifford in Cliffords)
            ret.Add(clifford);

        ret.Add("Qubit Index:");
        ret.Add(string.Join(",", SqList));

        ret.Add("Tapering values:");
        if (TaperingValues == null)
        {
            ret.Add("  - Possible values: " + string.Join(", ", 
                Enumerable.Range(0, (int)Math.Pow(2, SqList.Count))
                .Select(i => string.Join(",", GetTaperingValues(i)))
            ));
        }
        else
        {
            ret.Add(string.Join(", ", TaperingValues));
        }

        return string.Join("\n", ret);
    }
}
