using System;
using System.Collections.Generic;


[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class TrivialRecurseAttribute : Attribute
{
    public TrivialRecurseAttribute()
    {
    }
}


public class CommutativeCancellation : TransformationPass
{
    private readonly HashSet<string> _basis;
    private readonly Target _target;
    private readonly HashSet<string> _zRotations = new HashSet<string> { "p", "z", "u1", "rz", "t", "s" };
    private readonly HashSet<string> _xRotations = new HashSet<string> { "x", "rx" };
    private readonly HashSet<string> _gates = new HashSet<string> { "cx", "cy", "cz", "h", "y" };
    private readonly CommutationChecker _commutationChecker;

    private static readonly Dictionary<string, Type> _varZMap = new Dictionary<string, Type>
    {
        { "rz", typeof(RZGate) },
        { "p", typeof(PhaseGate) },
        { "u1", typeof(U1Gate) }
    };

    public CommutativeCancellation(List<string> basisGates = null, Target target = null)
    {
        if (basisGates != null)
        {
            _basis = new HashSet<string>(basisGates);
        }
        else
        {
            _basis = new HashSet<string>();
        }

        _target = target;

        if (_target != null)
        {
            _basis = new HashSet<string>(_target.OperationNames);
        }

        _commutationChecker = new CommutationChecker(
            StandardGateCommutations, 
            gates: _gates.Union(_zRotations).Union(_xRotations)
        );
    }

    [TrivialRecurse]
    public override DAGCircuit Run(DAGCircuit dag)
    {
        // Call the commutation cancellation logic recursively
        // Let's simulate that the recursion checks whether the DAG needs to be simplified more.
        bool simplified = CommutationCancellation.CancelCommutations(dag, _commutationChecker, _basis);

        // If the DAG was simplified, we recurse again.
        if (simplified)
        {
            return Run(dag);  // Recursive call if changes are made
        }

        return dag;
    }
}
