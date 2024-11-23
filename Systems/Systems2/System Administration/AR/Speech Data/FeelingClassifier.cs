public class FeelingClassifier
{
    public static string ClassifyFeeling(float pitch, float energy, float volume, float speechRate, float[] spectralContent)
    {
        if (pitch > 300 && energy > 0.6f && volume < 0.4f && speechRate < 3f)
            return "ACCEPTANCE";
        else if (pitch > 250 && energy > 0.5f && volume < 0.4f && speechRate > 2f)
            return "ADMIRATION";
        else if (pitch < 200 && energy > 0.5f && volume < 0.3f && speechRate < 2f)
            return "AFFECTIONATE";
        else if (pitch < 200 && energy > 0.4f && volume < 0.3f && speechRate < 2f)
            return "ALTRUISTIC";
        else if (pitch > 300 && energy > 0.7f && volume > 0.4f && speechRate > 3f)
            return "AMUSED";
        else if (pitch < 250 && energy > 0.3f && volume < 0.4f && speechRate < 2f)
            return "BEAUTIFUL";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "BLESSED";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "BRAVE";
        else if (pitch < 200 && energy < 0.3f && volume < 0.3f && speechRate < 2f)
            return "CALM";
        else if (pitch > 300 && energy > 0.7f && volume > 0.5f && speechRate > 3f)
            return "CHEERFUL";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "COLLECTED";
        else if (pitch > 250 && energy > 0.5f && volume < 0.4f && speechRate < 2f)
            return "CONCERNED";
        else if (pitch > 200 && energy > 0.6f && volume > 0.5f && speechRate > 2f)
            return "CONFIDENT";
        else if (pitch < 200 && energy < 0.3f && volume < 0.3f && speechRate < 2f)
            return "CONTENT";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "COURAGEOUS";
        else if (pitch > 250 && energy > 0.6f && volume > 0.4f && speechRate < 3f)
            return "DEFENSIVE";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "DETERMINED";
        else if (pitch > 300 && energy > 0.7f && volume > 0.5f && speechRate > 3f)
            return "EAGER";
        else if (pitch > 250 && energy > 0.5f && volume > 0.5f && speechRate > 3f)
            return "EMOTIONAL";
        else if (pitch < 200 && energy > 0.4f && volume < 0.3f && speechRate < 2f)
            return "EMPATHIC";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "ENERGETIC";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "FAITH";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "FAITHFUL";
        else if (pitch > 300 && energy > 0.7f && volume > 0.5f && speechRate > 3f)
            return "FASCINATED";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "FOCUSED";
        else if (pitch > 250 && energy > 0.6f && volume > 0.4f && speechRate > 2f)
            return "FULL_OF_PURPOSE";
        else if (pitch > 200 && energy > 0.5f && volume > 0.4f && speechRate > 2f)
            return "GOOD";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "GRACEFUL";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "GRATEFUL";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "GREAT";
        else if (pitch > 300 && energy > 0.7f && volume > 0.5f && speechRate > 3f)
            return "HAPPY";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "HONOR";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "HONORABLE";
        else if (pitch > 250 && energy > 0.6f && volume > 0.4f && speechRate > 2f)
            return "HOPEFUL";
        else if (pitch > 300 && energy > 0.7f && volume > 0.5f && speechRate > 3f)
            return "IMPRESSED";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "INDEPENDENT";
        else if (pitch > 300 && energy > 0.7f && volume > 0.5f && speechRate > 3f)
            return "LIKED";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "LOVED";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "MOTIVATED";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "NEUTRAL";
        else if (pitch > 250 && energy > 0.6f && volume > 0.5f && speechRate > 2f)
            return "OFFENSIVE";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "OPTIMISTIC";
        else if (pitch > 250 && energy > 0.6f && volume > 0.4f && speechRate > 2f)
            return "POSITIVE";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "POWERFUL";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "PROUD";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "RELAXED";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "RELIEF";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "SELFLESS";
        else if (pitch > 300 && energy > 0.7f && volume > 0.5f && speechRate > 3f)
            return "SENSATIONAL";
        else if (pitch > 250 && energy > 0.6f && volume > 0.5f && speechRate > 2f)
            return "SENSITIVE";
        else if (pitch > 250 && energy > 0.5f && volume > 0.4f && speechRate > 2f)
            return "SOCIAL";
        else if (pitch > 250 && energy > 0.5f && volume > 0.4f && speechRate > 2f)
            return "SPECIAL";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "STRONG";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "SURPRISED";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "SYMPATHETIC";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "THANKFUL";
        else if (pitch < 200 && energy < 0.4f && volume < 0.3f && speechRate < 2f)
            return "THOUGHTFUL";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f && speechRate > 3f)
            return "THRILLED";
        else if (pitch > 250 && energy > 0.6f && volume > 0.4f && speechRate > 2f)
            return "UNIQUE";

        return "UNKNOWN";
    }
}
