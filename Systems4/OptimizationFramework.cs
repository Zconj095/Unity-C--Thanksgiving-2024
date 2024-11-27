using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class OptimizationFramework
{
    public enum OptimizationResultStatus
    {
        SUCCESS,
        FAILURE,
        INFEASIBLE
    }

    public class SolutionSample
    {
        public float[] Variables { get; private set; }
        public float ObjectiveValue { get; private set; }
        public float Probability { get; private set; }
        public OptimizationResultStatus Status { get; private set; }

        public SolutionSample(float[] variables, float objectiveValue, float probability, OptimizationResultStatus status)
        {
            Variables = variables;
            ObjectiveValue = objectiveValue;
            Probability = probability;
            Status = status;
        }

        public override string ToString()
        {
            return $"Variables: [{string.Join(", ", Variables)}], Objective: {ObjectiveValue}, Probability: {Probability}, Status: {Status}";
        }
    }

    public class OptimizationResult
    {
        public float[] Variables { get; private set; }
        public float ObjectiveValue { get; private set; }
        public OptimizationResultStatus Status { get; private set; }
        public List<SolutionSample> Samples { get; private set; }

        public OptimizationResult(float[] variables, float objectiveValue, OptimizationResultStatus status, List<SolutionSample> samples)
        {
            Variables = variables;
            ObjectiveValue = objectiveValue;
            Status = status;
            Samples = samples;
        }

        public override string ToString()
        {
            return $"Objective Value: {ObjectiveValue}, Status: {Status}, Variables: [{string.Join(", ", Variables)}]";
        }
    }

    public class OptimizationProblem
    {
        public Func<float[], float> ObjectiveFunction { get; private set; }
        public int VariableCount { get; private set; }
        public float[] LowerBounds { get; private set; }
        public float[] UpperBounds { get; private set; }

        public OptimizationProblem(Func<float[], float> objectiveFunction, int variableCount, float[] lowerBounds, float[] upperBounds)
        {
            ObjectiveFunction = objectiveFunction;
            VariableCount = variableCount;
            LowerBounds = lowerBounds;
            UpperBounds = upperBounds;
        }
    }

    public class Optimizer
    {
        private readonly int _maxTrials;
        private readonly float _clip;

        public Optimizer(int maxTrials = 1, float clip = 100.0f)
        {
            _maxTrials = maxTrials;
            _clip = clip;
        }

        public OptimizationResult Optimize(OptimizationProblem problem, Func<float[], float[], (float[], float)> minimizer)
        {
            float bestObjective = float.MaxValue;
            float[] bestSolution = null;
            var allSamples = new List<SolutionSample>();

            for (int trial = 0; trial < _maxTrials; trial++)
            {
                float[] startPoint = InitializeStartPoint(problem, trial);

                Debug.Log($"Trial {trial + 1}: Starting optimization...");
                var (solution, objectiveValue) = minimizer(startPoint, problem.LowerBounds);

                if (objectiveValue < bestObjective)
                {
                    bestObjective = objectiveValue;
                    bestSolution = solution;
                }

                allSamples.Add(new SolutionSample(solution, objectiveValue, 1.0f / _maxTrials, OptimizationResultStatus.SUCCESS));
            }

            return new OptimizationResult(bestSolution, bestObjective, OptimizationResultStatus.SUCCESS, allSamples);
        }

        private float[] InitializeStartPoint(OptimizationProblem problem, int trial)
        {
            float[] startPoint = new float[problem.VariableCount];

            for (int i = 0; i < problem.VariableCount; i++)
            {
                if (trial == 0)
                {
                    startPoint[i] = 0f; // First trial starts at zero
                }
                else
                {
                    float lower = Mathf.Max(problem.LowerBounds[i], -_clip);
                    float upper = Mathf.Min(problem.UpperBounds[i], _clip);
                    startPoint[i] = UnityEngine.Random.Range(lower, upper);
                }
            }

            return startPoint;
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
    }
}
