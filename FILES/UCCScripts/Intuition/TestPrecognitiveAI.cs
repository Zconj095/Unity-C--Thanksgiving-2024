using System.Collections.Generic;
using UnityEngine;

public class TestPrecognitiveAI : MonoBehaviour
{
    private PrecognitiveAI ai;

    void Start()
    {
        // Create the AI component and attach it to the current GameObject
        ai = gameObject.AddComponent<PrecognitiveAI>();

        // Sample training data
        var trainingData = new Dictionary<string, List<string>> {
            { "meta_tasks", new List<string> { "task1", "task2" } },
            { "pattern_samples", new List<string> { "sample1", "sample2" } },
            { "inference_data", new List<string> { "sample1,0.8", "sample2,0.6" } },
            { "rl_experiences", new List<string> { "state1,10", "state2,5" } }
        };

        // Train the AI with the data
        ai.TrainIntuition(trainingData);

        // Use the AI to make a prediction based on new data
        string prediction = ai.ApplyIntuition("new_data_sample");
        Debug.Log("Precognitive Prediction: " + prediction);

        // Update memory with a new experience
        ai.UpdateMemory("new_experience");
    }
}
