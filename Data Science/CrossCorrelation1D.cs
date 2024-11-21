using UnityEngine;

public class CrossCorrelation1D : MonoBehaviour
{
    [SerializeField] private float[] signal1 = { 1f, 2f, 3f, 4f, 5f };   // First signal
    [SerializeField] private float[] signal2 = { 5f, 4f, 3f, 2f, 1f };   // Second signal (to correlate with the first)

    private void Start()
    {
        if (signal1.Length == 0 || signal2.Length == 0)
        {
            Debug.LogError("Signals must not be empty.");
            return;
        }

        // Compute Cross-Correlation for the example signals
        float[] result = ComputeCrossCorrelation1D(signal1, signal2);

        // Output the result
        Debug.Log("Cross-Correlation Result:");
        for (int i = 0; i < result.Length; i++)
        {
            Debug.Log($"Lag {i - (signal2.Length - 1)}: {result[i]}");
        }
    }

    // Function to compute the cross-correlation between two 1D signals
    private float[] ComputeCrossCorrelation1D(float[] signal1, float[] signal2)
    {
        int length = signal1.Length + signal2.Length - 1;  // Length of the output correlation
        float[] correlation = new float[length];          // Array to store the result

        // Normalize the second signal (optional)
        signal2 = NormalizeSignal(signal2);

        // Compute the cross-correlation (signal1 * signal2)
        for (int i = 0; i < length; i++)
        {
            correlation[i] = 0f;

            for (int j = 0; j < signal1.Length; j++)
            {
                int signal2Index = i - j;

                // Check for index validity (i.e., within bounds of signal2)
                if (signal2Index >= 0 && signal2Index < signal2.Length)
                {
                    correlation[i] += signal1[j] * signal2[signal2Index];
                }
            }
        }

        return correlation;
    }

    // Function to normalize a signal (optional)
    private float[] NormalizeSignal(float[] signal)
    {
        float sum = 0f;
        foreach (float val in signal)
        {
            sum += val;
        }

        // Normalize to the mean of the signal
        float mean = sum / signal.Length;

        float[] normalizedSignal = new float[signal.Length];
        for (int i = 0; i < signal.Length; i++)
        {
            normalizedSignal[i] = signal[i] - mean;
        }

        return normalizedSignal;
    }
}
