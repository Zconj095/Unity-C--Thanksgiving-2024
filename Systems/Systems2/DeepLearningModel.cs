using UnityEngine;

public class DeepLearningModel
{
    public float[] ProcessOutput(float[] hopfieldOutputs)
    {
        // Placeholder: Pass the Hopfield outputs through a simple softmax for demo purposes
        float sumExp = 0f;
        foreach (var value in hopfieldOutputs)
        {
            sumExp += Mathf.Exp(value);
        }

        float[] softmax = new float[hopfieldOutputs.Length];
        for (int i = 0; i < hopfieldOutputs.Length; i++)
        {
            softmax[i] = Mathf.Exp(hopfieldOutputs[i]) / sumExp;
        }
        return softmax;
    }
}
