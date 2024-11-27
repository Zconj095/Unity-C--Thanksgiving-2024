using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class UMDAOptimizer
{
    private readonly int _maxIterations;
    private readonly int _generationSize;
    private readonly double _alpha;
    private readonly Action<int, double[], double>? _callback;

    private double[][] _population;
    private double[][] _distributionParameters;
    private double[] _evaluations;
    private double[] _bestIndividual;
    private double _bestCost;
    private int _staleIterations;

    public UMDAOptimizer(int maxIterations, int generationSize, double alpha, Action<int, double[], double>? callback = null)
    {
        if (generationSize <= 0)
            throw new ArgumentException("Generation size must be greater than 0.");
        if (alpha <= 0 || alpha > 1)
            throw new ArgumentException("Alpha must be in the range (0, 1].");

        _maxIterations = maxIterations;
        _generationSize = generationSize;
        _alpha = alpha;
        _callback = callback;

        _bestCost = double.MaxValue;
    }

    public OptimizerResult Minimize(Func<double[], double> objectiveFunction, double[] initialPoint)
    {
        int numVariables = initialPoint.Length;

        InitializeDistributionParameters(numVariables);
        GenerateInitialPopulation(numVariables);

        for (int iteration = 0; iteration < _maxIterations; iteration++)
        {
            EvaluatePopulation(objectiveFunction);
            UpdateBestSolution();
            UpdateDistributionParameters();
            GenerateNewPopulation();

            _callback?.Invoke(iteration * _generationSize, _bestIndividual, _bestCost);

            if (_staleIterations >= _maxIterations / 5)
            {
                Debug.Log($"Optimization terminated early after {_staleIterations} stale iterations.");
                break;
            }
        }

        return CreateResult();
    }

    private void InitializeDistributionParameters(int numVariables)
    {
        _distributionParameters = new double[2][]; // 0: Mean, 1: StdDev
        _distributionParameters[0] = Enumerable.Repeat(Math.PI, numVariables).ToArray(); // Means
        _distributionParameters[1] = Enumerable.Repeat(0.5, numVariables).ToArray();    // StdDevs
    }

    private void GenerateInitialPopulation(int numVariables)
    {
        _population = GeneratePopulation(numVariables);
    }

    private void EvaluatePopulation(Func<double[], double> objectiveFunction)
    {
        _evaluations = new double[_generationSize];
        for (int i = 0; i < _generationSize; i++)
        {
            _evaluations[i] = InvokeFunctionUsingReflection(objectiveFunction, _population[i]);
        }
    }

    private void UpdateBestSolution()
    {
        double currentBestCost = _evaluations.Min();
        if (currentBestCost < _bestCost)
        {
            _bestCost = currentBestCost;
            _bestIndividual = _population[Array.IndexOf(_evaluations, _bestCost)];
            _staleIterations = 0;
        }
        else
        {
            _staleIterations++;
        }
    }

    private void UpdateDistributionParameters()
    {
        int eliteCount = (int)(_alpha * _generationSize);
        var eliteIndices = _evaluations
            .Select((value, index) => (value, index))
            .OrderBy(pair => pair.value)
            .Take(eliteCount)
            .Select(pair => pair.index)
            .ToArray();

        var elitePopulation = eliteIndices.Select(index => _population[index]).ToArray();

        for (int i = 0; i < _distributionParameters[0].Length; i++)
        {
            _distributionParameters[0][i] = elitePopulation.Average(ind => ind[i]);
            _distributionParameters[1][i] = Math.Max(0.3, elitePopulation.StandardDeviation(i));
        }
    }

    private void GenerateNewPopulation()
    {
        _population = GeneratePopulation(_distributionParameters[0].Length);
    }

    private double[][] GeneratePopulation(int numVariables)
    {
        System.Random random = new System.Random();
        double[][] population = new double[_generationSize][];

        for (int i = 0; i < _generationSize; i++)
        {
            population[i] = new double[numVariables];
            for (int j = 0; j < numVariables; j++)
            {
                population[i][j] = SampleNormal(random, _distributionParameters[0][j], _distributionParameters[1][j]);
            }
        }

        return population;
    }

    private static double SampleNormal(System.Random random, double mean, double stdDev)
    {
        // Box-Muller transform
        double u1 = 1.0 - random.NextDouble();
        double u2 = 1.0 - random.NextDouble();
        double standardNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        return mean + stdDev * standardNormal;
    }

    private double InvokeFunctionUsingReflection(Func<double[], double> function, double[] parameters)
    {
        MethodInfo methodInfo = function.Method;
        object target = function.Target ?? throw new InvalidOperationException("Function target cannot be null.");
        return (double)methodInfo.Invoke(target, new object[] { parameters });
    }

    private OptimizerResult CreateResult()
    {
        return new OptimizerResult
        {
            Parameters = _bestIndividual,
            Loss = _bestCost,
            FunctionEvaluations = _generationSize * _maxIterations,
            Iterations = _maxIterations
        };
    }

    public class OptimizerResult
    {
        public double[] Parameters { get; set; }
        public double Loss { get; set; }
        public int FunctionEvaluations { get; set; }
        public int Iterations { get; set; }
    }
}

public static class StatisticsExtensions
{
    public static double StandardDeviation(this IEnumerable<double[]> population, int variableIndex)
    {
        var values = population.Select(individual => individual[variableIndex]);
        double average = values.Average();
        double variance = values.Select(value => Math.Pow(value - average, 2)).Average();
        return Math.Sqrt(variance);
    }
}
