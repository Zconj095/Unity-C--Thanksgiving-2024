using System;
using System.Collections.Generic;
using System.Linq;

public class RXCalibrationBuilder : CalibrationBuilder
{
    private Target target;
    private Dictionary<string, bool> alreadyGenerated;

    /// <summary>
    /// Bootstrap single-pulse RX gate calibrations from the hardware-calibrated SX gate calibration.
    /// </summary>
    /// <param name="target">The target object containing the SX calibration used for bootstrapping RX calibrations.</param>
    public RXCalibrationBuilder(Target target)
    {
        if (target == null)
        {
            throw new ArgumentException("Target cannot be null.");
        }

        this.target = target;
        this.alreadyGenerated = new Dictionary<string, bool>();
        // Assuming NormalizeRXAngle pass is handled elsewhere in the transpiler pipeline
    }

    /// <summary>
    /// Checks if the calibration for an RX gate can be provided.
    /// </summary>
    /// <param name="nodeOp">The instruction representing the RX gate.</param>
    /// <param name="qubits">The qubits involved in the operation.</param>
    /// <returns>True if the calibration can be provided for this node and qubits.</returns>
    public override bool Supported(CircuitInst nodeOp, List<int> qubits)
    {
        return nodeOp is RXGate &&
               target.HasCalibration("sx", qubits.ToArray()) &&
               target.GetCalibration("sx", qubits.ToArray()).Instructions.Count == 1 &&
               target.GetCalibration("sx", qubits.ToArray()).Instructions[0][1].Pulse is ScalableSymbolicPulse &&
               target.GetCalibration("sx", qubits.ToArray()).Instructions[0][1].Pulse.PulseType == "Drag";
    }

    /// <summary>
    /// Generates the RX calibration for the specified rotation angle and qubits.
    /// </summary>
    /// <param name="nodeOp">The RX gate instruction to calibrate.</param>
    /// <param name="qubits">The qubits involved in the RX gate operation.</param>
    /// <returns>The generated RX calibration schedule.</returns>
    public override object GetCalibration(CircuitInst nodeOp, List<int> qubits)
    {
        // Normalize the rotation angle to [0, pi] range (assuming NormalizeRXAngles pass has already been run)
        double angle;
        try
        {
            angle = Convert.ToDouble(nodeOp.Params[0]);
        }
        catch (Exception ex)
        {
            throw new Exception("Target rotation angle is not assigned.", ex);
        }

        var sxCalibration = target.GetCalibration("sx", qubits.ToArray());
        var paramsCopy = sxCalibration.Instructions[0][1].Pulse.Parameters.Copy();

        return CreateRXSchedule(angle, sxCalibration.Channels[0], paramsCopy["duration"], paramsCopy["amp"], paramsCopy["sigma"], paramsCopy["beta"]);
    }

    private object CreateRXSchedule(double rxAngle, Channel channel, double duration, double amp, double sigma, double beta)
    {
        // Scale the amplitude based on the RX angle
        double newAmp = rxAngle / (Math.PI / 2) * amp;

        // Create the RX pulse schedule (pseudo code for pulse builder)
        var rxSchedule = new Schedule();
        rxSchedule.Play(new Drag(duration, newAmp, sigma, beta, 0), channel);

        return rxSchedule;
    }
}