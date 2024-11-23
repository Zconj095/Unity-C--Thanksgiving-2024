public class ARAccessManager
{
    private float[] referenceSignal = { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };
    private ARCrossValidation crossValidation = new ARCrossValidation();

    public bool VerifyAccess(float[] inputSignal)
    {
        // Step 1: Validate pattern similarity using Cross-Validation
        bool isValidPattern = crossValidation.ValidatePattern(inputSignal);

        // Step 2: Verify similarity using Cross-Correlation
        bool isCorrelated = ARCrossCorrelation.ValidateWithCorrelation(referenceSignal, inputSignal, 0.9f);

        // Grant access if both checks pass
        return isValidPattern && isCorrelated;
    }
}
