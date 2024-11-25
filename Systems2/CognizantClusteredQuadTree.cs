using UnityEngine;
using System.Collections.Generic;
public class CognizantClusteredQuadTree : MonoBehaviour
{
    private CognizantClusteredQuadTreeNode quadTree;
    private CognizantClusteredNeuralLayer neuralLayer;

    void Start()
    {
        Rect bounds = new Rect(0, 0, 100, 100);
        quadTree = new CognizantClusteredQuadTreeNode(bounds, 4);
        neuralLayer = new CognizantClusteredNeuralLayer(2, 1, 0.1f);

        // Example: Insert data points
        quadTree.Insert(new Vector2(10, 20));
        quadTree.Insert(new Vector2(15, 25));
        quadTree.Insert(new Vector2(50, 60));
        quadTree.Insert(new Vector2(80, 85));

        // Perform learning
        Train(new Vector2(10, 20), 1f); // Example: train with target value
    }

    void Train(Vector2 point, float target)
    {
        float[] inputs = { point.x / 100f, point.y / 100f }; // Normalize inputs
        float[] outputs = neuralLayer.Forward(inputs);

        // Compute error and backpropagate
        float[] outputErrors = { target - outputs[0] };
        neuralLayer.Backward(inputs, outputErrors);

        Debug.Log($"Trained on point {point}, Output: {outputs[0]}, Error: {outputErrors[0]}");
    }

    void Update()
    {
        // Example: Query and display points in a region
        Rect queryRegion = new Rect(0, 0, 50, 50);
        var foundPoints = quadTree.Query(queryRegion);

        foreach (var point in foundPoints)
        {
            Debug.Log($"Point in query region: {point}");
        }
    }

    void TrainBatch(List<Vector2> points, List<float> targets)
    {
        for (int i = 0; i < points.Count; i++)
        {
            Train(points[i], targets[i]);
        }
    }

}
