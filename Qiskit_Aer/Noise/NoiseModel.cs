using UnityEngine;
using System.Collections.Generic;

// Custom Complex number struct
public struct ComplexNumber
{
    public float Real;
    public float Imaginary;

    public ComplexNumber(float real, float imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // Overriding ToString for debugging purposes
    public override string ToString()
    {
        return string.Format("({0} + {1}i)", Real, Imaginary);
    }

    // Implement additional methods if necessary
}

// Logger class using Unity's Debug.Log
public static class Logger
{
    public static void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }
}

// Custom exception class for noise-related errors
public class NoiseError : System.Exception
{
    public NoiseError(string message) : base(message) { }
}

// Base class for quantum errors
public abstract class NoiseBaseQuantumError
{
    // Mark NumQubits with a protected setter so derived classes can override it
    public virtual int NumQubits { get; protected set; }

    public abstract bool IsIdeal();
    public abstract NoiseBaseQuantumError Compose(NoiseBaseQuantumError other);
}


// Quantum error class
public class NoiseQuantumError : NoiseBaseQuantumError
{
    // Override the NumQubits property with a protected setter
    public override int NumQubits { get; protected set; }

    public NoiseQuantumError(object error)
    {
        // Initialize based on the error object
        // Placeholder implementation
        NumQubits = 1; // Set appropriately based on the error object
    }

    public override bool IsIdeal()
    {
        // Placeholder implementation
        return false;
    }

    public override NoiseBaseQuantumError Compose(NoiseBaseQuantumError other)
    {
        // Placeholder for composing quantum errors
        return this; // Adjust as needed
    }
}

// Readout error class
public class NoiseReadoutError
{
    public int NumberOfQubits { get; private set; }

    public NoiseReadoutError(object probabilities)
    {
        // Initialize based on the probabilities
        // Placeholder implementation
        NumberOfQubits = 1; // Set appropriately based on the probabilities
    }

    public bool IsIdeal()
    {
        // Placeholder implementation
        return false;
    }
}

// NoiseInstruction class representing quantum instructions
public class NoiseInstruction
{
    public string Name { get; private set; }
    public string Label { get; private set; }

    public NoiseInstruction(string name, string label = null)
    {
        Name = name;
        Label = label ?? name;
    }
}

// The main NoiseModel class
public class NoiseModel
{
    // Static arrays for instruction types
    private static string[] _1qubit_instructions = new string[]
    {
        "u1", "u2", "u3", "u", "p", "r", "rx", "ry", "rz", "id",
        "x", "y", "z", "h", "s", "sdg", "sx", "sxdg", "t", "tdg",
    };

    private static string[] _2qubit_instructions = new string[]
    {
        "swap", "cx", "cy", "cz", "csx", "cp", "cu", "cu1",
        "cu2", "cu3", "rxx", "ryy", "rzz", "rzx", "ecr",
    };

    private static string[] _3qubit_instructions = new string[]
    {
        "ccx", "cswap"
    };

    // Instance variables
    private HashSet<string> _basis_gates;
    private HashSet<string> _noise_instructions;
    private HashSet<int> _noise_qubits;
    private Dictionary<string, NoiseQuantumError> _default_quantum_errors;
    private Dictionary<string, Dictionary<string, NoiseQuantumError>> _local_quantum_errors;
    private NoiseReadoutError _default_readout_error;
    private Dictionary<string, NoiseReadoutError> _local_readout_errors;
    private List<object> _custom_noise_passes;

    // Constructor
    public NoiseModel(List<string> basis_gates = null)
    {
        if (basis_gates == null)
        {
            // Default basis gates are id, rz, sx, cx
            _basis_gates = new HashSet<string> { "id", "rz", "sx", "cx" };
        }
        else
        {
            _basis_gates = new HashSet<string>();
            foreach (var nameLabel in _InstructionNamesLabels(basis_gates))
            {
                string name = nameLabel[0];
                _basis_gates.Add(name);
            }
        }

        // Initialize other variables
        _noise_instructions = new HashSet<string>();
        _noise_qubits = new HashSet<int>();
        _default_quantum_errors = new Dictionary<string, NoiseQuantumError>();
        _local_quantum_errors = new Dictionary<string, Dictionary<string, NoiseQuantumError>>();
        _default_readout_error = null;
        _local_readout_errors = new Dictionary<string, NoiseReadoutError>();
        _custom_noise_passes = new List<object>();
    }

    // Property to get basis gates
    public List<string> BasisGates
    {
        get { return new List<string>(_basis_gates); }
    }

    // Property to get noise instructions
    public List<string> NoiseInstructions
    {
        get { return new List<string>(_noise_instructions); }
    }

    // Property to get noise qubits
    public List<int> NoiseQubits
    {
        get { return new List<int>(_noise_qubits); }
    }

    // Method to check if the noise model is ideal
    public bool IsIdeal()
    {
        if (_default_quantum_errors.Count > 0)
            return false;
        if (_default_readout_error != null)
            return false;
        if (_local_quantum_errors.Count > 0)
            return false;
        if (_local_readout_errors.Count > 0)
            return false;
        if (_custom_noise_passes.Count > 0)
            return false;
        return true;
    }

    // Method to add basis gates
    public void AddBasisGates(List<string> instructions)
    {
        foreach (var nameLabel in _InstructionNamesLabels(instructions))
        {
            string name = nameLabel[0];
            // Add to basis gates if necessary
            _basis_gates.Add(name);
        }
    }

    // Method to add an all-qubit quantum error
    public void AddAllQubitQuantumError(NoiseQuantumError error, List<string> instructions, bool warnings = true)
    {
        if (error.IsIdeal())
            return;

        foreach (var nameLabel in _InstructionNamesLabels(instructions))
        {
            string name = nameLabel[0];
            string label = nameLabel[1];
            _CheckNumberOfQubits(error, name);
            if (_default_quantum_errors.ContainsKey(label))
            {
                NoiseQuantumError newError = (NoiseQuantumError)_default_quantum_errors[label].Compose(error);
                _default_quantum_errors[label] = newError;
                if (warnings)
                {
                    Logger.LogWarning(string.Format(
                        "WARNING: all-qubit error already exists for instruction \"{0}\", composing with additional error.",
                        label));
                }
            }
            else
            {
                _default_quantum_errors[label] = error;
            }

            if (_local_quantum_errors.ContainsKey(label))
            {
                string localQubits = _KeysToString(_local_quantum_errors[label].Keys);
                if (warnings)
                {
                    Logger.LogWarning(string.Format(
                        "WARNING: all-qubit error for instruction \"{0}\" will not apply to qubits: {1} as specific error already exists.",
                        label, localQubits));
                }
            }
            _noise_instructions.Add(label);
            AddBasisGates(new List<string> { name });
        }
    }

    // Method to add a quantum error
    public void AddQuantumError(NoiseQuantumError error, List<string> instructions, List<int> qubits, bool warnings = true)
    {
        if (error.IsIdeal())
            return;

        foreach (var qubit in qubits)
        {
            _noise_qubits.Add(qubit);
        }

        foreach (var nameLabel in _InstructionNamesLabels(instructions))
        {
            string name = nameLabel[0];
            string label = nameLabel[1];
            _CheckNumberOfQubits(error, name);

            if (!_local_quantum_errors.ContainsKey(label))
            {
                _local_quantum_errors[label] = new Dictionary<string, NoiseQuantumError>();
            }

            string qubitsKey = string.Join(",", qubits.ConvertAll(q => q.ToString()).ToArray());
            if (error.NumQubits != qubits.Count)
            {
                throw new NoiseError(string.Format(
                    "Number of qubits ({0}) does not match the error size ({1})",
                    qubits.Count, error.NumQubits));
            }

            if (_local_quantum_errors[label].ContainsKey(qubitsKey))
            {
                NoiseQuantumError newError = (NoiseQuantumError)_local_quantum_errors[label][qubitsKey].Compose(error);
                _local_quantum_errors[label][qubitsKey] = newError;
                if (warnings)
                {
                    Logger.LogWarning(string.Format(
                        "WARNING: quantum error already exists for instruction \"{0}\" on qubits {1}, appending additional error.",
                        label, qubitsKey));
                }
            }
            else
            {
                _local_quantum_errors[label][qubitsKey] = error;
            }

            if (_default_quantum_errors.ContainsKey(label))
            {
                if (warnings)
                {
                    Logger.LogWarning(string.Format(
                        "WARNING: Specific error for instruction \"{0}\" on qubits {1} overrides previously defined all-qubit error for these qubits.",
                        label, qubitsKey));
                }
            }
            _noise_instructions.Add(label);
            AddBasisGates(new List<string> { name });
        }
    }

    // Method to add an all-qubit readout error
    public void AddAllQubitReadoutError(NoiseReadoutError error, bool warnings = true)
    {
        if (error.IsIdeal())
            return;

        if (error.NumberOfQubits != 1)
        {
            throw new NoiseError("All-qubit readout errors must be defined as single-qubit errors.");
        }
        if (_default_readout_error != null)
        {
            if (warnings)
            {
                Logger.LogWarning("WARNING: all-qubit readout error already exists, overriding with new readout error.");
            }
        }
        _default_readout_error = error;

        if (_local_readout_errors.Count > 0)
        {
            string localQubits = _KeysToString(_local_readout_errors.Keys);
            if (warnings)
            {
                Logger.LogWarning(string.Format(
                    "WARNING: The all-qubit readout error will not apply to measure of qubits: {0} as specific readout errors already exist.",
                    localQubits));
            }
        }
        _noise_instructions.Add("measure");
    }

    // Method to add a readout error
    public void AddReadoutError(NoiseReadoutError error, List<int> qubits, bool warnings = true)
    {
        if (error.IsIdeal())
            return;

        foreach (var qubit in qubits)
        {
            _noise_qubits.Add(qubit);
        }

        if (error.NumberOfQubits != qubits.Count)
        {
            throw new NoiseError(string.Format(
                "Number of qubits ({0}) does not match the readout error size ({1})",
                qubits.Count, error.NumberOfQubits));
        }

        string qubitsKey = string.Join(",", qubits.ConvertAll(q => q.ToString()).ToArray());
        if (_local_readout_errors.ContainsKey(qubitsKey))
        {
            if (warnings)
            {
                Logger.LogWarning(string.Format(
                    "WARNING: readout error already exists for qubits {0}, overriding with new readout error.",
                    qubitsKey));
            }
        }
        _local_readout_errors[qubitsKey] = error;

        if (_default_readout_error != null)
        {
            if (warnings)
            {
                Logger.LogWarning(string.Format(
                    "WARNING: Specific readout error on qubits {0} overrides previously defined all-qubit readout error for these qubits.",
                    qubitsKey));
            }
        }
        _noise_instructions.Add("measure");
    }

    // Convert the noise model to a dictionary (simplified for Unity 6)
    public Dictionary<string, object> ToDict(bool serializable = false)
    {
        var errorList = new List<Dictionary<string, object>>();

        // Add default quantum errors
        foreach (KeyValuePair<string, NoiseQuantumError> kvp in _default_quantum_errors)
        {
            var errorDict = new Dictionary<string, object>();
            // Placeholder for error details
            errorDict["operations"] = new List<string> { kvp.Key };
            errorList.Add(errorDict);
        }

        // Add specific qubit errors
        foreach (KeyValuePair<string, Dictionary<string, NoiseQuantumError>> kvp in _local_quantum_errors)
        {
            foreach (KeyValuePair<string, NoiseQuantumError> qubitKvp in kvp.Value)
            {
                var errorDict = new Dictionary<string, object>();
                // Placeholder for error details
                errorDict["operations"] = new List<string> { kvp.Key };
                errorDict["gate_qubits"] = new List<string> { qubitKvp.Key };
                errorList.Add(errorDict);
            }
        }

        // Add default readout error
        if (_default_readout_error != null)
        {
            var errorDict = new Dictionary<string, object>();
            // Placeholder for error details
            errorList.Add(errorDict);
        }

        // Add local readout errors
        foreach (KeyValuePair<string, NoiseReadoutError> kvp in _local_readout_errors)
        {
            var errorDict = new Dictionary<string, object>();
            // Placeholder for error details
            errorDict["gate_qubits"] = new List<string> { kvp.Key };
            errorList.Add(errorDict);
        }

        var result = new Dictionary<string, object>
        {
            { "errors", errorList }
        };

        // Serialization with Unity's JsonUtility (Note: JsonUtility cannot serialize Dictionary directly)
        if (serializable)
        {
            // Convert to a serializable format
            string jsonString = JsonUtility.ToJson(new SerializationWrapper(result));
            result = JsonUtility.FromJson<SerializationWrapper>(jsonString).ToDictionary();
        }

        return result;
    }

    // Helper class for serialization (since Unity's JsonUtility cannot serialize Dictionary directly)
    [System.Serializable]
    private class SerializationWrapper
    {
        public List<DictionaryEntry> entries;

        public SerializationWrapper(Dictionary<string, object> dict)
        {
            entries = new List<DictionaryEntry>();
            foreach (var kvp in dict)
            {
                entries.Add(new DictionaryEntry(kvp.Key, kvp.Value));
            }
        }

        public Dictionary<string, object> ToDictionary()
        {
            var dict = new Dictionary<string, object>();
            foreach (var entry in entries)
            {
                dict[entry.key] = entry.value;
            }
            return dict;
        }
    }

    [System.Serializable]
    private class DictionaryEntry
    {
        public string key;
        public object value;

        public DictionaryEntry(string key, object value)
        {
            this.key = key;
            this.value = value;
        }
    }

    // Private helper methods
    private List<string[]> _InstructionNamesLabels(List<string> instructions)
    {
        var namesLabels = new List<string[]>();
        foreach (var inst in instructions)
        {
            namesLabels.Add(new string[] { inst, inst });
        }
        return namesLabels;
    }

    private void _CheckNumberOfQubits(NoiseQuantumError error, string name)
    {
        string ErrorMessage(int gateQubits)
        {
            return string.Format("{0} qubit NoiseQuantumError cannot be applied to {1} qubit instruction \"{2}\".", error.NumQubits, gateQubits, name);
        }

        if (System.Array.Exists(_1qubit_instructions, element => element == name) && error.NumQubits != 1)
        {
            throw new NoiseError(ErrorMessage(1));
        }
        if (System.Array.Exists(_2qubit_instructions, element => element == name) && error.NumQubits != 2)
        {
            throw new NoiseError(ErrorMessage(2));
        }
        if (System.Array.Exists(_3qubit_instructions, element => element == name) && error.NumQubits != 3)
        {
            throw new NoiseError(ErrorMessage(3));
        }
    }

    private string _KeysToString(ICollection<string> keys)
    {
        return string.Join(", ", new List<string>(keys).ToArray());
    }

    // Reset the noise model
    public void Reset()
    {
        _basis_gates.Clear();
        _noise_instructions.Clear();
        _noise_qubits.Clear();
        _default_quantum_errors.Clear();
        _local_quantum_errors.Clear();
        _default_readout_error = null;
        _local_readout_errors.Clear();
        _custom_noise_passes.Clear();
    }

    public override string ToString()
    {
        if (IsIdeal())
        {
            return "NoiseModel: Ideal";
        }

        var defaultErrorOps = new List<string>(_default_quantum_errors.Keys);
        if (_default_readout_error != null && !defaultErrorOps.Contains("measure"))
        {
            defaultErrorOps.Add("measure");
        }

        var localErrorOps = new List<string>();
        foreach (var kvp in _local_quantum_errors)
        {
            foreach (var qubits in kvp.Value.Keys)
            {
                localErrorOps.Add(string.Format("({0}, {1})", kvp.Key, qubits));
            }
        }
        foreach (var qubits in _local_readout_errors.Keys)
        {
            var tmp = string.Format("(measure, {0})", qubits);
            if (!localErrorOps.Contains(tmp))
            {
                localErrorOps.Add(tmp);
            }
        }

        string output = "NoiseModel:";
        output += "\n  Basis gates: " + string.Join(", ", BasisGates.ToArray());
        if (_noise_instructions.Count > 0)
        {
            output += "\n  Instructions with noise: " + string.Join(", ", new List<string>(_noise_instructions).ToArray());
        }
        if (_noise_qubits.Count > 0)
        {
            output += "\n  Qubits with noise: " + string.Join(", ", new List<int>(_noise_qubits).ConvertAll(q => q.ToString()).ToArray());
        }
        if (defaultErrorOps.Count > 0)
        {
            output += "\n  All-qubits errors: " + string.Join(", ", defaultErrorOps.ToArray());
        }
        if (localErrorOps.Count > 0)
        {
            output += "\n  Specific qubit errors: " + string.Join(", ", localErrorOps.ToArray());
        }
        return output;
    }

    public override bool Equals(object obj)
    {
        if (obj is NoiseModel)
        {
            var other = (NoiseModel)obj;
            return _ListsEqual(BasisGates, other.BasisGates) &&
                   _ListsEqual(NoiseInstructions, other.NoiseInstructions) &&
                   _ListsEqual(NoiseQubits, other.NoiseQubits) &&
                   _ReadoutErrorsEqual(other) &&
                   _AllQubitQuantumErrorsEqual(other) &&
                   _LocalQuantumErrorsEqual(other);
        }
        return false;
    }

    private bool _ListsEqual<T>(List<T> list1, List<T> list2)
    {
        if (list1.Count != list2.Count)
            return false;
        foreach (var item in list1)
        {
            if (!list2.Contains(item))
                return false;
        }
        return true;
    }

    private bool _ReadoutErrorsEqual(NoiseModel other)
    {
        if ((_default_readout_error == null) != (other._default_readout_error == null))
            return false;
        if (_default_readout_error != null && !_default_readout_error.Equals(other._default_readout_error))
            return false;
        if (!_ListsEqual(new List<string>(_local_readout_errors.Keys), new List<string>(other._local_readout_errors.Keys)))
            return false;
        foreach (var key in _local_readout_errors.Keys)
        {
            if (!_local_readout_errors[key].Equals(other._local_readout_errors[key]))
                return false;
        }
        return true;
    }

    private bool _AllQubitQuantumErrorsEqual(NoiseModel other)
    {
        if (!_ListsEqual(new List<string>(_default_quantum_errors.Keys), new List<string>(other._default_quantum_errors.Keys)))
            return false;
        foreach (var key in _default_quantum_errors.Keys)
        {
            if (!_default_quantum_errors[key].Equals(other._default_quantum_errors[key]))
                return false;
        }
        return true;
    }

    private bool _LocalQuantumErrorsEqual(NoiseModel other)
    {
        if (!_ListsEqual(new List<string>(_local_quantum_errors.Keys), new List<string>(other._local_quantum_errors.Keys)))
            return false;
        foreach (var key in _local_quantum_errors.Keys)
        {
            var innerDict = _local_quantum_errors[key];
            var otherInnerDict = other._local_quantum_errors[key];
            if (!_ListsEqual(new List<string>(innerDict.Keys), new List<string>(otherInnerDict.Keys)))
                return false;
            foreach (var innerKey in innerDict.Keys)
            {
                if (!innerDict[innerKey].Equals(otherInnerDict[innerKey]))
                    return false;
            }
        }
        return true;
    }

    public override int GetHashCode()
    {
        // Implement a suitable hash code if necessary
        return base.GetHashCode();
    }
}
