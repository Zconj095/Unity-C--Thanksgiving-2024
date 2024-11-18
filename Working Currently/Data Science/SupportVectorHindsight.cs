using UnityEngine;
using System.Collections.Generic;

public class SupportVectorHindsight : MonoBehaviour
{
    /// <summary>
    /// Represents a data point with position and a binary class label.
    /// </summary>
    public struct DataPoint
    {
        public Vector3 Position;
        public int Label; // 0 or 1 (binary classification)

        public DataPoint(Vector3 position, int label)
        {
            Position = position;
            Label = label;
        }
    }

    private List<DataPoint> trainingData = new List<DataPoint>(); // Training data points
    private List<DataPoint> improbableData = new List<DataPoint>(); // Rare or improbable data points

    private Vector3 hyperplaneNormal; // Direction of the separating hyperplane
    private float hyperplaneBias; // Bias term for the hyperplane
    private float margin = 5f; // Margin for decision refinement

    private void Start()
    {
        // Initialize training data
        trainingData.Add(new DataPoint(new Vector3(1, 1, 0), 0)); // Class 0
        trainingData.Add(new DataPoint(new Vector3(2, 2, 0), 0)); // Class 0
        trainingData.Add(new DataPoint(new Vector3(5, 5, 0), 1)); // Class 1
        trainingData.Add(new DataPoint(new Vector3(6, 6, 0), 1)); // Class 1

        // Add improbable or rare data points (outliers)
        improbableData.Add(new DataPoint(new Vector3(8, 0, 0), 1)); // Class 1 outlier
        improbableData.Add(new DataPoint(new Vector3(0, 8, 0), 0)); // Class 0 outlier

        // Train and refine the model
        TrainSVM();
        RefineModelWithImprobability();
    }

    /// <summary>
    /// Trains the SVM model by calculating an approximate decision boundary.
    /// </summary>
    private void TrainSVM()
    {
        Vector3 class0Avg = Vector3.zero;
        Vector3 class1Avg = Vector3.zero;
        int class0Count = 0;
        int class1Count = 0;

        // Calculate the average position for each class
        foreach (var data in trainingData)
        {
            if (data.Label == 0)
            {
                class0Avg += data.Position;
                class0Count++;
            }
            else
            {
                class1Avg += data.Position;
                class1Count++;
            }
        }

        if (class0Count > 0) class0Avg /= class0Count;
        if (class1Count > 0) class1Avg /= class1Count;

        // Calculate hyperplane direction and bias
        hyperplaneNormal = (class1Avg - class0Avg).normalized;
        hyperplaneBias = Vector3.Dot(hyperplaneNormal, class0Avg);

        Debug.Log("SVM Model Trained.");
    }

    /// <summary>
    /// Refines the SVM model using improbable data points.
    /// </summary>
    private void RefineModelWithImprobability()
    {
        foreach (var improbablePoint in improbableData)
        {
            float distanceToHyperplane = Mathf.Abs(Vector3.Dot(improbablePoint.Position, hyperplaneNormal) - hyperplaneBias);

            // Adjust the hyperplane if the improbable point lies far from the margin
            if (distanceToHyperplane > margin)
            {
                Vector3 adjustment = improbablePoint.Position - (hyperplaneNormal * Vector3.Dot(improbablePoint.Position, hyperplaneNormal));
                hyperplaneNormal += adjustment.normalized;
                hyperplaneNormal.Normalize();

                // Update the bias term after adjustment
                hyperplaneBias = Vector3.Dot(hyperplaneNormal, improbablePoint.Position);
            }
        }

        Debug.Log("Model Refined with Improbable Points.");
    }

    private void Update()
    {
        // Example usage: Test classification of a new point
        Vector3 testPoint = new Vector3(4, 4, 0);
        int predictedLabel = ClassifyPoint(testPoint);
        Debug.Log($"Predicted Label for Test Point {testPoint}: {predictedLabel}");
    }

    /// <summary>
    /// Classifies a point using the current hyperplane.
    /// </summary>
    /// <param name="point">The point to classify.</param>
    /// <returns>The predicted label (0 or 1).</returns>
    private int ClassifyPoint(Vector3 point)
    {
        float decisionValue = Vector3.Dot(point, hyperplaneNormal) - hyperplaneBias;
        return decisionValue >= 0 ? 1 : 0;
    }

    /// <summary>
    /// Visualizes training data, improbable points, and the decision boundary in the Unity Scene view.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (trainingData.Count > 0)
        {
            // Draw training data points
            foreach (var data in trainingData)
            {
                Gizmos.color = data.Label == 0 ? Color.blue : Color.green;
                Gizmos.DrawSphere(data.Position, 0.1f);
            }
        }

        if (improbableData.Count > 0)
        {
            // Draw improbable data points
            foreach (var data in improbableData)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(data.Position, 0.15f);
            }
        }

        // Visualize the hyperplane
        if (hyperplaneNormal != Vector3.zero)
        {
            Gizmos.color = Color.yellow;

            Vector3 origin = Vector3.zero; // Adjust origin if necessary
            Vector3 direction = hyperplaneNormal * 10f;
            Gizmos.DrawLine(origin - direction, origin + direction);
        }
    }
}
