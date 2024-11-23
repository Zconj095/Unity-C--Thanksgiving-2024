using UnityEngine;

public class RhythmCalculator
{
    /// <summary>
    /// Calculates the tempo (speed) of speech in beats per minute (BPM).
    /// </summary>
    public static float CalculateTempo(float speechRate)
    {
        // Convert speech rate (words per second) to beats per minute
        return speechRate * 60f;
    }

    /// <summary>
    /// Calculates the pause frequency (number of pauses per second).
    /// </summary>
    public static float CalculatePauseFrequency(float[] samples, int sampleRate)
    {
        int pauseCount = 0;
        int consecutiveSilence = 0;
        int threshold = 500; // Minimum length of silence to consider a pause

        for (int i = 0; i < samples.Length; i++)
        {
            if (Mathf.Abs(samples[i]) < 0.01f)
            {
                consecutiveSilence++;
                if (consecutiveSilence >= threshold)
                {
                    pauseCount++;
                    consecutiveSilence = 0;
                }
            }
            else
            {
                consecutiveSilence = 0;
            }
        }

        float totalTimeInSeconds = samples.Length / (float)sampleRate;
        return pauseCount / totalTimeInSeconds; // Pauses per second
    }

    /// <summary>
    /// Calculates the variability in speech rhythm based on time intervals between voiced segments.
    /// </summary>
    public static float CalculateRhythmVariability(float[] samples, int sampleRate)
    {
        int voicedSegmentCount = 0;
        float totalDuration = 0f;
        bool isVoiced = false;
        float currentSegmentDuration = 0f;

        for (int i = 0; i < samples.Length; i++)
        {
            if (Mathf.Abs(samples[i]) > 0.01f)
            {
                if (!isVoiced)
                {
                    isVoiced = true;
                    voicedSegmentCount++;
                }
                currentSegmentDuration++;
            }
            else
            {
                if (isVoiced)
                {
                    isVoiced = false;
                    totalDuration += currentSegmentDuration / sampleRate;
                    currentSegmentDuration = 0f;
                }
            }
        }

        return totalDuration / voicedSegmentCount; // Average duration of voiced segments
    }
}
