using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class CobylaOptimizer
{
    private readonly Dictionary<string, object> _parameters;
    private readonly List<Func<double[], double>> _constraints;

    public CobylaOptimizer(
        double rhobeg = 1.0,
        double rhoend = 1e-4,
        int maxfun = 1000,
        int? disp = null,
        double catol = 2e-4,
        int trials = 1,
        double clip = 100.0)
    {
        _parameters = new Dictionary<string, object>
        {
            { "rhobeg", rhobeg },
            { "rhoend", rhoend },
            { "maxfun", maxfun },
            { "disp", disp },
            { "catol", catol },
            { "trials", trials },
            { "clip", clip }
        };

        _constraints = new List<Func<double[], double>>();
    }

    public object GetParameter(string name)
    {
        if (_parameters.ContainsKey(name))
        {
            return _parameters[name];
        }
        throw new MissingMemberException($"Parameter {name} not found.");
    }

    public void SetParameter(string name, object value)
    {
        if (_parameters.ContainsKey(name))
        {
            _parameters[name] = value;
        }
        else
        {
            throw new MissingMemberException($"Parameter {name} not found.");
        }
    }

    public void AddConstraint(Func<double[], double> constraint)
    {
        _constraints.Add(constraint);
    }

    public object InvokeMethod(string methodName, object[] parameters)
    {
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
        if (method == null)
        {
            throw new MissingMethodException($"Method {methodName} not found.");
        }
        return method.Invoke(this, parameters);
    }

    public string GetCompatibilityMsg(Dictionary<string, object> problem)
    {
        // Check if the problem contains any discrete variables
        if (problem.ContainsKey("HasDiscreteVars") && (bool)problem["HasDiscreteVars"])
        {
            return "The COBYLA optimizer supports only continuous variables.";
        }
        return "";
    }

    public OptimizationResult Solve(Dictionary<string, object> problem)
    {
        string compatibilityMsg = GetCompatibilityMsg(problem);
        if (!string.IsNullOrEmpty(compatibilityMsg))
        {
            throw new InvalidOperationException(compatibilityMsg);
        }

        // Convert problem to minimization format
        Dictionary<string, object> transformedProblem = TransformToMinimization(problem);

        // Add bounds constraints
        AddBoundsConstraints(transformedProblem);

        // Add additional constraints from the problem
        AddProblemConstraints(transformedProblem);

        // Perform optimization
        return PerformOptimization(transformedProblem);
    }

    private Dictionary<string, object> TransformToMinimization(Dictionary<string, object> problem)
    {
        if ((bool)problem["IsMaximization"])
        {
            problem["ObjectiveFunction"] = (Func<double[], double>)((x) => -((Func<double[], double>)problem["ObjectiveFunction"])(x));
            problem["IsMaximization"] = false;
        }
        return problem;
    }

    private void AddBoundsConstraints(Dictionary<string, object> problem)
    {
        List<(double, double)> bounds = (List<(double, double)>)problem["Bounds"];
        for (int i = 0; i < bounds.Count; i++)
        {
            var lowerBound = bounds[i].Item1;
            var upperBound = bounds[i].Item2;

            if (!double.IsNegativeInfinity(lowerBound))
            {
                _constraints.Add(x => x[i] - lowerBound);
            }

            if (!double.IsPositiveInfinity(upperBound))
            {
                _constraints.Add(x => upperBound - x[i]);
            }
        }
    }

    private void AddProblemConstraints(Dictionary<string, object> problem)
    {
        if (problem.ContainsKey("Constraints"))
        {
            var problemConstraints = (List<Func<double[], double>>)problem["Constraints"];
            _constraints.AddRange(problemConstraints);
        }
    }

    private OptimizationResult PerformOptimization(Dictionary<string, object> problem)
    {
        // Extract parameters
        var rhobeg = (double)GetParameter("rhobeg");
        var rhoend = (double)GetParameter("rhoend");
        var maxfun = (int)GetParameter("maxfun");
        var disp = (int?)GetParameter("disp");
        var catol = (double)GetParameter("catol");

        var objectiveFunction = (Func<double[], double>)problem["ObjectiveFunction"];
        int dimension = ((List<(double, double)>)problem["Bounds"]).Count;

        // Initialize starting point
        double[] x0 = Enumerable.Repeat(0.0, dimension).ToArray();

        // COBYLA optimization (simple simulation)
        for (int iteration = 0; iteration < maxfun; iteration++)
        {
            for (int i = 0; i < x0.Length; i++)
            {
                x0[i] -= rhobeg * 0.1; // Adjust values for convergence
            }

            // Check constraints
            foreach (var constraint in _constraints)
            {
                if (constraint(x0) < -catol)
                {
                    throw new InvalidOperationException("Constraint violated.");
                }
            }

            // Terminate if rhoend is reached
            if (rhobeg < rhoend)
            {
                break;
            }
            rhobeg *= 0.95; // Reduce rhobeg for convergence
        }

        // Return the optimized result
        return new OptimizationResult
        {
            X = x0,
            ObjectiveValue = objectiveFunction(x0)
        };
    }

    public override string ToString()
    {
        string paramStr = "Parameters: ";
        foreach (var param in _parameters)
        {
            paramStr += $"{param.Key}={param.Value}, ";
        }

        string constraintsStr = $"Constraints Count: {_constraints.Count}";
        return $"{paramStr.TrimEnd(',', ' ')}\n{constraintsStr}";
    }
}

public class OptimizationResult
{
    public double[] X { get; set; }
    public double ObjectiveValue { get; set; }

    public override string ToString()
    {
        return $"Solution: [{string.Join(", ", X)}], Objective Value: {ObjectiveValue}";
    }
}
