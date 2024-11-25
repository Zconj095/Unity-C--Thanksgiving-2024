using System.Collections.Generic;
using System.Linq;
using System;
public class FeedbackSystem
{
    public static float[] ApplyFeedback(float[] vector, float[] feedback, float learningRate)
    {
        float[] updatedVector = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            updatedVector[i] = vector[i] + learningRate * feedback[i];
        }
        return updatedVector;
    }

    public static float[] GenerateFeedback(float[] source, float[] target, Func<float, float, float> feedbackFunction)
    {
        float[] feedback = new float[source.Length];
        for (int i = 0; i < feedback.Length; i++)
        {
            feedback[i] = feedbackFunction(source[i], target[i]);
        }
        return feedback;
    }
}
