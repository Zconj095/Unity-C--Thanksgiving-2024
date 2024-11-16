using System;
using System.Collections.Generic;
using System.Linq;

public class Optimize1qGatesSimpleCommutation : TransformationPass
{
    private const int DefaultSize = 10;
    private Optimize1qGatesDecomposition _optimize1q;
    private bool _runToCompletion;

    private static readonly Dictionary<Type, (List<string> Precommute, List<string> Postcommute)> commutationTable = new Dictionary<Type, (List<string>, List<string)>
    {
        { typeof(RZXGate), (new List<string> { "rz", "p" }, new List<string> { "x", "sx", "rx" }) },
        { typeof(CXGate), (new List<string> { "rz", "p" }, new List<string> { "x", "sx", "rx" }) },
    };

    public Optimize1qGatesSimpleCommutation(List<string> basis = null, bool runToCompletion = false, Target target = null)
    {
        _optimize1q = new Optimize1qGatesDecomposition(basis, target);
        _runToCompletion = runToCompletion;
    }

    public static List<DAGOpNode> FindAdjoiningRun(DAGCircuit dag, List<List<DAGOpNode>> runs, List<DAGOpNode> run, bool front = true)
    {
        DAGOpNode edgeNode = front ? run[0] : run[run.Count - 1];
        var blocker = front ? dag.Predecessors(edgeNode).First() : dag.Successors(edgeNode).First();
        var possibilities = front ? dag.Predecessors(blocker) : dag.Successors(blocker);

        List<DAGOpNode> adjoiningRun = new List<DAGOpNode>();
        foreach (var possibility in possibilities)
        {
            if (possibility is DAGOpNode node && node.Qargs.SequenceEqual(edgeNode.Qargs))
            {
                adjoiningRun = runs.FirstOrDefault(singleRun => singleRun.Count > 0 && singleRun[0].Qargs.SequenceEqual(possibility.Qargs));
                break;
            }
        }

        return adjoiningRun;
    }

    public static (List<DAGOpNode> commuted, List<DAGOpNode> remainder) CommuteThrough(DAGOpNode blocker, List<DAGOpNode> run, bool front = true)
    {
        var runClone = new LinkedList<DAGOpNode>(run);
        var commuted = new LinkedList<DAGOpNode>();

        var commutationRule = GetCommutationRule(blocker, run);
        if (commutationRule != null)
        {
            while (runClone.Count > 0)
            {
                var nextGate = front ? runClone.First() : runClone.Last();
                if (!commutationRule.Contains(nextGate.Name))
                {
                    break;
                }

                if (front)
                {
                    runClone.RemoveFirst();
                    commuted.AddLast(nextGate);
                }
                else
                {
                    runClone.RemoveLast();
                    commuted.AddFirst(nextGate);
                }
            }
        }

        return (commuted.ToList(), runClone.ToList());
    }

    private static List<string> GetCommutationRule(DAGOpNode blocker, List<DAGOpNode> run)
    {
        if (commutationTable.TryGetValue(blocker.Op.BaseClass, out var rule) && run[0].Qargs[0] == blocker.Qargs[0])
        {
            return rule.Precommute;
        }

        return null;
    }

    public DAGCircuit Resynthesize(List<DAGOpNode> run, Qubit qubit)
    {
        if (run.Count == 0)
        {
            var dag = new DAGCircuit();
            dag.AddQreg(new QuantumRegister(1));
            return dag;
        }

        var operatorMatrix = run[0].Op.ToMatrix();
        foreach (var gate in run.Skip(1))
        {
            operatorMatrix = gate.Op.ToMatrix().Dot(operatorMatrix);
        }

        return _optimize1q.GateSequenceToDag(_optimize1q.ResynthesizeRun(operatorMatrix, qubit));
    }

    private static void ReplaceSubdag(DAGCircuit dag, List<DAGOpNode> oldRun, DAGCircuit newDag)
    {
        var nodeMap = dag.SubstituteNodeWithDag(oldRun[0], newDag);
        foreach (var node in oldRun.Skip(1))
        {
            dag.RemoveOpNode(node);
        }

        var splicedRun = newDag.TopologicalOpNodes().Select(nodeMap.GetValueOrDefault).ToList();
        MovList(oldRun, splicedRun);
    }

    public bool Step(DAGCircuit dag)
    {
        var runs = dag.Collect1qRuns();
        bool didWork = false;

        foreach (var run in runs)
        {
            if (!run.Any()) continue;

            var runClone = new List<DAGOpNode>(run);

            // Find and commute with preceding run
            var precedingBlocker = FindAdjoiningRun(dag, runs, run);
            var commutedPreceding = new List<DAGOpNode>();
            if (precedingBlocker.Any())
            {
                commutedPreceding, runClone = CommuteThrough(precedingBlocker.First(), runClone);
            }

            // Find and commute with succeeding run
            var succeedingBlocker = FindAdjoiningRun(dag, runs, run, front: false);
            var commutedSucceeding = new List<DAGOpNode>();
            if (succeedingBlocker.Any())
            {
                runClone, commutedSucceeding = CommuteThrough(succeedingBlocker.First(), runClone, front: false);
            }

            // Resynthesize and check for optimization
            var qubit = dag.FindBit(run[0].Qargs[0]).Index;
            var newPrecedingRun = Resynthesize(precedingBlocker.Concat(commutedPreceding).ToList(), qubit);
            var newSucceedingRun = Resynthesize(commutedSucceeding.Concat(succeedingBlocker).ToList(), qubit);
            var newRun = Resynthesize(runClone, qubit);

            if (_optimize1q.SubstitutionChecks(dag, precedingBlocker.Concat(run).Concat(succeedingBlocker).ToList(),
                newPrecedingRun.OpNodes().Concat(newRun.OpNodes()).Concat(newSucceedingRun.OpNodes()).ToList(), _optimize1q.BasisGates, qubit))
            {
                if (newPrecedingRun != null && precedingBlocker.Any())
                    ReplaceSubdag(dag, precedingBlocker, newPrecedingRun);

                if (newSucceedingRun != null && succeedingBlocker.Any())
                    ReplaceSubdag(dag, succeedingBlocker, newSucceedingRun);

                if (newRun != null)
                    ReplaceSubdag(dag, run, newRun);

                didWork = true;
            }
        }

        return didWork;
    }

    public override DAGCircuit Run(DAGCircuit dag)
    {
        while (true)
        {
            bool didWork = Step(dag);
            if (!_runToCompletion || !didWork)
                break;
        }

        return dag;
    }

    public static void MovList(List<DAGOpNode> destination, List<DAGOpNode> source)
    {
        destination.Clear();
        destination.AddRange(source);
    }
}
