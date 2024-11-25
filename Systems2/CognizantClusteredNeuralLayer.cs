using UnityEngine;

public class CognizantClusteredNeuralLayer
{
    private float[,] weights; // Weights between inputs and neurons
    private float[] biases; // Bias terms
    private float[] outputs; // Outputs of the neurons
    private float learningRate;

    public CognizantClusteredNeuralLayer(int inputSize, int outputSize, float learningRate)
    {
        this.learningRate = learningRate;
        weights = new float[inputSize, outputSize];
        biases = new float[outputSize];
        outputs = new float[outputSize];

        InitializeWeights();
    }

    private void InitializeWeights()
    {
        for (int i = 0; i < weights.GetLength(0); i++)
        {
            for (int j = 0; j < weights.GetLength(1); j++)
            {
                weights[i, j] = Random.Range(-0.5f, 0.5f);
            }
        }

        for (int i = 0; i < biases.Length; i++)
        {
            biases[i] = Random.Range(-0.5f, 0.5f);
        }
    }

    public float[] Forward(float[] inputs)
    {
        for (int j = 0; j < outputs.Length; j++)
        {
            float sum = biases[j];
            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * weights[i, j];
            }
            outputs[j] = Sigmoid(sum);
        }

        return outputs;
    }

    public float[] Backward(float[] inputs, float[] outputErrors)
    {
        float[] inputErrors = new float[inputs.Length];

        for (int i = 0; i < inputs.Length; i++)
        {
            for (int j = 0; j < outputs.Length; j++)
            {
                float errorGradient = outputErrors[j] * SigmoidDerivative(outputs[j]);
                inputErrors[i] += errorGradient * weights[i, j];
                weights[i, j] -= learningRate * errorGradient * inputs[i];
                biases[j] -= learningRate * errorGradient;
            }
        }

        return inputErrors;
    }

    private float Sigmoid(float x)
    {
        return 1f / (1f + Mathf.Exp(-x));
    }

    private float SigmoidDerivative(float x)
    {
        return x * (1f - x);
    }
}
