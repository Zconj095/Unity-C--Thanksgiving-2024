public class DenseLayer
{
    private float[,] weights;
    private float[] biases;

    public DenseLayer(int inputSize, int outputSize)
    {
        weights = new float[inputSize, outputSize];
        biases = new float[outputSize];
        InitializeWeights();
    }

    private void InitializeWeights()
    {
        System.Random random = new System.Random();
        for (int i = 0; i < weights.GetLength(0); i++)
        {
            for (int j = 0; j < weights.GetLength(1); j++)
            {
                weights[i, j] = (float)random.NextDouble() * 2 - 1;
            }
        }
        for (int i = 0; i < biases.Length; i++)
        {
            biases[i] = 0f;
        }
    }

    public float[,] GetWeights()
    {
        return weights;
    }

    public float[] GetBiases()
    {
        return biases;
    }

    public float[] Forward(float[] input, float[,] weights, float[] biases)
    {
        float[] output = new float[weights.GetLength(1)];
        for (int j = 0; j < output.Length; j++)
        {
            output[j] = biases[j];
            for (int i = 0; i < input.Length; i++)
            {
                output[j] += input[i] * weights[i, j];
            }
            output[j] = (float)System.Math.Tanh(output[j]); // Activation function
        }
        return output;
    }
}
