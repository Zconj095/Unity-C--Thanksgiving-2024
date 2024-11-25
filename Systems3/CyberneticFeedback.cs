public class CyberneticFeedback
{
    public float[] AdjustWeights(float[] weights, float[] gradients, float learningRate)
    {
        float[] adjustedWeights = new float[weights.Length];
        for (int i = 0; i < weights.Length; i++)
        {
            adjustedWeights[i] = weights[i] - learningRate * gradients[i];
        }
        return adjustedWeights;
    }
}
