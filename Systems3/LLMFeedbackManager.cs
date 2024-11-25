using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class LLMFeedbackManager
{
    private Dictionary<string, float> feedbackScores;

    public LLMFeedbackManager()
    {
        feedbackScores = new Dictionary<string, float>();
    }

    public void RecordFeedback(string response, float score)
    {
        if (feedbackScores.ContainsKey(response))
        {
            feedbackScores[response] = (feedbackScores[response] + score) / 2; // Average feedback
        }
        else
        {
            feedbackScores[response] = score;
        }
    }

    public float GetFeedbackScore(string response)
    {
        return feedbackScores.ContainsKey(response) ? feedbackScores[response] : 0.5f; // Neutral by default
    }
}
