using System;
using System.Collections.Generic;

public abstract class Eigensolver
{
    /// <summary>
    /// The operator for which eigenvalues are computed.
    /// </summary>
    public abstract OperatorBase Operator { get; set; }

    /// <summary>
    /// The auxiliary operators whose expectation values are computed.
    /// </summary>
    public abstract List<OperatorBase> AuxOperators { get; set; }

    /// <summary>
    /// Computes eigenvalues and eigenstates for the given operator.
    /// </summary>
    /// <param name="operator">The main operator (optional, overrides if set).</param>
    /// <param name="auxOperators">Auxiliary operators (optional, overrides if set).</param>
    /// <returns>An EigensolverResult containing eigenvalues and eigenstates.</returns>
    public abstract EigensolverResult ComputeEigenvalues(
        OperatorBase operatorOverride = null,
        List<OperatorBase> auxOperatorsOverride = null);

    /// <summary>
    /// Indicates whether auxiliary operator computations are supported.
    /// </summary>
    public static bool SupportsAuxOperators()
    {
        return false;
    }
}

public class EigensolverResult
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
    /// Constructs a new EigensolverResult from a dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing result data.</param>
    public static EigensolverResult FromDictionary(Dictionary<string, object> data)
    {
        var result = new EigensolverResult
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
