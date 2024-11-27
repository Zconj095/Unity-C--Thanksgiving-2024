using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TNCOptimizer
{
    private readonly int _maxIterations;
    private readonly bool _display;
    private readonly double _accuracy;
    private readonly double _fTolerance;
    private readonly double _xTolerance;
    private readonly double _gTolerance;
    private readonly double _epsilon;
    private readonly double? _tolerance;

    public TNCOptimizer(
        int maxIterations = 100,
        bool display = false,
        double accuracy = 0,
        double fTolerance = -1,
        double xTolerance = -1,
        double gTolerance = -1,
        double epsilon = 1e-8,
        double? tolerance = null)
    {
        _maxIterations = maxIterations;
        _display = display;
        _accuracy = accuracy > 0 ? accuracy : Math.Sqrt(double.Epsilon);
        _fTolerance = fTolerance >= 0 ? fTolerance : 0;
        _xTolerance = xTolerance >= 0 ? xTolerance : Math.Sqrt(double.Epsilon);
        _gTolerance = gTolerance >= 0 ? gTolerance : 1e-2 * Math.Sqrt(_accuracy);
        _epsilon = epsilon;
        _tolerance = tolerance;
    }

    public OptimizerResult Minimize(
        Func<double[], double> objectiveFunction,
        double[] initialPoint,
        Func<double[], double[]>? gradientFunction = null,
        Tuple<double, double>[]? bounds = null)
    {
        double[] parameters = (double[])initialPoint.Clone();
        int iterations = 0;
        int functionEvaluations = 0;
        int gradientEvaluations = 0;
        double currentLoss = objectiveFunction(parameters);

        while (iterations < _maxIterations)
        {
            iterations++;
            if (gradientFunction == null)
            {
                gradientFunction = x => ComputeNumericalGradient(objectiveFunction, x);
            }

            double[] gradient = gradientFunction(parameters);
            gradientEvaluations++;

            // Convergence check using gradient tolerance
            if (gradient.Max(g => Math.Abs(g)) < _gTolerance)
            {
                break;
            }

            // Compute step direction (Newton-CG approximation)
            double[] step = ComputeStep(gradient);

            // Enforce bounds
            if (bounds != null)
            {
                parameters = ApplyBounds(parameters, step, bounds);
            }
            else
            {
                parameters = parameters.Zip(step, (p, s) => p - s).ToArray();
            }

            // Evaluate new loss
            double newLoss = objectiveFunction(parameters);
            functionEvaluations++;

            // Convergence check using function tolerance
            if (Math.Abs(currentLoss - newLoss) < _fTolerance)
            {
                break;
            }

            currentLoss = newLoss;

            if (_display)
            {
                Debug.Log($"Iteration {iterations}: Loss = {currentLoss}");
            }
        }

        return new OptimizerResult(parameters, currentLoss, functionEvaluations, gradientEvaluations, iterations);
    }

    private double[] ComputeNumericalGradient(Func<double[], double> objectiveFunction, double[] parameters)
    {
        double[] gradient = new double[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            double[] forward = (double[])parameters.Clone();
            double[] backward = (double[])parameters.Clone();

            forward[i] += _epsilon;
            backward[i] -= _epsilon;

            double lossForward = objectiveFunction(forward);
            double lossBackward = objectiveFunction(backward);

            gradient[i] = (lossForward - lossBackward) / (2 * _epsilon);
        }

        return gradient;
    }

    private double[] ComputeStep(double[] gradient)
    {
        return gradient.Select(g => g * _xTolerance).ToArray();
    }

    private double[] ApplyBounds(double[] parameters, double[] step, Tuple<double, double>[] bounds)
    {
        double[] boundedParams = new double[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            double newValue = parameters[i] - step[i];
            boundedParams[i] = Math.Clamp(newValue, bounds[i].Item1, bounds[i].Item2);
        }

        return boundedParams;
    }

    public class OptimizerResult
    {
        public double[] Parameters { get; }
        public double Loss { get; }
        public int FunctionEvaluations { get; }
        public int GradientEvaluations { get; }
        public int Iterations { get; }

        public OptimizerResult(double[] parameters, double loss, int functionEvaluations, int gradientEvaluations, int iterations)
        {
            Parameters = parameters;
            Loss = loss;
            FunctionEvaluations = functionEvaluations;
            GradientEvaluations = gradientEvaluations;
            Iterations = iterations;
        }
    }
}
