using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Probabilistic Output Calibration for linear machines using Unity Reflection.
    /// </summary>
    public class ProbabilisticOutputCalibration<TModel, TKernel, TInput>
        where TKernel : class
        where TModel : class
    {
        private TModel _model;
        private double[] _distances;
        private double[] _targets;
        private int _maxIterations = 100;
        private double _minStepSize = 1e-10;
        private double _sigma = 1e-12;
        private double _tolerance = 1e-5;

        public ProbabilisticOutputCalibration(TModel model)
        {
            _model = model;
        }

        public int MaxIterations
        {
            get => _maxIterations;
            set => _maxIterations = value;
        }

        public double MinStepSize
        {
            get => _minStepSize;
            set => _minStepSize = value;
        }

        public double Sigma
        {
            get => _sigma;
            set => _sigma = value;
        }

        public double Tolerance
        {
            get => _tolerance;
            set => _tolerance = value;
        }

        public TModel Train(TInput[] inputs, bool[] outputs)
        {
            Debug.Log("Initializing Probabilistic Output Calibration...");

            int positives = 0;
            int negatives = 0;
            _distances = new double[outputs.Length];
            _targets = new double[outputs.Length];

            for (int i = 0; i < outputs.Length; i++)
            {
                if (outputs[i]) positives++;
                else negatives++;
            }

            double high = (positives + 1.0) / (positives + 2.0);
            double low = 1.0 / (negatives + 2.0);

            for (int i = 0; i < outputs.Length; i++)
            {
                _targets[i] = outputs[i] ? high : low;
            }

            double A = 0.0;
            double B = Math.Log((negatives + 1.0) / (positives + 1.0));
            double logLikelihood = ComputeLogLikelihood(A, B);

            for (int iteration = 0; iteration < _maxIterations; iteration++)
            {
                double[] gradient = ComputeGradient(A, B, out double[] hessian);
                double dA, dB;

                UpdateNewtonDirection(gradient, hessian, out dA, out dB);

                double stepSize = 1.0;
                while (stepSize >= _minStepSize)
                {
                    double newA = A + stepSize * dA;
                    double newB = B + stepSize * dB;
                    double newLogLikelihood = ComputeLogLikelihood(newA, newB);

                    if (newLogLikelihood < logLikelihood + 1e-4 * stepSize * (gradient[0] * dA + gradient[1] * dB))
                    {
                        A = newA;
                        B = newB;
                        logLikelihood = newLogLikelihood;
                        break;
                    }

                    stepSize /= 2.0;

                    if (stepSize < _minStepSize)
                    {
                        Debug.LogError("Line search failed: No sufficient decrease.");
                        break;
                    }
                }

                if (stepSize < _minStepSize) break;

                Debug.Log($"Iteration {iteration + 1}, Log Likelihood: {logLikelihood}");
            }

            ApplyCalibration(A, B);
            return _model;
        }

        private double ComputeLogLikelihood(double A, double B)
        {
            double logLikelihood = 0.0;

            for (int i = 0; i < _distances.Length; i++)
            {
                double y = _distances[i] * A + B;

                if (y >= 0)
                {
                    logLikelihood += _targets[i] * y + Math.Log(1 + Math.Exp(-y));
                }
                else
                {
                    logLikelihood += (_targets[i] - 1) * y + Math.Log(1 + Math.Exp(y));
                }
            }

            return logLikelihood;
        }

        private double[] ComputeGradient(double A, double B, out double[] hessian)
        {
            double g1 = 0.0;
            double g2 = 0.0;

            double h11 = _sigma;
            double h22 = _sigma;
            double h21 = 0.0;

            for (int i = 0; i < _distances.Length; i++)
            {
                double y = _distances[i] * A + B;

                double p = y >= 0 ? 1.0 / (1.0 + Math.Exp(-y)) : Math.Exp(y) / (1.0 + Math.Exp(y));
                double q = y >= 0 ? Math.Exp(-y) / (1.0 + Math.Exp(-y)) : 1.0 / (1.0 + Math.Exp(y));

                double d1 = _targets[i] - p;
                double d2 = p * q;

                g1 += _distances[i] * d1;
                g2 += d1;

                h11 += _distances[i] * _distances[i] * d2;
                h22 += d2;
                h21 += _distances[i] * d2;
            }

            hessian = new double[] { h11, h22, h21 };
            return new double[] { g1, g2 };
        }

        private void UpdateNewtonDirection(double[] gradient, double[] hessian, out double dA, out double dB)
        {
            double h11 = hessian[0];
            double h22 = hessian[1];
            double h21 = hessian[2];

            double det = h11 * h22 - h21 * h21;
            dA = -(h22 * gradient[0] - h21 * gradient[1]) / det;
            dB = -(-h21 * gradient[0] + h11 * gradient[1]) / det;
        }

        private void ApplyCalibration(double A, double B)
        {
            FieldInfo weightsField = _model.GetType().GetField("Weights");
            FieldInfo thresholdField = _model.GetType().GetField("Threshold");

            if (weightsField != null)
            {
                double[] weights = (double[])weightsField.GetValue(_model);
                for (int i = 0; i < weights.Length; i++) weights[i] *= -A;
            }

            if (thresholdField != null)
            {
                double threshold = (double)thresholdField.GetValue(_model);
                thresholdField.SetValue(_model, threshold * -A - B);
            }

            MethodInfo setProbabilistic = _model.GetType().GetMethod("SetProbabilistic");
            setProbabilistic?.Invoke(_model, new object[] { true });

            Debug.Log("Calibration applied successfully.");
        }
    }
}
