using UnityEngine;

public class CognitiveSystemManager : MonoBehaviour
{
    // Fields for cognitive field data and domain system data
    private float[] cognitiveFieldData;
    private float[] fieldDomainData;
    
    // Threshold for cognitive state evaluation
    private float cognitionThreshold = 0.5f;

    // Emotional throughput metrics
    private float emotionalMagnitude = 0.0f;
    private float cognitiveShift = 0.0f;

    void Start()
    {
        // Initialize cognitive field and field domain data
        cognitiveFieldData = new float[] { 0.2f, 0.4f, 0.6f };
        fieldDomainData = new float[] { 0.0f, 0.0f, 0.0f };

        Debug.Log("Cognitive System Initialized.");
    }

    // Shift data from the cognitive field to the field domain system
    public void ShiftToFieldDomain()
    {
        fieldDomainData = (float[])cognitiveFieldData.Clone();
        Debug.Log("Cognitive Field Data has been shifted to Field Domain System.");
    }

    // Update field domain system via an administration console
    public void UpdateFieldDomain(float[] newData)
    {
        if (newData.Length == fieldDomainData.Length)
        {
            fieldDomainData = newData;
            Debug.Log("Field Domain System has been updated via Admin Console.");
        }
        else
        {
            Debug.LogWarning("Data size mismatch! Update failed.");
        }
    }

    // Evaluate cognition threshold based on the field domain data
    public bool EvaluateCognitionThreshold()
    {
        float averageValue = 0.0f;

        foreach (float value in fieldDomainData)
        {
            averageValue += value;
        }
        averageValue /= fieldDomainData.Length;

        Debug.Log($"Cognition Threshold Evaluation: Average Value = {averageValue}, Threshold = {cognitionThreshold}");
        return averageValue > cognitionThreshold;
    }

    // Measure emotional throughput and calculate cognitive shift
    public void ProcessEmotionalThroughput(float emotionalInput)
    {
        emotionalMagnitude = emotionalInput;
        cognitiveShift = CalculateCognitiveShift(emotionalMagnitude);

        Debug.Log($"Emotional Input: {emotionalMagnitude}, Cognitive Shift: {cognitiveShift}");
    }

    // Calculate the cognitive shift based on emotional magnitude
    private float CalculateCognitiveShift(float magnitude)
    {
        float shift = 0.0f;

        for (int i = 0; i < fieldDomainData.Length; i++)
        {
            shift += Mathf.Abs(fieldDomainData[i] - magnitude);
        }

        shift /= fieldDomainData.Length;
        return shift;
    }

    // Display the current state of the system
    public void DisplaySystemState()
    {
        Debug.Log("=== System State ===");
        Debug.Log("Cognitive Field Data: " + string.Join(", ", cognitiveFieldData));
        Debug.Log("Field Domain Data: " + string.Join(", ", fieldDomainData));
        Debug.Log($"Cognition Threshold: {cognitionThreshold}");
        Debug.Log($"Emotional Magnitude: {emotionalMagnitude}");
        Debug.Log($"Cognitive Shift: {cognitiveShift}");
    }
}
