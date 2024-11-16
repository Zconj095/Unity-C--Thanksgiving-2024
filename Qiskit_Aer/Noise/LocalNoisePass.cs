using System;
using System.Collections.Generic;

public class LocalNoisePass
{
    private readonly Func<Instruction, List<int>, Instruction> _noiseFunction;
    private readonly List<Type> _operationTypes;
    private readonly string _method;

    public LocalNoisePass(
        Func<Instruction, List<int>, Instruction> noiseFunction,
        IEnumerable<Type> operationTypes = null,
        string method = "append")
    {
        if (method != "append" && method != "prepend" && method != "replace")
        {
            throw new ArgumentException($"Invalid method: {method}. It must be 'append', 'prepend', or 'replace'.");
        }

        _noiseFunction = noiseFunction ?? throw new ArgumentNullException(nameof(noiseFunction));
        _operationTypes = operationTypes != null ? new List<Type>(operationTypes) : new List<Type>();
        _method = method;

        foreach (var type in _operationTypes)
        {
            if (!type.IsSubclassOf(typeof(Instruction)))
            {
                throw new ArgumentException($"Invalid operation type: {type}. Must be a subclass of Instruction.");
            }
        }
    }

    public DAGCircuit Run(DAGCircuit dag)
    {
        if (dag == null) throw new ArgumentNullException(nameof(dag));

        // Map qubit objects to their indices
        var qubitIndices = new Dictionary<Qubit, int>();
        for (int i = 0; i < dag.Qubits.Count; i++)
        {
            qubitIndices[dag.Qubits[i]] = i;
        }

        foreach (var node in dag.TopologicalOperationNodes())
        {
            // Skip nodes not matching specified operation types
            if (_operationTypes.Count > 0 && !_operationTypes.Contains(node.Operation.GetType()))
            {
                continue;
            }

            var qubits = new List<int>();
            foreach (var qubit in node.Qargs)
            {
                qubits.Add(qubitIndices[qubit]);
            }

            var newOp = _noiseFunction(node.Operation, qubits);

            // Handle "replace" method with no returned operation (removing the node)
            if (newOp == null)
            {
                if (_method == "replace")
                {
                    dag.RemoveOperationNode(node);
                }
                continue;
            }

            // Initialize a new sub-DAG for substitution
            var newDag = new DAGCircuit();
            newDag.AddQubits(node.Qargs);
            newDag.AddClbits(node.Cargs);

            if (_method == "append")
            {
                newDag.ApplyOperationBack(node.Operation, node.Qargs, node.Cargs);
            }

            if (newOp is QuantumCircuit newCircuit)
            {
                // If the new operation is a QuantumCircuit, compose its DAG with the new sub-DAG
                newDag.Compose(newCircuit.ToDAG(), node.Qargs);
            }
            else if (newOp is Instruction newInstruction)
            {
                if (newInstruction.NumClbits > 0)
                {
                    throw new InvalidOperationException("Noise must not contain classical bits.");
                }

                if (newInstruction.NumQubits != node.Qargs.Count)
                {
                    throw new InvalidOperationException($"Mismatch in qubit count between new operation ({newInstruction.NumQubits}) and the original operation ({node.Qargs.Count}).");
                }

                newDag.ApplyOperationBack(newInstruction, node.Qargs);
            }
            else
            {
                throw new InvalidOperationException("The noise function must return a QuantumCircuit or Instruction.");
            }

            if (_method == "prepend")
            {
                newDag.ApplyOperationBack(node.Operation, node.Qargs, node.Cargs);
            }

            dag.SubstituteNodeWithDAG(node, newDag);
        }

        return dag;
    }
}
