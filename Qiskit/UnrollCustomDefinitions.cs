using System;
using System.Collections.Generic;

public class UnrollCustomDefinitions : TransformationPass
{
    private EquivalenceLibrary equivalenceLibrary;
    private List<string> basisGates;
    private Target target;
    private int minQubits;

    /// <summary>
    /// Initializes the UnrollCustomDefinitions pass.
    /// </summary>
    /// <param name="equivalenceLibrary">The equivalence library used for decomposing gates.</param>
    /// <param name="basisGates">Optional list of target basis gates to unroll to. Ignored if `target` is specified.</param>
    /// <param name="target">Optional target object representing the compilation target. If specified, `basisGates` is ignored.</param>
    /// <param name="minQubits">The minimum number of qubits for operations in the input DAG to unroll.</param>
    public UnrollCustomDefinitions(EquivalenceLibrary equivalenceLibrary, List<string> basisGates = null, Target target = null, int minQubits = 0)
    {
        this.equivalenceLibrary = equivalenceLibrary;
        this.basisGates = basisGates;
        this.target = target;
        this.minQubits = minQubits;
    }

    /// <summary>
    /// Runs the UnrollCustomDefinitions pass on the given DAG.
    /// </summary>
    /// <param name="dag">The input DAG circuit.</param>
    /// <returns>The unrolled DAG circuit.</returns>
    public override DAGCircuit Run(DAGCircuit dag)
    {
        // If neither basisGates nor target is specified, do nothing
        if (basisGates == null && target == null)
            return dag;

        HashSet<string> deviceInsts = new HashSet<string> { "measure", "reset", "barrier", "snapshot", "delay", "store" };
        if (target == null)
        {
            deviceInsts.UnionWith(basisGates);
        }

        foreach (var node in dag.OpNodes())
        {
            if (node.Op is ControlFlowOp controlFlowOp)
            {
                dag.SubstituteNode(node, ControlFlowOp.MapBlocks(Run, controlFlowOp), propagateCondition: false);
                continue;
            }

            if (GetDirectiveFlag(node.Op))  // Check if the node has a directive flag
                continue;

            if (dag.HasCalibrationFor(node) || node.Qargs.Count < minQubits)
                continue;

            bool controlledGateOpenCtrl = node.Op is ControlledGate controlledGate && controlledGate.OpenCtrl;
            if (!controlledGateOpenCtrl)
            {
                bool instSupported = target != null
                    ? target.IsInstructionSupported(node.Op.Name, GetQargsIndices(dag, node))
                    : deviceInsts.Contains(node.Name);

                if (instSupported || equivalenceLibrary.HasEntry(node.Op))
                    continue;
            }

            try
            {
                var unrolled = node.Op.Definition;
                if (unrolled == null)
                {
                    throw new Exception($"Cannot unroll the circuit to the given basis. Instruction {node.Op.Name} not found in equivalence library and no rule found to expand.");
                }

                var decomposition = CircuitToDag(unrolled, copyOperations: false);
                var unrolledDag = Run(decomposition);
                dag.SubstituteNodeWithDag(node, unrolledDag);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error decomposing node {node.Name}: {ex.Message}", ex);
            }
        }

        return dag;
    }

    private bool GetDirectiveFlag(Instruction op)
    {
        // Placeholder for checking directive flag on the operation
        return op?.Directive ?? false;
    }

    private List<int> GetQargsIndices(DAGCircuit dag, DAGOpNode node)
    {
        var qargsIndices = new List<int>();
        foreach (var qarg in node.Qargs)
        {
            qargsIndices.Add(dag.FindBit(qarg).Index);  // Get the index of the qubit in the DAG
        }
        return qargsIndices;
    }

    private DAGCircuit CircuitToDag(Instruction op, bool copyOperations = true)
    {
        var dag = new DAGCircuit();
        dag.AddQubits(op.NumQubits);
        dag.AddClbits(op.NumClbits);
        dag.ApplyOperationBack(op, dag.Qubits, dag.Clbits, check: false);
        return dag;
    }
}
