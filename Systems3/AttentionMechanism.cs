using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AttentionMechanism
{
    public float[] ComputeAttention(float[] query, float[][] keys, float[][] values)
    {
        float[] scores = new float[keys.Length];
        for (int i = 0; i < keys.Length; i++)
        {
            scores[i] = DotProduct(query, keys[i]);
        }

        float[] attentionWeights = Softmax(scores);

        float[] output = new float[values[0].Length];
        for (int i = 0; i < values.Length; i++)
        {
            for (int j = 0; j < values[i].Length; j++)
            {
                output[j] += attentionWeights[i] * values[i][j];
            }
        }
        return output;
    }

    private float DotProduct(float[] a, float[] b)
    {
        float result = 0;
        for (int i = 0; i < a.Length; i++)
        {
            result += a[i] * b[i];
        }
        return result;
    }

    private float[] Softmax(float[] scores)
    {
        float sum = 0;
        float[] expScores = new float[scores.Length];
        for (int i = 0; i < scores.Length; i++)
        {
            expScores[i] = Mathf.Exp(scores[i]);
            sum += expScores[i];
        }
        for (int i = 0; i < scores.Length; i++)
        {
            expScores[i] /= sum;
        }
        return expScores;
    }
}
