using System.Collections.Generic;
using System.Linq;
using System;
public class OriginSynchronizer
{
    public static void SynchronizeFeedback(
        OriginLocation originA,
        OriginLocation originB,
        Func<float, float, float> feedbackFunction,
        float learningRate)
    {
        float[] feedback = new float[originA.StateVector.Length];
        for (int i = 0; i < feedback.Length; i++)
        {
            feedback[i] = feedbackFunction(originA.StateVector[i], originB.StateVector[i]);
        }

        originA.ApplyFeedback(feedback, learningRate);
        originB.ApplyFeedback(feedback, learningRate);
    }
}
