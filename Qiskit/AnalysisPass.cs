using System.Collections.Generic;

public abstract class AnalysisPass
{
    // A dictionary to hold results of various analysis passes
    protected Dictionary<string, object> propertySet = new Dictionary<string, object>();

    /// <summary>
    /// Abstract method that must be implemented by derived classes to run the analysis pass.
    /// </summary>
    /// <param name="dag">The DAG object on which the analysis is performed.</param>
    public abstract void Run(DAG dag);

    /// <summary>
    /// Gets the result of the analysis stored in the propertySet dictionary.
    /// </summary>
    /// <param name="key">The key of the result to retrieve from the propertySet.</param>
    /// <returns>The result associated with the given key, or null if the key does not exist.</returns>
    public object GetResult(string key)
    {
        if (propertySet.ContainsKey(key))
        {
            return propertySet[key];
        }
        return null;
    }
}
