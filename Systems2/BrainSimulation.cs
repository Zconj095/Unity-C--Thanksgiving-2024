using UnityEngine;

public class BrainSimulation : MonoBehaviour
{
    public int numberOfStimuli = 100;
    public float stimulusRange = 10.0f;
    public float sparsenessThreshold = 0.5f;
    public float differenceThreshold = 0.1f;

    private float[] stimuli;
    private float[] encodedStimuli;

    void Start()
    {
        GenerateStimuli();
        EncodeStimuli();
        VisualizeStimuli();
    }

    void GenerateStimuli()
    {
        stimuli = new float[numberOfStimuli];
        for (int i = 0; i < numberOfStimuli; i++)
        {
            stimuli[i] = Random.Range(-stimulusRange, stimulusRange);
        }
    }

    void EncodeStimuli()
    {
        encodedStimuli = new float[numberOfStimuli];
        for (int i = 0; i < numberOfStimuli; i++)
        {
            float difference = stimuli[i] - GetAverageStimulus();
            if (Mathf.Abs(difference) > differenceThreshold)
            {
                encodedStimuli[i] = difference;
            }
            else
            {
                encodedStimuli[i] = 0;
            }
        }
    }

    float GetAverageStimulus()
    {
        float sum = 0;
        for (int i = 0; i < numberOfStimuli; i++)
        {
            sum += stimuli[i];
        }
        return sum / numberOfStimuli;
    }

    void VisualizeStimuli()
    {
        string stimuliString = "Original Stimuli: " + string.Join(", ", stimuli) + "\n";
        string encodedString = "Encoded Stimuli: " + string.Join(", ", encodedStimuli) + "\n";
        Debug.Log(stimuliString + encodedString);
    }
}
