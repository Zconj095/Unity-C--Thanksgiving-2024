public class UnsupervisedUpdater
{
    public static void UpdateHopfield(HopfieldNetworkIntegration network, float[] input)
    {
        float[] stabilizedState = network.Recall(input);
        network.Train(stabilizedState);
    }

    public static void UpdateBayesian(LLMBayesianNetwork network, string nodeName, float evidence)
    {
        network.UpdateBelief(nodeName, "true", evidence);
    }
}
