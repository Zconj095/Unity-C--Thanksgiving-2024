using System;
using System.Collections.Generic;

public class PulseGates : CalibrationBuilder
{
    private InstructionScheduleMap instMap;
    private Target target;

    /// <summary>
    /// Creates a new PulseGates pass.
    /// </summary>
    /// <param name="instMap">Instruction schedule map that the user may override.</param>
    /// <param name="target">The target representing the backend.</param>
    public PulseGates(InstructionScheduleMap instMap = null, Target target = null)
    {
        if (instMap == null && target == null)
        {
            throw new Exception("instMap and target cannot be None simultaneously.");
        }

        if (target == null)
        {
            target = new Target();
            target.UpdateFromInstructionScheduleMap(instMap);
        }

        this.target = target;
    }

    /// <summary>
    /// Determines if a given node supports the calibration.
    /// </summary>
    /// <param name="nodeOp">Target instruction object.</param>
    /// <param name="qubits">List of integer qubit indices to check.</param>
    /// <returns>True if calibration can be provided for the node.</returns>
    public override bool Supported(CircuitInst nodeOp, List<int> qubits)
    {
        return target.HasCalibration(nodeOp.Name, qubits.ToArray());
    }

    /// <summary>
    /// Gets the calibrated schedule for the given instruction and qubits.
    /// </summary>
    /// <param name="nodeOp">Target instruction object.</param>
    /// <param name="qubits">List of integer qubit indices to check.</param>
    /// <returns>The calibrated schedule for the gate instruction.</returns>
    /// <exception cref="Exception">Throws when the node is parameterized and calibration is raw schedule object.</exception>
    public override object GetCalibration(CircuitInst nodeOp, List<int> qubits)
    {
        return target.GetCalibration(nodeOp.Name, qubits.ToArray(), nodeOp.Params.ToArray());
    }
}