using System;
using System.Collections.Generic;

public class NormalizeRXAngle
{
    public object Target { get; set; }  // Placeholder for target device or class
    public double ResolutionInRadian { get; set; }
    private Dictionary<int, List<double>> AlreadyGenerated { get; set; }

    public NormalizeRXAngle(object target = null, double resolutionInRadian = 0)
    {
        Target = target;
        ResolutionInRadian = resolutionInRadian;
        AlreadyGenerated = new Dictionary<int, List<double>>();
    }

    // Quantizes the RX rotation angles based on the specified resolution
    public double QuantizeAngles(int qubitId, double originalAngle)
    {
        if (!AlreadyGenerated.ContainsKey(qubitId))
        {
            AlreadyGenerated[qubitId] = new List<double> { originalAngle };
            return originalAngle;
        }

        var angles = AlreadyGenerated[qubitId];
        var similarAngles = angles.FindAll(angle => Math.Abs(angle - originalAngle) <= (ResolutionInRadian / 2));

        if (similarAngles.Count == 0)
        {
            angles.Add(originalAngle);
            return originalAngle;
        }

        return similarAngles[0];
    }

    public void NormalizeRXGate(ref double theta, int qubitId)
    {
        // Wrap RX Gate theta into [0, pi]
        double wrappedTheta = Math.Atan2(Math.Sin(theta), Math.Cos(theta)); // Normalize to [-pi, pi]

        if (ResolutionInRadian > 0)
            wrappedTheta = QuantizeAngles(qubitId, wrappedTheta);

        // Check if it can be replaced by SX or X gates
        bool halfPiRotation = Math.Abs(Math.Abs(wrappedTheta) - Math.PI / 2) < (ResolutionInRadian / 2);
        bool piRotation = Math.Abs(Math.Abs(wrappedTheta) - Math.PI) < (ResolutionInRadian / 2);

        if (halfPiRotation)
        {
            Console.WriteLine("Replace with SX Gate");
        }
        else if (piRotation)
        {
            Console.WriteLine("Replace with X Gate");
        }
        else
        {
            // If the angle is negative, add Rz gates around RX.
            if (wrappedTheta < 0)
            {
                Console.WriteLine("Wrap RX with RZ(π) and RZ(-π) gates.");
            }

            Console.WriteLine($"Apply RX with normalized angle: {Math.Abs(wrappedTheta)}");
        }

        // Update theta after normalization
        theta = wrappedTheta;
    }

    public void Run(List<double> rxGates, Dictionary<int, double> qubitAngles)
    {
        foreach (var gateAngle in rxGates)
        {
            foreach (var qubit in qubitAngles.Keys)
            {
                NormalizeRXGate(ref gateAngle, qubit);
            }
        }
    }
}

