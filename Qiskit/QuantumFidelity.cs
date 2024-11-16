using System;
using System.Linq;
using UnityEngine;

public class QuantumFidelity
{
    private static readonly UnityEngine.Logger logger = new UnityEngine.Logger(UnityEngine.LogType.Log);

    public static float ProcessFidelity(Operator channel, Operator target = null, bool requireCp = true, bool requireTp = true)
    {
        // Format inputs
        channel = InputFormatter(channel, "ProcessFidelity", "channel");
        target = InputFormatter(target, "ProcessFidelity", "target");

        if (target != null)
        {
            // Validate dimensions
            if (channel.Dimension != target.Dimension)
            {
                throw new Exception("Input quantum channel and target unitary must have the same dimensions.");
            }
        }

        // Validate complete-positivity and trace-preserving
        ValidateChannelProperties("Input", channel, requireCp, requireTp);
        if (target != null)
        {
            ValidateChannelProperties("Target", target, requireCp, requireTp);
        }

        if (target is Operator)
        {
            // Compute fidelity with unitary target by applying the inverse
            channel = channel.Compose(target.Adjoint());
            target = null;
        }

        int inputDim = channel.Dimension;
        if (target == null)
        {
            // Compute process fidelity with identity channel
            float fid = channel is Operator opChannel ? 
                MathF.Abs(opChannel.Trace() / inputDim) * MathF.Abs(opChannel.Trace() / inputDim) : 
                SuperOpFromChannel(channel).Trace() / (inputDim * inputDim);

            return fid;
        }

        // Compute fidelity between two non-unitary channels (using normalized Choi-matrices)
        DensityMatrix state1 = new DensityMatrix(Choi(channel).Data / inputDim);
        DensityMatrix state2 = new DensityMatrix(Choi(target).Data / inputDim);
        return StateFidelity(state1, state2);
    }

    public static float AverageGateFidelity(QuantumChannel channel, Operator target = null, bool requireCp = true, bool requireTp = false)
    {
        channel = InputFormatter(channel, "AverageGateFidelity", "channel");
        target = InputFormatter(target, "AverageGateFidelity", "target");

        if (target != null)
        {
            if (!(target is Operator))
            {
                throw new Exception("Target channel is not a unitary channel.");
            }
        }

        int dim = channel.Dimension;
        float fPro = ProcessFidelity(channel, target, requireCp, requireTp);
        return (dim * fPro + 1) / (dim + 1);
    }

    public static float GateError(QuantumChannel channel, Operator target = null, bool requireCp = true, bool requireTp = false)
    {
        channel = InputFormatter(channel, "GateError", "channel");
        target = InputFormatter(target, "GateError", "target");
        return 1 - AverageGateFidelity(channel, target, requireCp, requireTp);
    }

    private static void ValidateChannelProperties(string label, Operator channel, bool requireCp, bool requireTp)
    {
        if (requireCp)
        {
            var cpCond = CpCondition(channel);
            var neg = cpCond < -1 * channel.Atol;
            if (neg.Any())
            {
                logger.LogWarning($"{label} channel is not CP. Choi-matrix has negative eigenvalues: {string.Join(", ", neg)}");
            }
        }

        if (requireTp)
        {
            var tpCond = TpCondition(channel);
            var nonZero = tpCond.Where(x => MathF.Abs(x) > channel.Atol).ToArray();
            if (nonZero.Any())
            {
                logger.LogWarning($"{label} channel is not TP. Tr_2[Choi] - I has non-zero eigenvalues: {string.Join(", ", nonZero)}");
            }
        }
    }

    private static float[] CpCondition(Operator channel)
    {
        // Implement the Choi-matrix eigenvalue calculation for CP condition
        return new float[0];
    }

    private static float[] TpCondition(Operator channel)
    {
        // Implement the partial trace Choi-matrix eigenvalue calculation for TP condition
        return new float[0];
    }

    private static Operator InputFormatter(Operator obj, string funcName, string argName)
    {
        // Handle input formatting for different types
        if (obj == null) return obj;
        if (obj is QuantumChannel) return obj.ToOperator();
        if (obj is Gate || obj is BaseOperator) return new Operator(obj);
        throw new ArgumentException($"Invalid type supplied to {argName} of {funcName}. Expected an Operator or QuantumChannel.");
    }

    private static SuperOp SuperOpFromChannel(Operator channel)
    {
        // Assuming SuperOp is another class, convert channel to SuperOp
        return new SuperOp(channel);
    }

    private static DensityMatrix Choi(QuantumChannel channel)
    {
        // Choi-matrix conversion
        return new DensityMatrix(channel.Data);
    }

    private static float StateFidelity(DensityMatrix state1, DensityMatrix state2)
    {
        // Calculate the state fidelity between two density matrices
        return 0f; // Placeholder for fidelity calculation
    }
}
