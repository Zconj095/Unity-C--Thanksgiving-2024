using System;
using System.Collections.Generic;
using System.Linq;

public class NumPyEigensolver : Eigensolver
{
    private OperatorBase _operator;
    private List<OperatorBase> _auxOperators;
    private int _requestedEigenvalues;
    private int _actualEigenvalues;
    private Func<double[], double, List<double>, bool> _filterCriterion;
    private Dictionary<string, object> _result;

    public NumPyEigensolver(OperatorBase operatorInstance = null, int k = 1,
        List<OperatorBase> auxOperators = null, Func<double[], double, List<double>, bool> filterCriterion = null)
    {
        if (k < 1)
            throw new ArgumentException("The number of eigenvalues (k) must be at least 1.");

        _requestedEigenvalues = k;
        _filterCriterion = filterCriterion;
        Operator = operatorInstance;
        AuxOperators = auxOperators;
        _result = new Dictionary<string, object>();
    }

    public override OperatorBase Operator
    {
        get => _operator;
        set
        {
            _operator = value ?? throw new ArgumentNullException(nameof(Operator), "Operator cannot be null.");
            AdjustEigenvalueCount();
        }
    }

    public override List<OperatorBase> AuxOperators
    {
        get => _auxOperators;
        set => _auxOperators = value ?? new List<OperatorBase>();
    }

    public int K
    {
        get => _requestedEigenvalues;
        set
        {
            if (value < 1)
                throw new ArgumentException("The number of eigenvalues (k) must be at least 1.");
            _requestedEigenvalues = value;
            AdjustEigenvalueCount();
        }
    }

    public Func<double[], double, List<double>, bool> FilterCriterion
    {
        get => _filterCriterion;
        set => _filterCriterion = value;
    }

    public override EigensolverResult ComputeEigenvalues(OperatorBase operatorOverride = null, List<OperatorBase> auxOperatorsOverride = null)
    {
        if (operatorOverride != null) Operator = operatorOverride;
        if (auxOperatorsOverride != null) AuxOperators = auxOperatorsOverride;

        if (_operator == null)
            throw new InvalidOperationException("Operator must be set before computing eigenvalues.");

        Solve();
        EvaluateAuxiliaryOperators();
        FilterResults();

        return BuildResult();
    }

    private void AdjustEigenvalueCount()
    {
        if (_operator == null) return;

        int maxEigenvalues = (int)Math.Pow(2, _operator.NumQubits);
        _actualEigenvalues = Math.Min(_requestedEigenvalues, maxEigenvalues);
    }

    private void Solve()
    {
        var matrix = _operator.ToMatrix();
        var eigen = new EigenDecomposition(matrix);

        var eigenvalues = eigen.RealEigenvalues.Take(_actualEigenvalues).ToArray();
        var eigenvectors = eigen.Vectors.Take(_actualEigenvalues).ToArray();

        _result["eigenvalues"] = eigenvalues;
        _result["eigenvectors"] = eigenvectors;
    }

    private void EvaluateAuxiliaryOperators()
    {
        if (_auxOperators == null || _auxOperators.Count == 0) return;

        var eigenvectors = (double[][])_result["eigenvectors"];
        var auxValues = new List<List<double>>();

        foreach (var eigenvector in eigenvectors)
        {
            var values = _auxOperators.Select(auxOp => auxOp.ExpectationValue(eigenvector)).ToList();
            auxValues.Add(values);
        }

        _result["aux_operator_eigenvalues"] = auxValues;
    }

    private void FilterResults()
    {
        if (_filterCriterion == null) return;

        var eigenvalues = (double[])_result["eigenvalues"];
        var eigenvectors = (double[][])_result["eigenvectors"];
        var auxValues = (List<List<double>>)_result["aux_operator_eigenvalues"];

        var filteredEigenvalues = new List<double>();
        var filteredEigenvectors = new List<double[]>();
        var filteredAuxValues = new List<List<double>>();

        for (int i = 0; i < eigenvalues.Length; i++)
        {
            if (_filterCriterion(eigenvectors[i], eigenvalues[i], auxValues?[i]))
            {
                filteredEigenvalues.Add(eigenvalues[i]);
                filteredEigenvectors.Add(eigenvectors[i]);
                if (auxValues != null) filteredAuxValues.Add(auxValues[i]);
            }
        }

        _result["eigenvalues"] = filteredEigenvalues.ToArray();
        _result["eigenvectors"] = filteredEigenvectors.ToArray();
        _result["aux_operator_eigenvalues"] = filteredAuxValues;
    }

    private EigensolverResult BuildResult()
    {
        return new EigensolverResult
        {
            Eigenvalues = (double[])_result["eigenvalues"],
            Eigenstates = (double[][])_result["eigenvectors"],
            AuxOperatorEigenvalues = _result.ContainsKey("aux_operator_eigenvalues")
                ? (List<List<double>>)_result["aux_operator_eigenvalues"]
                : null
        };
    }
}

public class ExactEigensolver : NumPyEigensolver
{
    public ExactEigensolver(OperatorBase operatorInstance, int k = 1, List<OperatorBase> auxOperators = null)
        : base(operatorInstance, k, auxOperators)
    {
        Console.WriteLine("Warning: ExactEigensolver is deprecated. Use NumPyEigensolver instead.");
    }
}
