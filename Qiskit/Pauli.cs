using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Pauli : BasePauli
{
    // Set max string truncation size
    public static int MaxTruncate = 50;

    // Define regex pattern for valid Pauli labels
    private static readonly Regex _validLabelPattern = new Regex(@"(?<coeff>[+-]?1?[ij]?)(?<pauli>[IXYZ]*)");
    
    private static readonly Dictionary<string, Func<Pauli, int, Pauli>> _basis1Q = new Dictionary<string, Func<Pauli, int, Pauli>>()
    {
        { "x", ApplyX },
        { "y", ApplyY },
        { "z", ApplyZ },
        { "h", ApplyH },
        { "s", ApplyS },
        { "sdg", ApplySDG },
        { "sx", ApplySX },
        { "sxdg", ApplySXDG },
        { "v", ApplyV },
        { "w", ApplyW }
    };

    private static readonly Dictionary<string, Func<Pauli, int, int, Pauli>> _basis2Q = new Dictionary<string, Func<Pauli, int, int, Pauli>>()
    {
        { "cx", ApplyCX },
        { "cz", ApplyCZ },
        { "cy", ApplyCY },
        { "swap", ApplySwap },
        { "iswap", ApplyISwap },
        { "ecr", ApplyECR },
        { "dcx", ApplyDCX }
    };

    private static readonly HashSet<string> _nonClifford = new HashSet<string> { "t", "tdg", "ccx", "ccz" };

    // Constructor for Pauli from a label (string)
    public Pauli(string data)
    {
        var (baseZ, baseX, basePhase) = FromLabel(data);
        Initialize(baseZ, baseX, basePhase);
    }

    // Constructor for Pauli from a tuple of arrays (z, x, phase)
    public Pauli(Tuple<bool[], bool[], int> data)
    {
        var (baseZ, baseX, basePhase) = data;
        Initialize(baseZ, baseX, basePhase);
    }

    // Constructor to clone another Pauli object
    public Pauli(Pauli other) : base(other)
    {
    }

    // Helper function to initialize the Pauli object
    private void Initialize(bool[] baseZ, bool[] baseX, int basePhase)
    {
        _z = new bool[1, baseZ.Length];
        _x = new bool[1, baseX.Length];
        _phase = new int[1] { basePhase };

        // Use full index syntax for assignment
        for (int i = 0; i < baseZ.Length; i++)
        {
            _z[0, i] = baseZ[i];
            _x[0, i] = baseX[i];
        }
    }


    // Pauli string truncation based on MaxTruncate length
    public override string ToString()
    {
        if (num_qubits > MaxTruncate)
        {
            string front = ToLabel().Substring(ToLabel().Length - MaxTruncate);
            return front + "...";
        }
        return ToLabel();
    }

    // Set truncation length for the Pauli string representation
    public static void SetTruncation(int val)
    {
        MaxTruncate = val;
    }

    // Equality check for Pauli objects
    public bool Equals(Pauli other)
    {
        if (other == null) return false;
        return _z.SequenceEqual(other._z) && _x.SequenceEqual(other._x);
    }

    // Equivalence check (up to group phase)
    public bool IsEquivalent(Pauli other)
    {
        if (other == null) return false;
        return _z.SequenceEqual(other._z) && _x.SequenceEqual(other._x);
    }

    // Property for phase in group phase
    public int Phase
    {
        get
        {
            return (_phase[0] - CountY(_phase[0])) % 4;
        }
        set
        {
            _phase[0] = (value + CountY(_phase[0])) % 4;
        }
    }

    // Accessor and mutator for the X vector
    public int[] X
    {
        get => _x[0];
        set => _x[0] = value;
    }

    // Accessor and mutator for the Z vector
    public int[] Z
    {
        get => _z[0];
        set => _z[0] = value;
    }

    // Helper method to count the Y terms for phase calculation
    private int CountY(int phase)
    {
        return _x.Cast<bool>().Count(val => val);
    }

    // Create a copy of the current Pauli object
    public Pauli Copy()
    {
        return new Pauli(this);
    }

    // Convert the Pauli to its string label representation
    public string ToLabel()
    {
        return ToLabel(_z, _x, _phase[0]);
    }

    // Internal method to convert from a string label
    public static Tuple<bool[], bool[], int> FromLabel(string label)
    {
        var match = _validLabelPattern.Match(label);
        if (!match.Success)
            throw new Exception($"Invalid Pauli string label: {label}");

        var phase = (match.Groups["coeff"].Value == "") ? 0 : (match.Groups["coeff"].Value == "-i") ? 1 : (match.Groups["coeff"].Value == "-") ? 2 : 3;
        var pauliString = match.Groups["pauli"].Value;
        bool[] baseX = new bool[pauliString.Length];
        bool[] baseZ = new bool[pauliString.Length];

        for (int i = 0; i < pauliString.Length; i++)
        {
            char c = pauliString[i];
            if (c == 'X') { baseX[i] = true; }
            if (c == 'Z') { baseZ[i] = true; }
        }

        return new Tuple<bool[], bool[], int>(baseZ, baseX, phase);
    }

    // Apply the X operator
    public static Pauli ApplyX(Pauli pauli, int qubit)
    {
        pauli.Phase ^= pauli.Z[qubit];
        return pauli;
    }

    // Apply the Y operator
    public static Pauli ApplyY(Pauli pauli, int qubit)
    {
        var x = pauli.X[qubit];
        var z = pauli.Z[qubit];
        pauli.Phase ^= x ^ z;
        return pauli;
    }

    // Apply the Z operator
    public static Pauli ApplyZ(Pauli pauli, int qubit)
    {
        pauli.Phase ^= pauli.X[qubit];
        return pauli;
    }

    // Apply the Hadamard operator
    public static Pauli ApplyH(Pauli pauli, int qubit)
    {
        var x = pauli.X[qubit];
        var z = pauli.Z[qubit];
        pauli.Phase ^= x & z;

        var temp = x;
        pauli.X[qubit] = z;
        pauli.Z[qubit] = temp;

        return pauli;
    }

    // Apply the S operator
    public static Pauli ApplyS(Pauli pauli, int qubit)
    {
        var x = pauli.X[qubit];
        var z = pauli.Z[qubit];
        pauli.Phase ^= x & z;
        pauli.Z[qubit] ^= x;
        return pauli;
    }

    // Apply the S† operator
    public static Pauli ApplySDG(Pauli pauli, int qubit)
    {
        var x = pauli.X[qubit];
        var z = pauli.Z[qubit];
        pauli.Phase ^= x & ~z;
        pauli.Z[qubit] ^= x;
        return pauli;
    }

    // Apply the SX operator
    public static Pauli ApplySX(Pauli pauli, int qubit)
    {
        var x = pauli.X[qubit];
        var z = pauli.Z[qubit];

        pauli.Phase ^= ~x & z;
        x ^= z;
        return pauli;
    }

    // Apply the SX† operator
    public static Pauli ApplySXDG(Pauli pauli, int qubit)
    {
        var x = pauli.X[qubit];
        var z = pauli.Z[qubit];

        pauli.Phase ^= x & z;
        x ^= z;
        return pauli;
    }

    // Apply the V operator
    public static Pauli ApplyV(Pauli pauli, int qubit)
    {
        var x = pauli.X[qubit];
        var z = pauli.Z[qubit];
        var temp = x;
        x ^= z;
        z = temp;
        return pauli;
    }

    // Apply the W operator
    public static Pauli ApplyW(Pauli pauli, int qubit)
    {
        var x = pauli.X[qubit];
        var z = pauli.Z[qubit];
        var temp = z;
        z ^= x;
        x = temp;
        return pauli;
    }

        // Apply the CX (CNOT) gate
    public static Pauli ApplyCX(Pauli pauli, int control, int target)
    {
        var x0 = pauli.X[control];
        var z0 = pauli.Z[control];
        var x1 = pauli.X[target];
        var z1 = pauli.Z[target];

        pauli.Phase ^= (x1 ^ z0 ^ true) & z1 & x0;
        x1 ^= x0;
        z0 ^= z1;

        return pauli;
    }

    // Apply the CZ gate
    public static Pauli ApplyCZ(Pauli pauli, int control, int target)
    {
        var x0 = pauli.X[control];
        var z0 = pauli.Z[control];
        var x1 = pauli.X[target];
        var z1 = pauli.Z[target];

        pauli.Phase ^= x0 & x1 & (z0 ^ z1);
        z1 ^= x0;
        z0 ^= x1;

        return pauli;
    }

    // Apply the CY gate
    public static Pauli ApplyCY(Pauli pauli, int control, int target)
    {
        pauli = ApplySDG(pauli, target);
        pauli = ApplyCX(pauli, control, target);
        pauli = ApplyS(pauli, target);
        return pauli;
    }

    // Apply the Swap gate
    public static Pauli ApplySwap(Pauli pauli, int qubit0, int qubit1)
    {
        var tempX = pauli.X[qubit0];
        pauli.X[qubit0] = pauli.X[qubit1];
        pauli.X[qubit1] = tempX;

        var tempZ = pauli.Z[qubit0];
        pauli.Z[qubit0] = pauli.Z[qubit1];
        pauli.Z[qubit1] = tempZ;

        return pauli;
    }

    // Apply the iSwap gate
    public static Pauli ApplyISwap(Pauli pauli, int qubit0, int qubit1)
    {
        pauli = ApplyS(pauli, qubit0);
        pauli = ApplyH(pauli, qubit0);
        pauli = ApplyS(pauli, qubit1);
        pauli = ApplyCX(pauli, qubit0, qubit1);
        pauli = ApplyCX(pauli, qubit1, qubit0);
        pauli = ApplyH(pauli, qubit1);
        return pauli;
    }

    // Apply the ECR gate
    public static Pauli ApplyECR(Pauli pauli, int qubit0, int qubit1)
    {
        pauli = ApplyS(pauli, qubit0);
        pauli = ApplySX(pauli, qubit1);
        pauli = ApplyCX(pauli, qubit0, qubit1);
        pauli = ApplyX(pauli, qubit0);
        return pauli;
    }

    // Apply the DCX gate
    public static Pauli ApplyDCX(Pauli pauli, int qubit0, int qubit1)
    {
        pauli = ApplyCX(pauli, qubit0, qubit1);
        pauli = ApplyCX(pauli, qubit1, qubit0);
        return pauli;
    }

        // Compose two Pauli operators
    public Pauli Compose(Pauli other, List<int> qargs = null, bool front = false, bool inplace = false)
    {
        if (qargs == null)
            qargs = other.qargs ?? new List<int>();

        if (!front)
            return ComposePauli(this, other, qargs);
        return ComposePauli(other, this, qargs);
    }

    // Utility method for Pauli composition
    public Pauli ComposePauli(Pauli first, Pauli second, List<int> qargs)
    {
        // Check if qargs match or need to be expanded
        if (qargs.Count != first.num_qubits)
            throw new Exception("Incompatible qubit count");

        // Perform the actual composition
        return new Pauli(ApplyCX(first, qargs[0], qargs[1])); // Example for CX operator
    }

    // Tensor product of two Pauli operators
    public Pauli Tensor(Pauli other)
    {
        return new Pauli(_z.Concat(other._z).ToArray(), _x.Concat(other._x).ToArray(), _phase[0]);
    }

    // Expand Pauli
    public Pauli Expand(Pauli other)
    {
        return Tensor(other);
    }

    // Multiply two Pauli operators
    public Pauli Multiply(Pauli other)
    {
        return new Pauli(_z.Concat(other._z).ToArray(), _x.Concat(other._x).ToArray(), _phase[0]);
    }

    // Compute conjugate of Pauli
    public Pauli Conjugate()
    {
        return this;
    }

    // Compute transpose of Pauli
    public Pauli Transpose()
    {
        return this;
    }

    // Compute adjoint of Pauli
    public Pauli Adjoint()
    {
        return this;
    }

    // Compute inverse of Pauli
    public Pauli Inverse()
    {
        return Adjoint();
    }

    // Check commutation with other Pauli operator
    public bool Commutes(Pauli other, List<int> qargs = null)
    {
        return Equals(other);  // Placeholder logic for commutation
    }

    // Check if Pauli anticommutes with other Pauli operator
    public bool Anticommutates(Pauli other)
    {
        return !Commutes(other);
    }
}

