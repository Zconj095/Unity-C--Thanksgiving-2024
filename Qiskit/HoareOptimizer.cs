using System;
using System.Collections.Generic;
using Z3;

public class HoareOptimizer : TransformationPass
{
    private const double CHOP_THRESHOLD = 1e-15;
    private Z3Solver solver;
    private Dictionary<Qubit, List<Z3BoolRef>> variables;
    private Dictionary<Qubit, int> gatenum;
    private Dictionary<Qubit, List<Gate>> gatecache;
    private Dictionary<Qubit, Dictionary<Gate, int>> varnum;
    private int size;

    /// <summary>
    /// Initializes the HoareOptimizer pass.
    /// </summary>
    public HoareOptimizer(int size = 10)
    {
        this.solver = new Z3Solver();
        this.variables = new Dictionary<Qubit, List<Z3BoolRef>>();
        this.gatenum = new Dictionary<Qubit, int>();
        this.gatecache = new Dictionary<Qubit, List<Gate>>();
        this.varnum = new Dictionary<Qubit, Dictionary<Gate, int>>();
        this.size = size;
    }

    private Z3BoolRef _genVariable(Qubit qubit)
    {
        string varname = "q" + qubit.Index + "_" + gatenum[qubit];
        Z3BoolRef var = solver.Bool(varname);
        gatenum[qubit]++;
        variables[qubit].Add(var);
        return var;
    }

    private void _initialize(DAGCircuit dag)
    {
        foreach (var qubit in dag.Qubits)
        {
            gatenum[qubit] = 0;
            variables[qubit] = new List<Z3BoolRef>();
            gatecache[qubit] = new List<Gate>();
            varnum[qubit] = new Dictionary<Gate, int>();
            _genVariable(qubit);
        }
    }

    private void _addPostConditions(Gate gate, Z3BoolRef ctrlOnes, List<Qubit> trgtQubits, List<Z3BoolRef> trgtVars)
    {
        List<Z3BoolRef> newVars = new List<Z3BoolRef>();
        foreach (var qbt in trgtQubits)
        {
            newVars.Add(_genVariable(qbt));
        }

        try
        {
            solver.Add(Z3.Implies(ctrlOnes, gate.GetPostConditions(trgtVars.Concat(newVars))));
        }
        catch (Exception)
        {
            // Ignore if gate has no post-conditions
        }

        for (int i = 0; i < trgtVars.Count; i++)
        {
            solver.Add(Z3.Implies(Z3.Not(ctrlOnes), newVars[i] == trgtVars[i]));
        }
    }

    private bool _testGate(Gate gate, Z3BoolRef ctrlOnes, List<Z3BoolRef> trgtVars)
    {
        bool trivial = false;
        solver.Push();

        try
        {
            var trivialCond = gate.GetTrivialIf(trgtVars);
            if (trivialCond is bool)
            {
                trivial = (bool)trivialCond;
            }
            else
            {
                solver.Add(Z3.And(ctrlOnes, Z3.Not(trivialCond)));
                trivial = solver.Check() == Z3Status.UNSAT;
            }
        }
        catch (Exception)
        {
            solver.Add(ctrlOnes);
            trivial = solver.Check() == Z3Status.UNSAT;
        }

        solver.Pop();
        return trivial;
    }

    private bool _removeControl(Gate gate, List<Z3BoolRef> ctrlVars, List<Z3BoolRef> trgtVars)
    {
        bool remove = false;

        var qarg = new QuantumRegister(gate.NumQubits);
        var dag = new DAGCircuit();
        dag.AddQreg(qarg);

        var qb = new List<int>(new int[gate.NumQubits]);

        if (gate is ControlledGate)
        {
            remove = _checkRemoval(ctrlVars);
        }

        if (remove)
        {
            List<Qubit> qubits = new List<Qubit>(qb.Select(index => qarg[index]));
            dag.ApplyOperationBack(gate.BaseGate, qubits);
        }

        return remove;
    }

    private bool _checkRemoval(List<Z3BoolRef> ctrlVars)
    {
        solver.Push();
        solver.Add(Z3.Not(Z3.And(ctrlVars)));
        bool remove = solver.Check() == Z3Status.UNSAT;
        solver.Pop();

        return remove;
    }

    private void _traverseDag(DAGCircuit dag)
    {
        var nodes = new List<DAGOpNode>(dag.TopologicalOpNodes());
        foreach (var node in nodes)
        {
            var gate = node.Op;
            var (ctrlQubits, ctrlVars, trgtQubits, trgtVars) = _separateCtrlTrgt(node);

            Z3BoolRef ctrlOnes = Z3.And(ctrlVars);

            var (removeCtrl, newDag, _) = _removeControl(gate, ctrlVars, trgtVars);

            if (removeCtrl)
            {
                var mappedNodes = dag.SubstituteNodeWithDag(node, newDag);
                node = mappedNodes.First().Value;
                gate = node.Op;
                (ctrlQubits, ctrlVars, trgtQubits, trgtVars) = _separateCtrlTrgt(node);

                ctrlOnes = Z3.And(ctrlVars);
            }

            bool trivial = _testGate(gate, ctrlOnes, trgtVars);
            if (trivial)
            {
                dag.RemoveOpNode(node);
            }
            else if (size > 1)
            {
                foreach (var qbt in node.Qargs)
                {
                    gatecache[qbt].Add(node);
                    varnum[qbt][node] = gatenum[qbt] - 1;
                }

                foreach (var qbt in node.Qargs)
                {
                    if (gatecache[qbt].Count >= size)
                    {
                        _multigateOpt(dag, qbt);
                    }
                }
            }

            _addPostConditions(gate, ctrlOnes, trgtQubits, trgtVars);
        }
    }

    private (List<Qubit>, List<Z3BoolRef>, List<Qubit>, List<Z3BoolRef>) _separateCtrlTrgt(DAGOpNode node)
    {
        var gate = node.Op;
        int numCtrl = gate is ControlledGate ? ((ControlledGate)gate).NumCtrlQubits : 0;
        var ctrlQubits = node.Qargs.GetRange(0, numCtrl);
        var trgtQubits = node.Qargs.GetRange(numCtrl, node.Qargs.Count - numCtrl);

        var ctrlVars = ctrlQubits.Select(qb => variables[qb][varnum[qb][node]]).ToList();
        var trgtVars = trgtQubits.Select(qb => variables[qb][varnum[qb][node]]).ToList();

        return (ctrlQubits, ctrlVars, trgtQubits, trgtVars);
    }

    private void _multigateOpt(DAGCircuit dag, Qubit qubit, int maxIdx = -1, HashSet<int> dontRec = null)
    {
        if (gatecache[qubit].Count == 0)
            return;

        _removeSuccessiveIdentity(dag, qubit, maxIdx);
        if (gatecache[qubit].Count < size && maxIdx == -1)
            return;

        var gatesToRemove = new List<Gate>();
        if (maxIdx == -1)
        {
            maxIdx = 0;
            dontRec = new HashSet<int> { qubit.Index };
        }
        else
        {
            gatesToRemove.AddRange(gatecache[qubit].GetRange(0, maxIdx + 1));
        }

        foreach (var node in gatesToRemove)
        {
            var newQbt = node.Qargs.Where(q => !dontRec.Contains(q.Index)).ToList();
            dontRec.UnionWith(newQbt.Select(q => q.Index));

            foreach (var qbt in newQbt)
            {
                var idx = gatecache[qbt].IndexOf(node);
                _multigateOpt(dag, qbt, maxIdx: idx, dontRec: dontRec);
            }
        }

        gatecache[qubit] = gatecache[qubit].GetRange(maxIdx + 1, gatecache[qubit].Count - maxIdx - 1);
    }

    private void _removeSuccessiveIdentity(DAGCircuit dag, Qubit qubit, int fromIdx = -1)
    {
        int i = 0;
        while (i < gatecache[qubit].Count - 1)
        {
            var node1 = gatecache[qubit][i];
            var node2 = gatecache[qubit][i + 1];
            var trgtqb1 = _separateCtrlTrgt(node1).Item3;
            var trgtqb2 = _separateCtrlTrgt(node2).Item3;

            i++;
            if (trgtqb1 != trgtqb2)
                continue;

            if (_isIdentity(new List<DAGOpNode> { node1, node2 }) && _seqAsOne(new List<DAGOpNode> { node1, node2 }))
            {
                dag.RemoveOpNode(node1);
                dag.RemoveOpNode(node2);

                foreach (var qbt in trgtqb1)
                {
                    gatecache[qbt].Remove(node1);
                    gatecache[qbt].Remove(node2);
                }
            }
        }
    }

    private bool _isIdentity(List<DAGOpNode> sequence)
    {
        var gate1 = sequence[0].Op;
        var gate2 = sequence[1].Op;

        return gate1 == gate2 && gate1.Params == gate2.Params;
    }

    private bool _seqAsOne(List<DAGOpNode> sequence)
    {
        var ctrlVar1 = _separateCtrlTrgt(sequence[0]).Item2;
        var ctrlVar2 = _separateCtrlTrgt(sequence[1]).Item2;

        solver.Push();
        solver.Add(Z3.Or(Z3.And(Z3.And(ctrlVar1), Z3.Not(Z3.And(ctrlVar2))),
                         Z3.And(Z3.Not(Z3.And(ctrlVar1)), Z3.And(ctrlVar2))));

        bool res = solver.Check() == Z3Status.UNSAT;
        solver.Pop();

        return res;
    }

    public override DAGCircuit Run(DAGCircuit dag)
    {
        _initialize(dag);
        _traverseDag(dag);
        if (size > 1)
        {
            foreach (var qubit in dag.Qubits)
            {
                _multigateOpt(dag, qubit);
            }
        }
        return dag;
    }
}
