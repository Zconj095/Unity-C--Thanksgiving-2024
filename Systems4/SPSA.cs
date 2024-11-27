using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SPSA : MonoBehaviour
{
    private readonly int _maxIter;
    private readonly bool _blocking;
    private readonly double? _allowedIncrease;
    private readonly bool _trustRegion;
    private readonly double _learningRate;
    private readonly double _perturbation;
    private readonly int _lastAvg;
    private readonly int _resamplings;
    private readonly int? _perturbationDims;
    private readonly bool _secondOrder;
    private readonly double _regularization;
    private readonly int _hessianDelay;

    private int _nfev;
    private double[][] _smoothedHessian;

    public SPSA(
        int maxIter = 100,
        bool blocking = false,
        double? allowedIncrease = null,
        bool trustRegion = false,
        double learningRate = 0.1,
        double perturbation = 0.01,
        int lastAvg = 1,
        int resamplings = 1,
        int? perturbationDims = null,
        bool secondOrder = false,
        double regularization = 0.01,
        int hessianDelay = 0)
    {
        _maxIter = maxIter;
        _blocking = blocking;
        _allowedIncrease = allowedIncrease;
        _trustRegion = trustRegion;
        _learningRate = learningRate;
        _perturbation = perturbation;
        _lastAvg = lastAvg;
        _resamplings = resamplings;
        _perturbationDims = perturbationDims;
        _secondOrder = secondOrder;
        _regularization = regularization;
        _hessianDelay = hessianDelay;
    }

    private double[] AverageVectors(double[][] vectors)
    {
        int size = vectors[0].Length; // Assuming all vectors are the same size
        double[] average = new double[size];

        // Sum each component across all vectors
        foreach (var vector in vectors)
        {
            for (int i = 0; i < size; i++)
            {
                average[i] += vector[i];
            }
        }

        // Divide by the number of vectors to get the average
        for (int i = 0; i < size; i++)
        {
            average[i] /= vectors.Length;
        }

        return average;
    }


    public OptimizerResult Minimize(Func<double[], double> objectiveFunction, double[] initialPoint)
    {
        double[] parameters = (double[])initialPoint.Clone();
        double currentLoss = objectiveFunction(parameters);
        _nfev = 1;
        _smoothedHessian = InitializeHessian(parameters.Length);

        Queue<double[]> recentParameters = new Queue<double[]>();
        recentParameters.Enqueue(parameters);

        for (int iteration = 1; iteration <= _maxIter; iteration++)
        {
            double[][] gradients = new double[_resamplings][];
            double[][][] hessians = _secondOrder ? new double[_resamplings][][] : null;

            for (int resample = 0; resample < _resamplings; resample++)
            {
                double[] delta1 = GenerateRandomPerturbation(parameters.Length);
                double[] delta2 = _secondOrder ? GenerateRandomPerturbation(parameters.Length) : null;

                (double loss, double[] gradient, double[][] hessian) = SamplePoint(
                    objectiveFunction, parameters, delta1, delta2);

                gradients[resample] = gradient;
                if (_secondOrder && hessian != null)
                {
                    hessians[resample] = hessian;
                }
            }

            double[] avgGradient = AverageVectors(gradients);
            double[][] avgHessian = _secondOrder && hessians != null ? AverageMatrices(hessians) : null;

            if (_secondOrder && avgHessian != null && iteration > _hessianDelay)
            {
                avgHessian = RegularizeHessian(avgHessian);
                avgGradient = SolveLinearSystem(avgHessian, avgGradient);
            }

            double[] update = avgGradient.Select(v => v * _learningRate).ToArray();

            if (_trustRegion)
            {
                double norm = Math.Sqrt(update.Sum(v => v * v));
                if (norm > 1)
                {
                    update = update.Select(v => v / norm).ToArray();
                }
            }

            double[] newParameters = parameters.Zip(update, (p, u) => p - u).ToArray();
            double newLoss = objectiveFunction(newParameters);
            _nfev++;

            if (_blocking && newLoss > currentLoss + (_allowedIncrease ?? 0))
            {
                Debug.Log($"Iteration {iteration}: Blocked. No improvement.");
                continue;
            }

            parameters = newParameters;
            currentLoss = newLoss;

            recentParameters.Enqueue(parameters);
            if (recentParameters.Count > _lastAvg)
            {
                recentParameters.Dequeue();
            }

            Debug.Log($"Iteration {iteration}: Loss = {currentLoss}");
        }

        double[] finalParameters = recentParameters
            .Aggregate(new double[parameters.Length], (sum, p) => sum.Zip(p, (s, v) => s + v).ToArray())
            .Select(v => v / recentParameters.Count)
            .ToArray();

        return new OptimizerResult
        {
            Parameters = finalParameters,
            Loss = objectiveFunction(finalParameters),
            FunctionEvaluations = _nfev,
            Iterations = _maxIter
        };
    }

    private (double, double[], double[][]) SamplePoint(
        Func<double[], double> objectiveFunction,
        double[] parameters,
        double[] delta1,
        double[] delta2 = null)
    {
        double[] plus = parameters.Zip(delta1, (p, d) => p + _perturbation * d).ToArray();
        double[] minus = parameters.Zip(delta1, (p, d) => p - _perturbation * d).ToArray();

        double lossPlus = objectiveFunction(plus);
        double lossMinus = objectiveFunction(minus);
        _nfev += 2;

        double[] gradient = delta1.Select(d => (lossPlus - lossMinus) / (2 * _perturbation * d)).ToArray();
        double[][] hessian = null;

        if (_secondOrder && delta2 != null)
        {
            double[] crossPlus = parameters.Zip(delta1.Zip(delta2, (d1, d2) => d1 + d2), (p, d) => p + _perturbation * d).ToArray();
            double[] crossMinus = parameters.Zip(delta1.Zip(delta2, (d1, d2) => d1 - d2), (p, d) => p + _perturbation * d).ToArray();

            double lossCrossPlus = objectiveFunction(crossPlus);
            double lossCrossMinus = objectiveFunction(crossMinus);
            _nfev += 2;

            hessian = InitializeHessian(parameters.Length);

            for (int i = 0; i < parameters.Length; i++)
            {
                for (int j = 0; j < parameters.Length; j++)
                {
                    hessian[i][j] = (lossCrossPlus - lossPlus - lossCrossMinus + lossMinus)
                                   / (4 * _perturbation * delta1[i] * delta2[j]);
                }
            }
        }

        return (lossPlus, gradient, hessian);
    }

    private double[][] InitializeHessian(int size)
    {
        double[][] hessian = new double[size][];
        for (int i = 0; i < size; i++)
        {
            hessian[i] = new double[size];
            hessian[i][i] = 1.0; // Identity matrix
        }
        return hessian;
    }

    private double[] GenerateRandomPerturbation(int size)
    {
        System.Random random = new System.Random();
        return Enumerable.Range(0, size).Select(_ => random.NextDouble() * 2 - 1).ToArray();
    }

    private double[][] RegularizeHessian(double[][] hessian)
    {
        for (int i = 0; i < hessian.Length; i++)
        {
            hessian[i][i] += _regularization;
        }
        return hessian;
    }

    private double[][] AverageMatrices(double[][][] matrices)
    {
        int size = matrices[0].Length;
        double[][] average = InitializeHessian(size);

        foreach (var matrix in matrices)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    average[i][j] += matrix[i][j];
                }
            }
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                average[i][j] /= matrices.Length;
            }
        }

        return average;
    }

    private double[] SolveLinearSystem(double[][] matrix, double[] vector)
    {
        int size = vector.Length;
        double[] result = new double[size];
        double[][] inverse = InitializeHessian(size);

        for (int i = 0; i < size; i++)
        {
            inverse[i][i] = 1.0 / matrix[i][i];
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i] += inverse[i][j] * vector[j];
            }
        }

        return result;
    }

    public class OptimizerResult
    {
        public double[] Parameters { get; set; }
        public double Loss { get; set; }
        public int FunctionEvaluations { get; set; }
        public int Iterations { get; set; }
    }
}
