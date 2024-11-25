using UnityEngine;

public class ARCrossValidation
{
    private const int MinValidAttempts = 3; // Minimum attempts for validation
    private float[][] storedPatterns;

    public ARCrossValidation()
    {
        // Initialize stored patterns with dummy data
        storedPatterns = new float[MinValidAttempts][];
        for (int i = 0; i < MinValidAttempts; i++)
        {
            storedPatterns[i] = new float[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };
        }
    }

    public bool ValidatePattern(float[] inputPattern)
    {
        int validCount = 0;

        foreach (var storedPattern in storedPatterns)
        {
            if (ComparePatterns(storedPattern, inputPattern))
                validCount++;
        }

        return validCount >= MinValidAttempts;
    }

    private bool ComparePatterns(float[] stored, float[] input)
    {
        if (stored.Length != input.Length)
            return false;

        float threshold = 0.05f;
        float difference = 0f;

        for (int i = 0; i < stored.Length; i++)
        {
            difference += Mathf.Abs(stored[i] - input[i]);
        }

        return difference <= threshold;
    }
}
