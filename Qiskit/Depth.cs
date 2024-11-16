using System.Collections.Generic;

public class Depth : AnalysisPass
{
    /// <summary>
    /// Calculates the depth of a DAG circuit.
    /// </summary>
    public bool Recurse { get; private set; }

    /// <summary>
    /// Constructor for the Depth analysis pass.
    /// </summary>
    /// <param name="recurse">Whether to allow recursion into control flow. If false, an exception will be thrown when control flow is present.</param>
    public Depth(bool recurse = false)
    {
        Recurse = recurse;
    }

    /// <summary>
    /// Run the Depth pass on the given DAG.
    /// </summary>
    /// <param name="dag">The DAG to analyze.</param>
    public override void Run(DAG dag)
    {
        // Assuming propertySet is a dictionary in the base class AnalysisPass
        propertySet["depth"] = dag.Depth(Recurse);
    }
}