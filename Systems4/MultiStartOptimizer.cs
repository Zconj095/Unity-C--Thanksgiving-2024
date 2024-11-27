using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MultiStartOptimizer
{
    private int _trials;
    private float _clip;
    private readonly List<Variable> _variables;

    public MultiStartOptimizer(int trials = 1, float clip = 100.0f)
    {
        if (trials < 1)
        {
            throw new ArgumentException($"Number of trials should be 1 or higher, but was {trials}");
        }

        _trials = trials;
        _clip = clip;
        _variables = new List<Variable>();
    }

    public int Trials
    {
        get => _trials;
        set
        {
            if (value < 1)
            {
                throw new ArgumentException($"Number of trials should be 1 or higher, but was {value}");
            }
            _trials = value;
        }
    }

    public float Clip
    {
        get => _clip;
        set => _clip = value;
    }

    public OptimizationResult MultiStartSolve(Func<float[], Tuple<float[], object>> minimize, OptimizationProblem problem)
    {
        float fvalSolution = float.MaxValue;
        float[] xSolution = null;
        object restSolution = null;

        // Turn problem into a minimization problem
        problem = ConvertToMinimization(problem);

        // Multi-start optimization implementation
        for (int trial = 0; trial < _trials; trial++)
        {
            float[] x0 = InitializeStartPoint(problem, trial);

            // Perform optimization
            Debug.Log($"Trial {trial + 1}: Starting optimization...");
            var (x, rest) = minimize(x0);

            float fval = EvaluateObjective(problem, x);
            Debug.Log($"Trial {trial + 1}: fval = {fval}");

            if (fval < fvalSolution)
            {
                fvalSolution = fval;
                xSolution = x;
                restSolution = rest;
            }
        }

        // Convert the result back to the original problem (if necessary)
        return InterpretSolution(xSolution, fvalSolution, problem, restSolution);
    }

    private float[] InitializeStartPoint(OptimizationProblem problem, int trial)
    {
        float[] x0 = new float[problem.NumVars];

        if (trial > 0)
        {
            // Random initialization for subsequent trials
            for (int i = 0; i < problem.NumVars; i++)
            {
                Variable variable = problem.Variables[i];
                float lowerBound = Mathf.Max(variable.LowerBound, -_clip);
                float upperBound = Mathf.Min(variable.UpperBound, _clip);
                x0[i] = UnityEngine.Random.Range(lowerBound, upperBound);
            }
        }

        return x0;
    }

    private float EvaluateObjective(OptimizationProblem problem, float[] solution)
    {
        return problem.Objective(solution);
    }

    private OptimizationProblem ConvertToMinimization(OptimizationProblem problem)
    {
        // Convert problem to a minimization problem if necessary
        problem.IsMaximization = false;
        return problem;
    }

    private OptimizationResult InterpretSolution(float[] xSolution, float fvalSolution, OptimizationProblem problem, object restSolution)
    {
        return new OptimizationResult(xSolution, fvalSolution, "Success", restSolution);
    }

    public object InvokeMethod(string methodName, params object[] parameters)
    {
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (method == null)
        {
            throw new MissingMethodException($"Method '{methodName}' not found.");
        }
        return method.Invoke(this, parameters);
    }

    public object GetProperty(string propertyName)
    {
        PropertyInfo property = GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (property == null)
        {
            throw new MissingMemberException($"Property '{propertyName}' not found.");
        }
        return property.GetValue(this);
    }

    public void SetProperty(string propertyName, object value)
    {
        PropertyInfo property = GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (property == null)
        {
            throw new MissingMemberException($"Property '{propertyName}' not found.");
        }
        property.SetValue(this, value);
    }

    public class Variable
    {
        public string Name { get; }
        public float LowerBound { get; }
        public float UpperBound { get; }

        public Variable(string name, float lowerBound, float upperBound)
        {
            Name = name;
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }
    }

    public class OptimizationProblem
    {
        public int NumVars => Variables.Count;
        public bool IsMaximization { get; set; }
        public List<Variable> Variables { get; }
        public Func<float[], float> Objective { get; }

        public OptimizationProblem(List<Variable> variables, Func<float[], float> objective, bool isMaximization = false)
        {
            Variables = variables;
            Objective = objective;
            IsMaximization = isMaximization;
        }
    }

    public class OptimizationResult
    {
        public float[] Solution { get; }
        public float Fval { get; }
        public string Status { get; }
        public object RawResults { get; }

        public OptimizationResult(float[] solution, float fval, string status, object rawResults)
        {
            Solution = solution;
            Fval = fval;
            Status = status;
            RawResults = rawResults;
        }

        public override string ToString()
        {
            return $"Solution: [{string.Join(", ", Solution)}], Objective Value: {Fval}, Status: {Status}";
        }
    }
}
