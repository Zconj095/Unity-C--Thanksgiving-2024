using System.Collections.Generic;
using UnityEngine;

public class EmotionalFluxEngine
{
    private Queue<string> recentEmotions = new Queue<string>();
    private const int maxTrackedEmotions = 5;

    public void UpdateEmotion(string currentEmotion)
    {
        if (recentEmotions.Count >= maxTrackedEmotions)
        {
            recentEmotions.Dequeue();
        }
        recentEmotions.Enqueue(currentEmotion);
    }

    public string DetectFlux()
    {
        if (recentEmotions.Count < 2) return "STABLE";

        string[] emotions = recentEmotions.ToArray();
        if (emotions[0] != emotions[emotions.Length - 1])
        {
            return "SHIFTING";
        }

        return "STABLE";
    }
}
