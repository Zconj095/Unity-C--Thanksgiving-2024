using UnityEngine;

public class DynamicCognitionController : MonoBehaviour
{
    public SpatialCognition spatialCognition;
    public TemporalFeedback temporalFeedback;
    public CognitiveState cognitiveState;

    public float spatialWeight = 0.6f;
    public float temporalWeight = 0.4f;

    void Update()
    {
        if (spatialCognition == null || temporalFeedback == null || cognitiveState == null) return;

        // Get current state and spatial influence
        float[] currentState = cognitiveState.GetState();
        float[] spatialInfluence = ComputeSpatialInfluence();

        // Get temporal feedback
        float[] temporalFeedbackState = temporalFeedback.ComputeTemporalFeedback();

        // Compute new state
        float[] newState = new float[currentState.Length];
        for (int i = 0; i < currentState.Length; i++)
        {
            newState[i] = spatialWeight * spatialInfluence[i] + temporalWeight * temporalFeedbackState[i];
        }

        // Update cognitive state
        cognitiveState.UpdateState(newState);

        // Store current state in temporal feedback
        temporalFeedback.StoreState(currentState);
    }

    float[] ComputeSpatialInfluence()
    {
        float[] influence = new float[cognitiveState.vectorSize];
        for (int i = 0; i < cognitiveState.vectorSize; i++)
        {
            int x = i % spatialCognition.gridSize;
            int y = i / spatialCognition.gridSize;
            influence[i] = spatialCognition.GetSpatialInfluence(x, y);
        }
        return influence;
    }
}
