using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;


public class TrainingDataGenerator
{
    public List<(string input, string output)> GenerateTrainingPairs(ConversationHistory conversation)
    {
        List<(string input, string output)> trainingPairs = new List<(string input, string output)>();

        string context = "";
        foreach (var message in conversation.Messages)
        {
            if (message.Sender == "User")
            {
                context += message.Message + " ";
            }
            else if (message.Sender == "AI")
            {
                trainingPairs.Add((context.Trim(), message.Message));
                context = ""; // Reset context after AI response
            }
        }

        return trainingPairs;
    }
}
