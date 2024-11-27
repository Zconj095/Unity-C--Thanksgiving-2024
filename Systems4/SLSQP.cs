using System;
using System.Collections.Generic;
using UnityEngine;

public class SLSQP : MonoBehaviour
{
    private readonly Dictionary<string, object> _options;
    private readonly int _maxEvalsGrouped;
    private readonly double? _tol;

    public SLSQP(
        int maxIter = 100,
        bool disp = false,
        double ftol = 1e-06,
        double? tol = null,
        double eps = 1.4901161193847656e-08,
        Dictionary<string, object>? options = null,
        int maxEvalsGrouped = 1)
    {
        // Initialize options
        _options = options ?? new Dictionary<string, object>();
        _options["maxiter"] = maxIter;
        _options["disp"] = disp;
        _options["ftol"] = ftol;
        _options["eps"] = eps;

        _tol = tol;
        _maxEvalsGrouped = Math.Max(1, maxEvalsGrouped);
    }

    public OptimizerResult Minimize(
        Func<double[], double> objectiveFunction,
        double[] initialPoint,
        Func<double[], double[]>? gradientFunction = null,
        Tuple<double, double>[]? bounds = null,
        Func<double[], double[]>? equalityConstraints = null,
        Func<double[], double[]>? inequalityConstraints = null)
    {
        // Handle gradient
        if (gradientFunction == null)
        {
            gradientFunction = (x) => ComputeNumericalGradient(objectiveFunction, x);
        }

        double[] currentPoint = (double[])initialPoint.Clone();
        double currentLoss = objectiveFunction(currentPoint);

        for (int iteration = 0; iteration < (int)_options["maxiter"]; iteration++)
        {
            double[] gradient = gradientFunction(currentPoint);
            double[] step = ComputeStep(gradient);

            // Apply step and enforce bounds
            for (int i = 0; i < currentPoint.Length; i++)
            {
                currentPoint[i] -= step[i];
                if (bounds != null)
                {
                    currentPoint[i] = Math.Clamp(currentPoint[i], bounds[i].Item1, bounds[i].Item2);
                }
            }

            // Evaluate new loss
            double newLoss = objectiveFunction(currentPoint);

            // Check constraints
            if (equalityConstraints != null)
            {
                double[] eq = equalityConstraints(currentPoint);
                if (!AreConstraintsSatisfied(eq))
                {
                    Debug.LogWarning("Equality constraints not satisfied. Terminating.");
                    break;
                }
            }
            if (inequalityConstraints != null)
            {
                double[] ineq = inequalityConstraints(currentPoint);
                if (!AreConstraintsSatisfied(ineq, isEquality: false))
                {
                    Debug.LogWarning("Inequality constraints not satisfied. Terminating.");
                    break;
                }
            }

            // Check termination criteria
            if (_tol.HasValue && Math.Abs(currentLoss - newLoss) < _tol.Value)
            {
                Debug.Log($"Converged at iteration {iteration + 1}.");
                break;
            }

            currentLoss = newLoss;
            Debug.Log($"Iteration {iteration + 1}: Loss = {currentLoss}");
        }

        return new OptimizerResult
        {
            Parameters = currentPoint,
            Loss = currentLoss,
            Iterations = (int)_options["maxiter"]
        };
    }

    private double[] ComputeNumericalGradient(Func<double[], double> objectiveFunction, double[] x)
    {
        double eps = (double)_options["eps"];
        double[] gradient = new double[x.Length];

        for (int i = 0; i < x.Length; i++)
        {
            double[] xPlus = (double[])x.Clone();
            double[] xMinus = (double[])x.Clone();

            xPlus[i] += eps;
            xMinus[i] -= eps;

            gradient[i] = (objectiveFunction(xPlus) - objectiveFunction(xMinus)) / (2 * eps);
        }

        return gradient;
    }

    private double[] ComputeStep(double[] gradient)
    {
        double ftol = (double)_options["ftol"];
        double[] step = new double[gradient.Length];

        for (int i = 0; i < gradient.Length; i++)
        {
            step[i] = ftol * gradient[i];
        }

        return step;
    }

    private bool AreConstraintsSatisfied(double[] constraints, bool isEquality = true)
    {
        double threshold = 1e-6;
        foreach (double constraint in constraints)
        {
            if (isEquality && Math.Abs(constraint) > threshold)
            {
                return false;
            }
            if (!isEquality && constraint < 0)
            {
                return false;
            }
        }
        return true;
    }

    public class OptimizerResult
    {
        public double[] Parameters { get; set; }
        public double Loss { get; set; }
        public int Iterations { get; set; }
    }

    public object InvokePrivateMethod(string methodName, object[] parameters)
    {
        var method = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (method != null)
        {
            return method.Invoke(this, parameters);
        }
        throw new ArgumentException($"Method {methodName} does not exist.");
    }
}
