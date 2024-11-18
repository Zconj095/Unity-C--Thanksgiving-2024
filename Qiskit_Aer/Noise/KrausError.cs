using System;
using System.Collections.Generic;

public class KrausError
{
    public static QuantumError KrausErrorCall(List<Matrix> noiseOps, bool canonicalKraus = false)
    {
        if (noiseOps == null || noiseOps.Count == 0)
        {
            throw new ArgumentException("Invalid Kraus error input. NoiseOps must not be empty.");
        }

        Kraus kraus = new Kraus(noiseOps);

        if (canonicalKraus)
        {
            // Convert to Choi and back to get canonical Kraus
            kraus = new Kraus(new Choi(kraus));
        }

        return new QuantumError(kraus);
    }
}
