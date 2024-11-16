using System;
using System.Collections.Generic;
using System.Linq;

public class Optimize1qGates : TransformationPass
{
    private HashSet<string> basis;
    private double eps;
    private Target target;

    private const double CHOP_THRESHOLD = 1e-15;

    /// <summary>
    /// Initializes the Optimize1qGates pass.
    /// </summary>
    public Optimize1qGates(List<string> basis = null, double eps = 1e-15, Target target = null)
    {
        this.basis = basis != null ? new HashSet<string>(basis) : new HashSet<string> { "u1", "u2", "u3" };
        this.eps = eps;
        this.target = target;
    }

    public override DAGCircuit Run(DAGCircuit dag)
    {
        // Perform optimization pass on the circuit DAG
        var runs = dag.CollectRuns(new List<string> { "u1", "u2", "u3", "u", "p" });
        runs = SplitRunsOnParameters(runs);

        foreach (var run in runs)
        {
            // Identify the best gate to combine the gates
            string rightName = DetermineRightGate(run);
            var rightParameters = new double[] { 0, 0, 0 }; // (theta, phi, lambda)
            double rightGlobalPhase = 0;

            foreach (var currentNode in run)
            {
                var leftName = currentNode.Name;
                double[] leftParameters = GetGateParameters(currentNode);

                if (currentNode.HasGlobalPhase())
                {
                    rightGlobalPhase += currentNode.GlobalPhase;
                }

                // Compose gates based on their types
                (rightName, rightParameters) = CombineGates(leftName, leftParameters, rightName, rightParameters);
            }

            // Handle gate selection based on target backend and available gates
            OptimizeGateSelection(ref rightName, ref rightParameters, run);

            // Create a new optimized gate
            Operation newOp = CreateOptimizedGate(rightName, rightParameters);

            // Substitute the optimized operation in the DAG
            dag.GlobalPhase += rightGlobalPhase;
            dag.SubstituteNode(run[0], newOp);

            // Remove redundant nodes
            RemoveRedundantNodes(dag, run);
        }

        return dag;
    }

    private string DetermineRightGate(List<DAGOpNode> run)
    {
        // Determine which gate should be used based on the available gates and target backend
        if (target != null)
        {
            var runQubits = run.Select(x => target.FindBit(x.Qargs[0]).Index).ToArray();
            if (target.IsInstructionSupported("p", runQubits))
            {
                return "p";
            }
            return "u1";
        }

        return "p";
    }

    private double[] GetGateParameters(DAGOpNode currentNode)
    {
        // Retrieve the parameters for the gate in the current node
        var currentOp = currentNode.Op;
        string leftName = currentOp.Name;

        double[] parameters = new double[3];

        if (leftName == "u1" || leftName == "p")
        {
            parameters = new double[] { 0, 0, currentOp.Params[0] };
        }
        else if (leftName == "u2")
        {
            parameters = new double[] { Math.PI / 2, currentOp.Params[0], currentOp.Params[1] };
        }
        else if (leftName == "u3" || leftName == "u")
        {
            parameters = currentOp.Params.ToArray();
        }
        return parameters;
    }

    private (string, double[]) CombineGates(string leftName, double[] leftParameters, string rightName, double[] rightParameters)
    {
        // Combine two gates based on their names and parameters
        if (leftName == "u1" && rightName == "u1" || leftName == "p" && rightName == "p")
        {
            rightParameters[2] += leftParameters[2]; // u1 or p lambda addition
        }
        else if (leftName == "u1" && rightName == "u2" || leftName == "p" && rightName == "u2")
        {
            rightParameters[1] += leftParameters[2]; // u2 phi addition
        }
        else if (leftName == "u2" && rightName == "u1" || leftName == "u2" && rightName == "p")
        {
            rightName = "u2";
            rightParameters[2] += leftParameters[2]; // u2 lambda addition
        }
        else if (leftName == "u1" && rightName == "u3" || leftName == "p" && rightName == "u3")
        {
            rightParameters[1] += leftParameters[2]; // u3 phi addition
        }
        else if (leftName == "u3" && rightName == "u1" || leftName == "u" && rightName == "u1")
        {
            rightParameters[1] += leftParameters[2]; // u3 phi addition
        }

        return (rightName, rightParameters);
    }

    private void OptimizeGateSelection(ref string rightName, ref double[] rightParameters, List<DAGOpNode> run)
    {
        // Choose the most appropriate gate based on available basis and the backend target
        if (target != null)
        {
            if (rightName == "u2" && !target.IsInstructionSupported("u2", run.Select(x => target.FindBit(x.Qargs[0]).Index).ToArray()))
            {
                if (target.IsInstructionSupported("u", run.Select(x => target.FindBit(x.Qargs[0]).Index).ToArray()))
                {
                    rightName = "u";
                }
                else
                {
                    rightName = "u3";
                }
            }
            if ((rightName == "u1" || rightName == "p") && !target.IsInstructionSupported(rightName, run.Select(x => target.FindBit(x.Qargs[0]).Index).ToArray()))
            {
                if (target.IsInstructionSupported("u", run.Select(x => target.FindBit(x.Qargs[0]).Index).ToArray()))
                {
                    rightName = "u";
                }
                else
                {
                    rightName = "u3";
                }
            }
        }
        else
        {
            if (rightName == "u2" && !basis.Contains("u2"))
            {
                rightName = "u3";
            }
            if ((rightName == "u1" || rightName == "p") && !basis.Contains(rightName))
            {
                rightName = "u3";
            }
        }
    }

    private Operation CreateOptimizedGate(string rightName, double[] rightParameters)
    {
        // Create the optimized gate based on the selected gate name and parameters
        if (rightName == "u1")
        {
            return new U1Gate(rightParameters[2]);
        }
        if (rightName == "p")
        {
            return new PhaseGate(rightParameters[2]);
        }
        if (rightName == "u2")
        {
            return new U2Gate(rightParameters[1], rightParameters[2]);
        }
        if (rightName == "u")
        {
            return new UGate(rightParameters[0], rightParameters[1], rightParameters[2]);
        }
        if (rightName == "u3")
        {
            return new U3Gate(rightParameters[0], rightParameters[1], rightParameters[2]);
        }
        throw new Exception($"Unsupported gate: {rightName}");
    }

    private void RemoveRedundantNodes(DAGCircuit dag, List<DAGOpNode> run)
    {
        // Remove redundant nodes after substitution
        for (int i = 1; i < run.Count; i++)
        {
            dag.RemoveOpNode(run[i]);
        }
        if (run.Count > 0 && run[0].Op.Name == "nop")
        {
            dag.RemoveOpNode(run[0]);
        }
    }

    private static List<List<DAGOpNode>> SplitRunsOnParameters(List<List<DAGOpNode>> runs)
    {
        // This function splits the runs of parameterized gates into smaller runs
        var outRuns = new List<List<DAGOpNode>>();
        foreach (var run in runs)
        {
            // Split the runs based on parameterized gates (e.g., u3, u gates)
            var groups = run.GroupBy(x => x.Op.IsParameterized() && (x.Op.Name == "u3" || x.Op.Name == "u")).ToList();
            foreach (var group in groups)
            {
                outRuns.Add(group.ToList());
            }
        }
        return outRuns;
    }
}
