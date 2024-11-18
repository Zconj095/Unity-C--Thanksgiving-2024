using System;
using System.Collections.Generic;
using System.Linq;

public class EchoRZXWeylDecomposition : TransformationPass
{
    private InstructionScheduleMap _instMap;

    public EchoRZXWeylDecomposition(InstructionScheduleMap instructionScheduleMap = null, Target target = null)
    {
        base();
        _instMap = instructionScheduleMap;

        if (target != null)
        {
            _instMap = target.InstructionScheduleMap();
        }
    }

    private bool IsNative(Tuple<int, int> qubitPair)
    {
        var (calType, _, _) = CheckCalibrationType(_instMap, qubitPair);
        return calType == CRCalType.ECR_CX_FORWARD || calType == CRCalType.ECR_FORWARD || calType == CRCalType.DIRECT_CX_FORWARD;
    }

    private static DAGCircuit EchoRzxDag(double theta)
    {
        // Create the circuit for the echoed RZX with the given theta value
        var rzxDag = new DAGCircuit();
        var qr = new QuantumRegister();
        rzxDag.AddQreg(qr);
        rzxDag.ApplyOperationBack(new RZXGate(theta / 2), new List<Qubit> { qr[0], qr[1] });
        rzxDag.ApplyOperationBack(new XGate(), new List<Qubit> { qr[0] });
        rzxDag.ApplyOperationBack(new RZXGate(-theta / 2), new List<Qubit> { qr[0], qr[1] });
        rzxDag.ApplyOperationBack(new XGate(), new List<Qubit> { qr[0] });

        return rzxDag;
    }

    private static DAGCircuit ReverseEchoRzxDag(double theta)
    {
        // Create the circuit for the reverse echoed RZX with the given theta value
        var reverseRzxDag = new DAGCircuit();
        var qr = new QuantumRegister();
        reverseRzxDag.AddQreg(qr);
        reverseRzxDag.ApplyOperationBack(new HGate(), new List<Qubit> { qr[0] });
        reverseRzxDag.ApplyOperationBack(new HGate(), new List<Qubit> { qr[1] });
        reverseRzxDag.ApplyOperationBack(new RZXGate(theta / 2), new List<Qubit> { qr[1], qr[0] });
        reverseRzxDag.ApplyOperationBack(new XGate(), new List<Qubit> { qr[1] });
        reverseRzxDag.ApplyOperationBack(new RZXGate(-theta / 2), new List<Qubit> { qr[1], qr[0] });
        reverseRzxDag.ApplyOperationBack(new XGate(), new List<Qubit> { qr[1] });
        reverseRzxDag.ApplyOperationBack(new HGate(), new List<Qubit> { qr[0] });
        reverseRzxDag.ApplyOperationBack(new HGate(), new List<Qubit> { qr[1] });

        return reverseRzxDag;
    }

    // Override the Run method to provide custom transformation logic for EchoRZXWeylDecomposition
    public override DAGCircuit Run(DAGCircuit dag)
    {
        if (dag.Qregs.Count > 1)
        {
            throw new TranspilerError(
                "EchoRZXWeylDecomposition expects a single qreg input DAG, but input DAG had qregs: " + string.Join(", ", dag.Qregs.Keys));
        }

        var trivialLayout = Layout.GenerateTrivialLayout(dag.Qregs.Values.ToArray());
        var decomposer = new TwoQubitControlledUDecomposer(new RZXGate(0));

        foreach (var node in dag.TwoQubitOps())
        {
            var unitary = new Operator(node.Op).Data;
            var dagWeyl = CircuitToDag(decomposer.Decompose(unitary));
            dag.SubstituteNodeWithDag(node, dagWeyl);
        }

        foreach (var node in dag.TwoQubitOps())
        {
            if (node.Name == "rzx")
            {
                var control = node.Qargs[0];
                var target = node.Qargs[1];

                var physicalQ0 = trivialLayout[control];
                var physicalQ1 = trivialLayout[target];

                var isNative = IsNative(new Tuple<int, int>(physicalQ0, physicalQ1));

                var theta = node.Op.Params[0];
                if (isNative)
                {
                    dag.SubstituteNodeWithDag(node, EchoRzxDag(theta));
                }
                else
                {
                    dag.SubstituteNodeWithDag(node, ReverseEchoRzxDag(theta));
                }
            }
        }

        return dag;
    }
}
