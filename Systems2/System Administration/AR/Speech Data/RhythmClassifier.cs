public class RhythmClassifier
{
    public static string ClassifyRhythm(float tempo, float pauseFrequency, float rhythmVariability)
    {
        if (tempo < 100 && pauseFrequency < 0.5f && rhythmVariability < 0.5f)
            return "STEADY";
        else if (tempo >= 100 && pauseFrequency >= 0.5f && rhythmVariability < 1.0f)
            return "VARIABLE";
        else if (tempo > 150 && pauseFrequency > 1.0f && rhythmVariability > 1.5f)
            return "ERRATIC";
        else if (tempo < 80 && pauseFrequency > 0.7f)
            return "SLOW_AND_PAUSED";
        else if (tempo > 120 && pauseFrequency < 0.3f)
            return "FAST_AND_CONTINUOUS";
        else if (rhythmVariability > 2.0f)
            return "UNPREDICTABLE";

        return "UNKNOWN";
    }
}
