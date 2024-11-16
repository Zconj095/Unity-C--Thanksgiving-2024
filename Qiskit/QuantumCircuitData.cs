using System;
using System.Collections;
using System.Collections.Generic;
public class QuantumCircuitData : IList<CircuitInstruction>
{
    private List<CircuitInstruction> _data = new List<CircuitInstruction>();
    private QuantumCircuit _circuit;

    public QuantumCircuitData(QuantumCircuit circuit)
    {
        _circuit = circuit;
    }

    public CircuitInstruction this[int index]
    {
        get => _data[index];
        set
        {
            var resolvedValue = ResolveLegacyValue(value.Operation, value.Qubits, value.Clbits);
            _data[index] = resolvedValue;
        }
    }

    public int Count => _data.Count;
    public bool IsReadOnly => false;

    public void Add(CircuitInstruction item)
    {
        var resolvedItem = ResolveLegacyValue(item.Operation, item.Qubits, item.Clbits);
        _data.Add(resolvedItem);
    }

    public void Clear() => _data.Clear();

    public bool Contains(CircuitInstruction item) => _data.Contains(item);

    public void CopyTo(CircuitInstruction[] array, int arrayIndex) => _data.CopyTo(array, arrayIndex);

    public IEnumerator<CircuitInstruction> GetEnumerator() => _data.GetEnumerator();

    public int IndexOf(CircuitInstruction item) => _data.IndexOf(item);

    public void Insert(int index, CircuitInstruction item)
    {
        var resolvedItem = ResolveLegacyValue(item.Operation, item.Qubits, item.Clbits);
        _data.Insert(index, resolvedItem);
    }

    public bool Remove(CircuitInstruction item) => _data.Remove(item);

    public void RemoveAt(int index) => _data.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();

    private CircuitInstruction ResolveLegacyValue(Operation operation, List<Qubit> qubits, List<Clbit> clbits)
    {
        if (!(operation is Operation))
        {
            operation = operation.ToInstruction();
        }

        if (!(operation is Operation))
        {
            throw new CircuitError("Object is not an Operation.");
        }

        var expandedQargs = qubits; // Placeholder for _qbit_argument_conversion
        var expandedCargs = clbits; // Placeholder for _cbit_argument_conversion

        var broadcastArgs = (operation is Instruction instruction)
            ? instruction.BroadcastArguments(expandedQargs, expandedCargs)
            : Instruction.BroadcastArguments(operation, expandedQargs, expandedCargs);

        if (!broadcastArgs.GetEnumerator().MoveNext())
        {
            throw new CircuitError("QuantumCircuit.data modification does not support argument broadcasting.");
        }

        var firstArgs = broadcastArgs.GetEnumerator().Current;

        _circuit.CheckForDuplicateQubits(firstArgs.Item1); // Placeholder for _check_dups
        return new CircuitInstruction(operation, firstArgs.Item1, firstArgs.Item2);
    }
}