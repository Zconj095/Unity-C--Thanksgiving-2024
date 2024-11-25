public class NeuralOptimizer
{
    private float[,] weights;
    private float[] biases;

    public NeuralOptimizer(int inputSize, int outputSize)
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
                weights[i, j] = (float)(random.NextDouble() * 2 - 1); // Random between -1 and 1
            }
        }
        for (int i = 0; i < biases.Length; i++)
        {
            biases[i] = 0f;
        }
    }

    public void AdjustWeights(float[] gradients)
    {
        for (int i = 0; i < weights.GetLength(0); i++)
        {
            for (int j = 0; j < weights.GetLength(1); j++)
            {
                weights[i, j] -= gradients[j] * 0.01f; // Learning rate = 0.01
            }
        }
    }
}
