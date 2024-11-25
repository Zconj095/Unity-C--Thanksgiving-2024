public class HyperStateLearner
{
    public HyperState LearnPattern(HyperState[] patterns, float learningRate)
    {
        HyperState aggregated = patterns[0];
        for (int i = 1; i < patterns.Length; i++)
        {
            aggregated = aggregated.Superpose(patterns[i]);
        }

        float[] adjusted = aggregated.Vector;
        for (int i = 0; i < adjusted.Length; i++)
        {
            adjusted[i] *= learningRate; // Scale by learning rate
        }

        return new HyperState(adjusted);
    }
}
