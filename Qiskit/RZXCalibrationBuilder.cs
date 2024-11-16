using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum CRCalType
{
    ECR_FORWARD = 1,
    ECR_REVERSE,
    ECR_CX_FORWARD,
    ECR_CX_REVERSE,
    DIRECT_CX_FORWARD,
    DIRECT_CX_REVERSE
}

public class RZXCalibrationBuilder : CalibrationBuilder
{
    private InstructionScheduleMap instMap;
    private bool verbose;
    private Target target;

    /// <summary>
    /// Initializes a RZXGate calibration builder.
    /// </summary>
    /// <param name="instructionScheduleMap">Instruction schedule map representing the backend's pulse calibrations.</param>
    /// <param name="verbose">If true, will raise warnings when RZX schedule cannot be built.</param>
    /// <param name="target">Target backend to use for calibration.</param>
    public RZXCalibrationBuilder(InstructionScheduleMap instructionScheduleMap = null, bool verbose = true, Target target = null)
    {
        this.instMap = instructionScheduleMap;
        this.verbose = verbose;
        if (target != null)
        {
            this.instMap = target.GetInstructionScheduleMap();
        }

        if (this.instMap == null)
        {
            throw new Exception("Calibrations can only be added to Pulse-enabled backends.");
        }
    }

    /// <summary>
    /// Checks if the given node supports calibration.
    /// </summary>
    /// <param name="nodeOp">The node operation (RZXGate).</param>
    /// <param name="qubits">The qubits involved.</param>
    /// <returns>True if calibration can be provided for the node.</returns>
    public override bool Supported(CircuitInst nodeOp, List<int> qubits)
    {
        return nodeOp is RZXGate && (instMap.HasInstruction("cx", qubits) || instMap.HasInstruction("ecr", qubits));
    }

    /// <summary>
    /// Rescales the cross resonance instruction for the specified angle and creates a stretched or compressed pulse.
    /// </summary>
    /// <param name="instruction">The original instruction.</param>
    /// <param name="theta">The desired rotation angle.</param>
    /// <param name="sampleMult">Sample multiplier for the pulse duration.</param>
    /// <returns>The duration of the stretched pulse.</returns>
    public static int RescaleCRInst(Play instruction, float theta, int sampleMult = 16)
    {
        if (theta == 0)
            throw new Exception("Target rotation angle is not assigned.");

        var paramsCopy = new Dictionary<string, float>(instruction.Pulse.Parameters);
        float risefallSigmaRatio = (paramsCopy["duration"] - paramsCopy["width"]) / paramsCopy["sigma"];

        // The error function used here adjusts for the Gaussian pulse rounding error.
        float risefallArea = paramsCopy["sigma"] * Math.Sqrt(2 * Math.PI) * Erf(risefallSigmaRatio);
        float fullArea = paramsCopy["width"] + risefallArea;

        // Assume this is a pi/2 controlled rotation
        float calAngle = (float)Math.PI / 2;
        float targetArea = Math.Abs(theta) / calAngle * fullArea;
        float newWidth = targetArea - risefallArea;

        if (newWidth >= 0)
        {
            paramsCopy["amp"] *= Math.Sign(theta);
            paramsCopy["width"] = newWidth;
        }
        else
        {
            paramsCopy["amp"] *= Math.Sign(theta) * targetArea / risefallArea;
            paramsCopy["width"] = 0;
        }

        int roundDuration = (int)Math.Round((paramsCopy["width"] + risefallSigmaRatio * paramsCopy["sigma"]) / sampleMult) * sampleMult;
        paramsCopy["duration"] = roundDuration;

        var newPulse = new GaussianSquare(paramsCopy);
        instruction.Play(newPulse, instruction.Channel);

        return roundDuration;
    }

    /// <summary>
    /// Builds the RZXGate calibration schedule based on the control and target qubits.
    /// </summary>
    /// <param name="nodeOp">The node operation (RZXGate).</param>
    /// <param name="qubits">The qubits involved in the operation.</param>
    /// <returns>The generated calibration schedule.</returns>
    public override object GetCalibration(CircuitInst nodeOp, List<int> qubits)
    {
        float theta = Convert.ToSingle(nodeOp.Params[0]);

        if (theta == 0.0f)
        {
            return new ScheduleBlock("rzx(0.000)");
        }

        var (calType, crTones, compTones) = CheckCalibrationType(instMap, qubits);

        if (calType == CRCalType.DIRECT_CX_FORWARD || calType == CRCalType.DIRECT_CX_REVERSE)
        {
            if (verbose)
            {
                Console.WriteLine($"CR instruction for qubits {string.Join(",", qubits)} is likely {calType} sequence. " +
                    "Pulse stretch for this calibration is not currently implemented.");
            }
            throw new Exception("Calibration not available.");
        }

        if (calType == CRCalType.ECR_CX_FORWARD || calType == CRCalType.ECR_FORWARD)
        {
            // Implementing forward ECR calibration with echo
            using (var builder = new PulseBuilder())
            {
                builder.StartNewSchedule("rzx(" + theta.ToString("F3") + ")");
                for (int i = 0; i < crTones.Count; i++)
                {
                    RescaleCRInst(crTones[i], theta);
                    RescaleCRInst(compTones[i], theta);
                    builder.Call(instMap.Get("x", qubits[0]));
                }
                return builder.Build();
            }
        }

        // Handle other cases where the calibration type is not native
        throw new Exception("Only native RZX gates supported.");
    }

    private static (CRCalType, List<Play>, List<Play>) CheckCalibrationType(InstructionScheduleMap instSchedMap, List<int> qubits)
    {
        // Here, logic will determine the calibration type based on the given instruction schedule map and qubit pair
        CRCalType calType = CRCalType.ECR_FORWARD;  // Assuming ECR_FORWARD for simplicity
        List<Play> crTones = new List<Play>();  // Placeholder
        List<Play> compTones = new List<Play>();  // Placeholder
        return (calType, crTones, compTones);
    }
}