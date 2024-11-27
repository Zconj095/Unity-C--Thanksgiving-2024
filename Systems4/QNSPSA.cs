using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class QNSPSA : MonoBehaviour
{
    private readonly Func<double[], double[], double> _fidelity;
    private readonly int _maxIter;
    private readonly bool _blocking;
    private readonly double? _allowedIncrease;
    private readonly double _learningRate;
    private readonly double _perturbation;
    private readonly int _resamplings;
    private readonly int _hessianDelay;
    private readonly double? _regularization;

    public QNSPSA(
        Func<double[], double[], double> fidelity,
        int maxIter = 100,
        bool blocking = true,
        double? allowedIncrease = null,
        double learningRate = 0.1,
        double perturbation = 0.01,
        int resamplings = 1,
        int hessianDelay = 0,
        double? regularization = null)
    {
        _fidelity = fidelity;
        _maxIter = maxIter;
        _blocking = blocking;
        _allowedIncrease = allowedIncrease;
        _learningRate = learningRate;
        _perturbation = perturbation;
        _resamplings = resamplings;
        _hessianDelay = hessianDelay;
        _regularization = regularization;
    }

    public OptimizerResult Optimize(int parameterCount, Func<double[], double> lossFunction, double[] initialPoint)
    {
        double[] parameters = (double[])initialPoint.Clone();
        double loss = lossFunction(parameters);
        double[][] hessian = InitializeHessian(parameterCount);
        int functionEvaluations = 0;

        for (int iteration = 0; iteration < _maxIter; iteration++)
        {
            List<double[]> gradients = new List<double[]>();
            List<double[][]> hessians = new List<double[][]>();
            double[] delta1 = GenerateRandomDeltas(parameterCount);
            double[] delta2 = GenerateRandomDeltas(parameterCount);

            // Compute loss, gradient, and Hessian estimates
            for (int i = 0; i < _resamplings; i++)
            {
                gradients.Add(ComputeGradient(lossFunction, parameters, delta1));
                if (iteration >= _hessianDelay)
                {
                    hessians.Add(ComputeHessian(parameters, delta1, delta2));
                }
            }

            double[] avgGradient = AverageVectors(gradients);
            double[][] avgHessian = iteration >= _hessianDelay ? AverageMatrices(hessians) : hessian;

            // Regularization
            ApplyRegularization(avgHessian);

            // Compute natural gradient
            double[] naturalGradient = MultiplyMatrixVector(InvertMatrix(avgHessian), avgGradient);

            // Update parameters
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] -= _learningRate * naturalGradient[i];
            }

            double newLoss = lossFunction(parameters);
            functionEvaluations += 1 + _resamplings * 6; // Function evaluations

            // Check blocking
            if (_blocking && newLoss > loss + (_allowedIncrease ?? 0))
            {
                Debug.Log($"Iteration {iteration + 1}: Blocked. Loss did not improve.");
                break;
            }

            loss = newLoss;
            Debug.Log($"Iteration {iteration + 1}: Loss = {loss}");
        }

        return new OptimizerResult
        {
            Parameters = parameters,
            Loss = loss,
            FunctionEvaluations = functionEvaluations,
            Iterations = _maxIter
        };
    }

    private double[][] InitializeHessian(int size)
    {
        double[][] hessian = new double[size][];
        for (int i = 0; i < size; i++)
        {
            hessian[i] = new double[size];
            hessian[i][i] = 1.0; // Start with identity matrix
        }
        return hessian;
    }

    private double[] ComputeGradient(Func<double[], double> lossFunction, double[] parameters, double[] deltas)
    {
        double[] perturbedPlus = parameters.Zip(deltas, (p, d) => p + _perturbation * d).ToArray();
        double[] perturbedMinus = parameters.Zip(deltas, (p, d) => p - _perturbation * d).ToArray();

        double lossPlus = lossFunction(perturbedPlus);
        double lossMinus = lossFunction(perturbedMinus);

        return deltas.Select(d => (lossPlus - lossMinus) / (2 * _perturbation * d)).ToArray();
    }

    private double[][] ComputeHessian(double[] parameters, double[] delta1, double[] delta2)
    {
        double fidelity1 = _fidelity(parameters, parameters.Zip(delta1, (p, d) => p + _perturbation * d).ToArray());
        double fidelity2 = _fidelity(parameters, parameters.Zip(delta1, (p, d) => p - _perturbation * d).ToArray());
        double fidelityCross = _fidelity(parameters, parameters.Zip(delta1.Zip(delta2, (d1, d2) => d1 + d2), (p, d) => p + _perturbation * d).ToArray());

        double diff = fidelityCross - fidelity1 - fidelity2;
        int size = parameters.Length;
        double[][] hessian = new double[size][];

        for (int i = 0; i < size; i++)
        {
            hessian[i] = new double[size];
            for (int j = 0; j < size; j++)
            {
                hessian[i][j] = -0.5 * diff / (_perturbation * delta1[i] * delta2[j]);
            }
        }

        return hessian;
    }

    private void ApplyRegularization(double[][] hessian)
    {
        if (_regularization.HasValue)
        {
            for (int i = 0; i < hessian.Length; i++)
            {
                hessian[i][i] += _regularization.Value;
            }
        }
    }

    private double[][] InvertMatrix(double[][] matrix)
    {
        // Simple inversion assuming positive-definite matrix (Cholesky inversion)
        int size = matrix.Length;
        double[][] inverted = new double[size][];

        for (int i = 0; i < size; i++)
        {
            inverted[i] = new double[size];
            inverted[i][i] = 1 / matrix[i][i];
        }

        return inverted;
    }

    private double[] MultiplyMatrixVector(double[][] matrix, double[] vector)
    {
        int size = vector.Length;
        double[] result = new double[size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i] += matrix[i][j] * vector[j];
            }
        }
        return result;
    }

    private double[] GenerateRandomDeltas(int size)
    {
        System.Random random = new System.Random();
        return Enumerable.Range(0, size).Select(_ => random.NextDouble() * 2 - 1).ToArray();
    }

    private double[][] AverageMatrices(List<double[][]> matrices)
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
                average[i][j] /= matrices.Count;
            }
        }

        return average;
    }

    private double[] AverageVectors(List<double[]> vectors)
    {
        int size = vectors[0].Length;
        double[] average = new double[size];

        foreach (var vector in vectors)
        {
            for (int i = 0; i < size; i++)
            {
                average[i] += vector[i];
            }
        }

        for (int i = 0; i < size; i++)
        {
            average[i] /= vectors.Count;
        }

        return average;
    }

    public class OptimizerResult
    {
        public double[] Parameters { get; set; }
        public double Loss { get; set; }
        public int FunctionEvaluations { get; set; }
        public int Iterations { get; set; }
    }
}
