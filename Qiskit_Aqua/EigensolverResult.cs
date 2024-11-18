using System;
using System.Collections.Generic;

public class AquaEigensolverResult
{
    /// <summary>
    /// Eigenvalues computed by the solver.
    /// </summary>
    public double[] Eigenvalues { get; set; }

    /// <summary>
    /// Eigenstates corresponding to the computed eigenvalues.
    /// </summary>
    public double[][] Eigenstates { get; set; }

    /// <summary>
    /// Expectation values of auxiliary operators.
    /// </summary>
    public double[] AuxOperatorEigenvalues { get; set; }

    /// <summary>
    /// Constructs a new AquaEigensolverResult from a dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing result data.</param>
    public static AquaEigensolverResult FromDictionary(Dictionary<string, object> data)
    {
        var result = new AquaEigensolverResult
        {
            Eigenvalues = data.ContainsKey("eigenvalues") ? (double[])data["eigenvalues"] : null,
            Eigenstates = data.ContainsKey("eigenstates") ? (double[][])data["eigenstates"] : null,
            AuxOperatorEigenvalues = data.ContainsKey("aux_operator_eigenvalues")
                ? (double[])data["aux_operator_eigenvalues"]
                : null
        };
        return result;
    }
}
