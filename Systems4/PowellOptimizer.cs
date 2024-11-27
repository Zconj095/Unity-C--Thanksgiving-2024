using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PowellOptimizer : MonoBehaviour
{
    private readonly Dictionary<string, object> _options = new Dictionary<string, object>();

    public PowellOptimizer(int? maxiter = null, int maxfev = 1000, bool disp = false, double xtol = 0.0001, double? tol = null)
    {
        // Initialize optimization parameters
        _options["maxiter"] = maxiter;
        _options["maxfev"] = maxfev;
        _options["disp"] = disp;
        _options["xtol"] = xtol;
        _options["tol"] = tol;
    }

    public void SetOption(string key, object value)
    {
        if (_options.ContainsKey(key))
        {
            _options[key] = value;
        }
        else
        {
            throw new ArgumentException($"Option '{key}' does not exist.");
        }
    }

    public object GetOption(string key)
    {
        if (_options.ContainsKey(key))
        {
            return _options[key];
        }
        throw new ArgumentException($"Option '{key}' does not exist.");
    }

    public object InvokePrivateMethod(string methodName, object[] parameters)
    {
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (method != null)
        {
            return method.Invoke(this, parameters);
        }
        throw new ArgumentException($"Method '{methodName}' does not exist.");
    }

    public OptimizerResult Minimize(Func<double[], double> objectiveFunction, double[] initialPoint, Tuple<double, double>[]? bounds = null)
    {
        int maxIter = _options["maxiter"] is int ? (int)_options["maxiter"] : int.MaxValue;
        int maxFev = (int)_options["maxfev"];
        double xtol = (double)_options["xtol"];
        double? tol = _options["tol"] as double?;

        int iterations = 0;
        int functionEvaluations = 0;

        double[] currentPoint = (double[])initialPoint.Clone();
        double currentValue = objectiveFunction(currentPoint);

        while (iterations < maxIter && functionEvaluations < maxFev)
        {
            iterations++;
            functionEvaluations++;

            // Perform one-dimensional minimization along each direction
            double[] newPoint = (double[])currentPoint.Clone();
            for (int i = 0; i < currentPoint.Length; i++)
            {
                newPoint[i] += xtol; // Small step in one direction
                double newValue = objectiveFunction(newPoint);
                if (newValue < currentValue)
                {
                    currentValue = newValue;
                    currentPoint[i] = newPoint[i];
                }
                else
                {
                    newPoint[i] -= 2 * xtol; // Try the opposite direction
                    newValue = objectiveFunction(newPoint);
                    if (newValue < currentValue)
                    {
                        currentValue = newValue;
                        currentPoint[i] = newPoint[i];
                    }
                }
            }

            // Check for convergence
            if (tol.HasValue && Math.Abs(currentValue - objectiveFunction(currentPoint)) < tol.Value)
            {
                break;
            }
        }

        return new OptimizerResult
        {
            X = currentPoint,
            Fun = currentValue,
            Iterations = iterations,
            FunctionEvaluations = functionEvaluations
        };
    }

    private double[] OptimizeOneDirection(Func<double[], double> objectiveFunction, double[] point, int direction, double xtol)
    {
        double[] directionPoint = (double[])point.Clone();
        directionPoint[direction] += xtol;

        double bestValue = objectiveFunction(point);
        double bestStep = 0;

        for (double step = -xtol; step <= xtol; step += xtol / 10)
        {
            directionPoint[direction] = point[direction] + step;
            double value = objectiveFunction(directionPoint);
            if (value < bestValue)
            {
                bestValue = value;
                bestStep = step;
            }
        }

        point[direction] += bestStep;
        return point;
    }

    public class OptimizerResult
    {
        public double[] X { get; set; }
        public double Fun { get; set; }
        public int Iterations { get; set; }
        public int FunctionEvaluations { get; set; }
    }
}
