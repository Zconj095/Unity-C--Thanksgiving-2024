using UnityEngine;
using System.Collections.Generic;

public class SupportVectorCorrelation : MonoBehaviour
{
    /// <summary>
    /// Represents a data point with a position and value.
    /// </summary>
    public struct DataPoint
    {
        public Vector3 Position;
        public float Value;

        public DataPoint(Vector3 position, float value)
        {
            Position = position;
            Value = value;
        }
    }

    [Header("SVR Parameters")]
    [SerializeField] private float epsilon = 0.1f; // Margin of tolerance
    [SerializeField] private float C = 1.0f; // Regularization parameter
    [SerializeField] private float gamma = 0.5f; // RBF kernel parameter

    private List<DataPoint> trainingData = new List<DataPoint>();
    private List<DataPoint> supportVectors = new List<DataPoint>();

    private void Start()
    {
        // Initialize some training data
        trainingData.Add(new DataPoint(new Vector3(1, 1, 0), 2.0f));
        trainingData.Add(new DataPoint(new Vector3(2, 2, 0), 3.0f));
        trainingData.Add(new DataPoint(new Vector3(3, 3, 0), 4.0f));
        trainingData.Add(new DataPoint(new Vector3(4, 4, 0), 5.0f));

        // Train the model
        TrainModel();
    }

    /// <summary>
    /// Trains the Support Vector Regression (SVR) model.
    /// </summary>
    private void TrainModel()
    {
        // For simplicity, all training points are used as support vectors in this example.
        supportVectors = trainingData;

        Debug.Log($"Model Trained: Support Vectors Count = {supportVectors.Count}");
    }

    /// <summary>
    /// Predicts the value for a given test point using the support vectors.
    /// </summary>
    /// <param name="point">The test point to predict the value for.</param>
    /// <returns>The predicted value.</returns>
    private float Predict(Vector3 point)
    {
        float prediction = 0.0f;

        foreach (var supportVector in supportVectors)
        {
            float kernelValue = RbfKernel(point, supportVector.Position);
            prediction += kernelValue * supportVector.Value;
        }

        return prediction;
    }

    /// <summary>
    /// Calculates the Radial Basis Function (RBF) kernel value.
    /// </summary>
    /// <param name="x1">First data point.</param>
    /// <param name="x2">Second data point.</param>
    /// <returns>The kernel value.</returns>
    private float RbfKernel(Vector3 x1, Vector3 x2)
    {
        float distanceSquared = (x1 - x2).sqrMagnitude;
        return Mathf.Exp(-gamma * distanceSquared);
    }

    private void Update()
    {
        // Example usage: Predict the value for a new test point
        Vector3 testPoint = new Vector3(2.5f, 2.5f, 0);
        float predictedValue = Predict(testPoint);
        Debug.Log($"Predicted Value for Test Point {testPoint}: {predictedValue}");
    }

    /// <summary>
    /// Visualizes the training data and support vectors in the Unity Scene view.
    /// </summary>
    private void OnDrawGizmos()
    {
        // Visualize the training data points
        foreach (var data in trainingData)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(data.Position, 0.1f);
        }

        // Visualize the support vectors
        foreach (var vector in supportVectors)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(vector.Position, 0.2f);
        }
    }
}
