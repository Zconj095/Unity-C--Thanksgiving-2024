using System;

public class Attention
{
    public static float[] ComputeAttention(float[] query, float[][] keys, float[][] values)
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

    private static float DotProduct(float[] a, float[] b)
    {
        float result = 0;
        for (int i = 0; i < a.Length; i++)
        {
            result += a[i] * b[i];
        }
        return result;
    }

    private static float[] Softmax(float[] scores)
    {
        double sum = 0;
        float[] expScores = new float[scores.Length];
        for (int i = 0; i < scores.Length; i++)
        {
            expScores[i] = (float)Math.Exp(scores[i]); // Replace Mathf.Exp with Math.Exp
            sum += expScores[i];
        }
        for (int i = 0; i < scores.Length; i++)
        {
            expScores[i] /= (float)sum; // Cast sum back to float
        }
        return expScores;
    }

    public static float[] FeedForward(float[] input, float[,] weights, float[] biases)
    {
        float[] output = new float[weights.GetLength(1)];
        for (int j = 0; j < weights.GetLength(1); j++)
        {
            output[j] = biases[j];
            for (int i = 0; i < input.Length; i++)
            {
                output[j] += input[i] * weights[i, j];
            }
        }
        return output;
    }

}
