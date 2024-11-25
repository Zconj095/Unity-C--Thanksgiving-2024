public class RecursiveLearning
{
    public void Train(float[][] data, RecursiveLayer layer, CyberneticFeedback feedback, float learningRate)
    {
        foreach (var sample in data)
        {
            float[] prediction = layer.Forward(sample, sample, (input, fb) => input + fb * learningRate);
            float[] gradients = ComputeGradients(sample, prediction);
            feedback.AdjustWeights(sample, gradients, learningRate);
        }
    }

    private float[] ComputeGradients(float[] target, float[] prediction)
    {
        float[] gradients = new float[target.Length];
        for (int i = 0; i < target.Length; i++)
        {
            gradients[i] = 2 * (prediction[i] - target[i]);
        }
        return gradients;
    }
}
