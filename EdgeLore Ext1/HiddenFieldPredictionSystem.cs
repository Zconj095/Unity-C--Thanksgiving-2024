using System.Collections.Generic;
using UnityEngine;

public class HiddenFieldPredictionSystem : MonoBehaviour
{
    private TimeSeriesManager timeSeriesManager;
    private MovingAveragePredictor predictor;

    void Start()
    {
        timeSeriesManager = gameObject.AddComponent<TimeSeriesManager>();
        predictor = new MovingAveragePredictor();
    }

    void Update()
    {
        // Simulate hidden variables (replace with real data in production)
        Dictionary<string, float> simulatedVariables = new Dictionary<string, float>
        {
            { "VariableA", Mathf.Sin(Time.time) },
            { "VariableB", Mathf.Cos(Time.time) }
        };

        // Add time field to the manager
        timeSeriesManager.AddTimeField(Time.time, simulatedVariables);

        // Perform prediction for "VariableA"
        float predictedValue = predictor.Predict(timeSeriesManager.GetTimeFields(), "VariableA");
        Debug.Log($"Predicted Value for VariableA: {predictedValue}");
    }
}
