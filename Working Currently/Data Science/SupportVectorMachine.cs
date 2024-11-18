using System.Collections.Generic;
using UnityEngine;

public class SupportVectorMachine : MonoBehaviour
{
    [Header("SVM Parameters")]
    [SerializeField] private int numberOfFeatures;  // Number of features for input data
    [SerializeField] private float learningRate = 0.01f;  // Learning rate for gradient descent
    [SerializeField] private int epochs = 1000;  // Number of training iterations

    private float[] weights;  // Weights for the hyperplane
    private float bias;       // Bias term for the hyperplane

    private void Start()
    {
        InitializeModel(numberOfFeatures);
    }

    /// <summary>
    /// Initializes the SVM model with the specified number of features.
    /// </summary>
    public void InitializeModel(int numberOfFeatures)
    {
        weights = new float[numberOfFeatures];
        bias = 0f;
        Debug.Log($"SVM initialized with {numberOfFeatures} features.");
    }

    /// <summary>
    /// Trains the SVM model using the provided data.
    /// </summary>
    /// <param name="trainingData">List of training samples.</param>
    public void Train(List<TrainingSample> trainingData)
    {
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            foreach (var sample in trainingData)
            {
                // Compute the dot product of weights and sample features
                float dotProduct = 0;
                for (int i = 0; i < weights.Length; i++)
                {
                    dotProduct += weights[i] * sample.Features[i];
                }

                // Check if the sample is on the correct side of the margin
                if (sample.Label * (dotProduct + bias) < 1)
                {
                    // Update weights and bias based on the margin condition
                    for (int i = 0; i < weights.Length; i++)
                    {
                        weights[i] -= learningRate * (2 * weights[i] - sample.Label * sample.Features[i]);
                    }
                    bias -= learningRate * -sample.Label;
                }
                else
                {
                    // Regularize weights if the sample is on the correct side of the margin
                    for (int i = 0; i < weights.Length; i++)
                    {
                        weights[i] -= learningRate * 2 * weights[i];
                    }
                }
            }

            if (epoch % 100 == 0)  // Log progress every 100 epochs
            {
                Debug.Log($"Epoch {epoch}: Weights = {string.Join(", ", weights)}, Bias = {bias:F4}");
            }
        }

        Debug.Log("Training completed.");
    }

    /// <summary>
    /// Predicts the class of a new data point.
    /// </summary>
    /// <param name="features">Feature vector of the data point.</param>
    /// <returns>Predicted class label (1 or -1).</returns>
    public int Predict(float[] features)
    {
        float sum = bias;
        for (int i = 0; i < features.Length; i++)
        {
            sum += weights[i] * features[i];
        }
        return sum >= 0 ? 1 : -1;  // Threshold-based classification
    }

    /// <summary>
    /// Retrieves the learned weights for debugging or analysis.
    /// </summary>
    /// <returns>Array of learned weights.</returns>
    public float[] GetWeights()
    {
        return weights;
    }

    /// <summary>
    /// Retrieves the learned bias for debugging or analysis.
    /// </summary>
    /// <returns>Learned bias value.</returns>
    public float GetBias()
    {
        return bias;
    }
}

[System.Serializable]
public class TrainingSample
{
    public float[] Features;  // Feature vector of the sample
    public int Label;         // Label (1 or -1)

    public TrainingSample(float[] features, int label)
    {
        Features = features;
        Label = label;
    }

    public override string ToString()
    {
        return $"Features: [{string.Join(", ", Features)}], Label: {Label}";
    }
}
