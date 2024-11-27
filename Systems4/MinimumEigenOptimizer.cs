using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class MinimumEigenOptimizer
{
    private object _minEigenSolver;
    private double? _penalty;
    private List<object> _converters;

    public MinimumEigenOptimizer(object minEigenSolver, double? penalty = null, List<object> converters = null)
    {
        if (!(bool)InvokeMethod(minEigenSolver, "supports_aux_operators", null))
        {
            throw new InvalidOperationException(
                "Given MinimumEigensolver does not return the eigenstate and is not supported by the MinimumEigenOptimizer."
            );
        }

        _minEigenSolver = minEigenSolver;
        _penalty = penalty;
        _converters = converters ?? new List<object>();
    }

    public object MinEigenSolver
    {
        get => _minEigenSolver;
        set
        {
            if (!(bool)InvokeMethod(value, "supports_aux_operators", null))
            {
                throw new InvalidOperationException("The provided MinimumEigensolver does not support auxiliary operators.");
            }
            _minEigenSolver = value;
        }
    }

    public string GetCompatibilityMsg(object problem)
    {
        // Ensure compatibility of the problem with the optimizer
        return (string)InvokeStaticMethod("QuadraticProgramToQubo", "get_compatibility_msg", new[] { problem });
    }

    public object Solve(object problem)
    {
        string compatibilityMsg = GetCompatibilityMsg(problem);
        if (!string.IsNullOrEmpty(compatibilityMsg))
        {
            throw new InvalidOperationException($"Problem is not compatible: {compatibilityMsg}");
        }

        // Convert problem to QUBO
        object convertedProblem = ConvertProblem(problem);

        // Generate Ising Hamiltonian
        var (operatorObj, offset) = ToIsing(convertedProblem);

        return SolveInternal(operatorObj, offset, convertedProblem, problem);
    }

    private object ConvertProblem(object problem)
    {
        foreach (var converter in _converters)
        {
            problem = InvokeMethod(converter, "convert", new[] { problem });
        }
        return problem;
    }

    private (object, double) ToIsing(object problem)
    {
        object operatorObj = InvokeMethod(problem, "to_ising", null);
        double offset = (double)InvokeMethod(problem, "get_offset", null);
        return (operatorObj, offset);
    }

    private object SolveInternal(object operatorObj, double offset, object convertedProblem, object originalProblem)
    {
        object eigenResult = null;

        if ((int)InvokeMethod(operatorObj, "num_qubits", null) > 0)
        {
            // Compute the minimum eigenvalue
            eigenResult = InvokeMethod(_minEigenSolver, "compute_minimum_eigenvalue", new[] { operatorObj });

            // Extract solutions if eigenstate is available
            if (!HasAttribute(eigenResult, "eigenstate"))
            {
                throw new InvalidOperationException(
                    "MinimumEigenOptimizer does not support this MinimumEigensolver. Use a supported solver instead."
                );
            }

            var eigenstate = GetAttribute(eigenResult, "eigenstate");
            if (eigenstate != null)
            {
                List<SolutionSample> rawSamples = EigenvectorToSolutions(eigenstate, convertedProblem);
                rawSamples = rawSamples.OrderBy(sample => sample.Fval).ToList();

                return CreateResult(rawSamples, eigenResult, offset, originalProblem);
            }
        }

        // Handle cases with no valid operator
        return CreateEmptyResult(originalProblem, offset, eigenResult);
    }

    private List<SolutionSample> EigenvectorToSolutions(object eigenstate, object problem)
    {
        // Convert eigenstate into solutions
        return (List<SolutionSample>)InvokeMethod(problem, "eigenvector_to_solutions", new[] { eigenstate });
    }

    private object CreateResult(List<SolutionSample> rawSamples, object eigenResult, double offset, object originalProblem)
    {
        var bestRaw = rawSamples.First();

        // Adjust solutions for the original problem
        List<SolutionSample> samples = AdjustSamples(rawSamples, originalProblem);

        return new OptimizationResult(
            x: bestRaw.X,
            fval: bestRaw.Fval + offset,
            variables: GetAttribute(originalProblem, "variables"),
            status: "SUCCESS",
            rawSamples: rawSamples,
            eigenSolverResult: eigenResult
        );
    }

    private object CreateEmptyResult(object originalProblem, double offset, object eigenResult)
    {
        double[] emptySolution = new double[GetNumVariables(originalProblem)];
        return new OptimizationResult(
            x: emptySolution,
            fval: offset,
            variables: GetAttribute(originalProblem, "variables"),
            status: "FAILURE",
            rawSamples: null,
            eigenSolverResult: eigenResult
        );
    }

    private List<SolutionSample> AdjustSamples(List<SolutionSample> rawSamples, object originalProblem)
    {
        // Adjust samples for the original problem
        return rawSamples.Select(sample => TransformSample(sample, originalProblem)).ToList();
    }

    private SolutionSample TransformSample(SolutionSample sample, object originalProblem)
    {
        // Transform sample values to match the original problem space
        return new SolutionSample(sample.X, sample.Fval, sample.Probability, "SUCCESS");
    }

    private int GetNumVariables(object problem)
    {
        return (int)InvokeMethod(problem, "get_num_vars", null);
    }

    private object InvokeMethod(object instance, string methodName, object[] parameters)
    {
        MethodInfo method = instance.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (method == null)
        {
            throw new MissingMethodException($"Method {methodName} not found.");
        }
        return method.Invoke(instance, parameters);
    }

    private object InvokeStaticMethod(string typeName, string methodName, object[] parameters)
    {
        Type type = Type.GetType(typeName);
        MethodInfo method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (method == null)
        {
            throw new MissingMethodException($"Static method {methodName} not found in {typeName}.");
        }
        return method.Invoke(null, parameters);
    }

    private bool HasAttribute(object obj, string attributeName)
    {
        return obj.GetType().GetProperty(attributeName) != null;
    }

    private object GetAttribute(object obj, string attributeName)
    {
        PropertyInfo property = obj.GetType().GetProperty(attributeName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        return property?.GetValue(obj);
    }

    public class OptimizationResult
    {
        public double[] X { get; }
        public double Fval { get; }
        public object Variables { get; }
        public string Status { get; }
        public object RawSamples { get; }
        public object EigenSolverResult { get; }

        public OptimizationResult(double[] x, double fval, object variables, string status, object rawSamples, object eigenSolverResult)
        {
            X = x;
            Fval = fval;
            Variables = variables;
            Status = status;
            RawSamples = rawSamples;
            EigenSolverResult = eigenSolverResult;
        }
    }

    public class SolutionSample
    {
        public double[] X { get; }
        public double Fval { get; }
        public double Probability { get; }
        public string Status { get; }

        public SolutionSample(double[] x, double fval, double probability, string status)
        {
            X = x;
            Fval = fval;
            Probability = probability;
            Status = status;
        }
    }
}
