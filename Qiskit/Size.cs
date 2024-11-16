using System.Collections.Generic;

public class Size : AnalysisPass
{
    /// <summary>
    /// Calculates the size of a DAG circuit.
    /// </summary>
    public bool Recurse { get; private set; }

    /// <summary>
    /// Constructor for the Size analysis pass.
    /// </summary>
    /// <param name="recurse">Whether to allow recursion into control flow. If false, an exception will be thrown when control flow is present.</param>
    public Size(bool recurse = false)
    {
        Recurse = recurse;
    }

    /// <summary>
    /// Run the Size pass on the given DAG.
    /// </summary>
    /// <param name="dag">The DAG to analyze.</param>
    public override void Run(DAG dag)
    {
        // Assuming propertySet is a dictionary in the base class AnalysisPass
        propertySet["size"] = dag.Size(Recurse);
    }
}
