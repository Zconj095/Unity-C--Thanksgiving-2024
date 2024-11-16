using System;
using System.Collections.Generic;

public static class QuantumCircuitComparison
{
    public static bool LegacyConditionEq(
        object cond1, object cond2, Dictionary<Clbit, int> bitIndices1, Dictionary<Clbit, int> bitIndices2)
    {
        if (cond1 == null && cond2 == null)
        {
            return true;
        }
        if (cond1 == null || cond2 == null)
        {
            return false;
        }

        var (target1, val1) = ((Clbit, object))cond1;
        var (target2, val2) = ((Clbit, object))cond2;
        if (!val1.Equals(val2))
        {
            return false;
        }

        if (target1 is Clbit && target2 is Clbit)
        {
            return bitIndices1[target1] == bitIndices2[target2];
        }
        if (target1 is ClassicalRegister reg1 && target2 is ClassicalRegister reg2)
        {
            return reg1.Size == reg2.Size && CompareBitIndices(reg1, reg2, bitIndices1, bitIndices2);
        }
        return false;
    }

    private static bool CompareBitIndices(ClassicalRegister reg1, ClassicalRegister reg2,
        Dictionary<Clbit, int> bitIndices1, Dictionary<Clbit, int> bitIndices2)
    {
        for (int i = 0; i < reg1.Size; i++)
        {
            if (bitIndices1[reg1[i]] != bitIndices2[reg2[i]])
            {
                return false;
            }
        }
        return true;
    }

    public static DAGCircuit CircuitToDAG(QuantumCircuit circuit, 
        List<Qubit> nodeQargs, List<Clbit> nodeCargs, Dictionary<Bit, int> bitIndices)
    {
        Func<Tuple<Bit>, int> sortKey = bits => bitIndices[bits.Item1];

        return CircuitToDAGConverter(circuit, false,
            nodeQargs.OrderBy(inner => sortKey(inner)).ToList(),
            nodeCargs.OrderBy(inner => sortKey(inner)).ToList());
    }

    private static DAGCircuit CircuitToDAGConverter(QuantumCircuit circuit, bool copyOperations,
        List<Qubit> qubitOrder, List<Clbit> clbitOrder)
    {
        // Replace with your actual logic for DAG conversion.
        return new DAGCircuit();
    }

    public static Func<object, int> MakeExprKey(Dictionary<Bit, int> bitIndices)
    {
        return var =>
        {
            if (var is Clbit clbit)
            {
                return bitIndices[clbit];
            }

            if (var is ClassicalRegister reg)
            {
                return reg.Select(bit => bitIndices[bit]).ToList();
            }
            return -1;
        };
    }

    public static bool ConditionOpEq(DAGOpNode node1, DAGOpNode node2,
        Dictionary<Clbit, int> bitIndices1, Dictionary<Clbit, int> bitIndices2)
    {
        var cond1 = node1.Condition;
        var cond2 = node2.Condition;

        if (cond1 is Expr expr1 && cond2 is Expr expr2)
        {
            return Expr.StructurallyEquivalent(expr1, expr2, MakeExprKey(bitIndices1), MakeExprKey(bitIndices2));
        }

        if (cond1 is Expr || cond2 is Expr)
        {
            return false;
        }

        if (!LegacyConditionEq(cond1, cond2, bitIndices1, bitIndices2))
        {
            return false;
        }

        return node1.Op.Blocks.Count == node2.Op.Blocks.Count && node1.Op.Blocks
            .Zip(node2.Op.Blocks, (block1, block2) =>
                CircuitToDAG(block1, node1.Qargs, node1.Cargs, bitIndices1)
                .Equals(CircuitToDAG(block2, node2.Qargs, node2.Cargs, bitIndices2)))
            .All(x => x);
    }

    public static bool SwitchCaseOpEq(DAGOpNode node1, DAGOpNode node2,
        Dictionary<Clbit, int> bitIndices1, Dictionary<Clbit, int> bitIndices2)
    {
        var target1 = node1.Op.Target;
        var target2 = node2.Op.Target;

        if (target1 is Expr expr1 && target2 is Expr expr2)
        {
            return Expr.StructurallyEquivalent(expr1, expr2, MakeExprKey(bitIndices1), MakeExprKey(bitIndices2));
        }

        if (target1 is Clbit clbit1 && target2 is Clbit clbit2)
        {
            return bitIndices1[clbit1] == bitIndices2[clbit2];
        }

        if (target1 is ClassicalRegister reg1 && target2 is ClassicalRegister reg2)
        {
            return reg1.Size == reg2.Size && CompareBitIndices(reg1, reg2, bitIndices1, bitIndices2);
        }

        return false;
    }

    public static bool ForLoopOpEq(DAGOpNode node1, DAGOpNode node2,
        Dictionary<Clbit, int> bitIndices1, Dictionary<Clbit, int> bitIndices2)
    {
        var (indexset1, param1, body1) = node1.Op.Params;
        var (indexset2, param2, body2) = node2.Op.Params;

        if (indexset1 != indexset2)
        {
            return false;
        }

        if ((param1 == null && param2 != null) || (param1 != null && param2 == null))
        {
            return false;
        }

        if (param1 != null && param2 != null)
        {
            var sentinel = new Parameter(Guid.NewGuid().ToString());
            body1 = body1.AssignParameters(new Dictionary<Parameter, object> { { param1, sentinel } }, false);
            body2 = body2.AssignParameters(new Dictionary<Parameter, object> { { param2, sentinel } }, false);
        }

        return CircuitToDAG(body1, node1.Qargs, node1.Cargs, bitIndices1)
            .Equals(CircuitToDAG(body2, node2.Qargs, node2.Cargs, bitIndices2));
    }
}
