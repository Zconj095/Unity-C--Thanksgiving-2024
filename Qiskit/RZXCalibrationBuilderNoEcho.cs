using System;
using System.Collections.Generic;
public class RZXCalibrationBuilderNoEcho : RZXCalibrationBuilder
{
    public RZXCalibrationBuilderNoEcho(InstructionScheduleMap instructionScheduleMap, bool verbose = true, Target target = null)
        : base(instructionScheduleMap, verbose, target)
    {
    }

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
            using (var builder = new PulseBuilder())
            {
                builder.StartNewSchedule("rzx(" + theta.ToString("F3") + ")");
                RescaleCRInst(crTones[0], 2 * theta);
                RescaleCRInst(compTones[0], 2 * theta);
                builder.Delay(RescaleCRInst(crTones[0], 2 * theta), qubits[0]);
                return builder.Build();
            }
        }

        throw new Exception("RZXCalibrationBuilderNoEcho only supports hardware-native RZX gates.");
    }
}
