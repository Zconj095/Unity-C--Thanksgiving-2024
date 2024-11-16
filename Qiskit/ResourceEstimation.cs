using System.Collections.Generic;

public class ResourceEstimation : AnalysisPass
{
    /// <summary>
    /// Automatically requires analysis passes for resource estimation.
    /// Includes:
    /// - Depth
    /// - Width
    /// - Size
    /// - CountOps
    /// - NumTensorFactors
    /// - NumQubits
    /// </summary>
    private List<AnalysisPass> requiredPasses;

    /// <summary>
    /// Constructor for ResourceEstimation.
    /// </summary>
    public ResourceEstimation()
    {
        requiredPasses = new List<AnalysisPass>
        {
            new Depth(),
            new Width(),
            new Size(),
            new CountOps(),
            new NumTensorFactors(),
            new NumQubits()
        };
    }

    /// <summary>
    /// Run the ResourceEstimation pass. This is a placeholder since the required passes are executed individually.
    /// </summary>
    /// <param name="_">The DAG (not used here).</param>
    public override void Run(DAG _)
    {
        // This method is intentionally left empty as it orchestrates other passes.
    }

    /// <summary>
    /// Gets the required analysis passes.
    /// </summary>
    /// <returns>List of required analysis passes.</returns>
    public List<AnalysisPass> GetRequiredPasses()
    {
        return requiredPasses;
    }
}