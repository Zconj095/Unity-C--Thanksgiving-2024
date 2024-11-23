public class MoodClassifier
{
    public static string ClassifyMood(float pitch, float energy, float volume, float speechRate, float[] spectralContent)
    {
        if (pitch < 200 && energy < 0.3f && volume < 0.3f && speechRate < 2f)
            return "CALM";
        else if (pitch > 300 && energy > 0.6f && speechRate > 3f)
            return "CHEERFUL";
        else if (energy < 0.4f && volume < 0.4f && speechRate < 2f)
            return "CONTENT";
        else if (pitch > 250 && energy > 0.7f && volume > 0.5f)
            return "DEFENSIVE";
        else if (pitch > 300 && energy > 0.8f && volume > 0.7f && speechRate > 4f)
            return "ENERGETIC";
        else if (pitch > 300 && energy > 0.8f && volume > 0.6f)
            return "EXCITED";
        else if (pitch < 200 && energy < 0.4f && speechRate < 2f)
            return "GRATEFUL";
        else if (pitch > 300 && energy > 0.6f && spectralContent[1] > 0.5f)
            return "JOYFUL";
        else if (volume > 0.7f && energy > 0.6f)
            return "LOUD";
        else if (pitch < 200 && energy < 0.3f && speechRate < 2f)
            return "MELLOW";
        else if (speechRate > 2f && energy > 0.5f)
            return "NARRATIVE";
        else if (energy > 0.5f && volume < 0.5f)
            return "NEUTRAL";
        else if (energy < 0.3f && volume < 0.3f && speechRate < 2f)
            return "PEACEFUL";
        else if (volume < 0.2f)
            return "QUIET";
        else if (energy < 0.3f && speechRate < 2f)
            return "RELAXED";
        else if (speechRate > 5f && energy > 0.5f)
            return "TALKATIVE";

        return "UNKNOWN";
    }
}
