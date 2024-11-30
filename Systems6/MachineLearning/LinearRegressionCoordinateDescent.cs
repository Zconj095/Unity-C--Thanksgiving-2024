using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class LinearRegressionCoordinateDescent<TModel, TKernel, TInput>
        where TModel : class
        where TKernel : class
    {
        private TModel _model;
        private TKernel _kernel;

        private int _maxIterations = 1000;
        private double _tolerance = 0.1;
        private double _epsilon = 0.1;
        private double _complexity = 1.0;

        private double[] _alpha;
        private double[] _beta;
        private double[] _weights;
        private double _bias;

        public LinearRegressionCoordinateDescent(TModel model, TKernel kernel)
        {
            _model = model;
            _kernel = kernel;
        }

        public int MaxIterations
        {
            get => _maxIterations;
            set => _maxIterations = value;
        }

        public double Tolerance
        {
            get => _tolerance;
            set => _tolerance = value;
        }

        public double Epsilon
        {
            get => _epsilon;
            set => _epsilon = value;
        }

        public double Complexity
        {
            get => _complexity;
            set => _complexity = value;
        }

        public void Train(TInput[] inputs, double[] outputs)
        {
            int sampleCount = inputs.Length;
            int featureCount = GetFeatureCount(inputs) + 1;

            _alpha = new double[sampleCount];
            _beta = new double[sampleCount];
            _weights = new double[featureCount];
            _bias = 0;

            double[] QD = new double[sampleCount];
            int[] indices = new int[sampleCount];

            // Initialize values
            for (int i = 0; i < sampleCount; i++)
            {
                QD[i] = KernelFunction(inputs[i], inputs[i]);
                indices[i] = i;
            }

            int iter = 0;
            double GmaxOld = double.PositiveInfinity;
            double Gnorm1Init = -1.0;
            var rand = new System.Random();

            while (iter < _maxIterations)
            {
                double GmaxNew = 0;
                double Gnorm1New = 0;

                // Shuffle indices
                for (int i = 0; i < sampleCount; i++)
                {
                    int j = i + rand.Next(sampleCount - i);
                    (indices[i], indices[j]) = (indices[j], indices[i]);
                }

                for (int s = 0; s < sampleCount; s++)
                {
                    int i = indices[s];
                    double G = -outputs[i] + KernelDotProduct(_weights, inputs[i]);

                    double Gp = G + _epsilon;
                    double Gn = G - _epsilon;

                    double violation = 0;

                    if (_beta[i] == 0)
                    {
                        if (Gp < 0)
                            violation = -Gp;
                        else if (Gn > 0)
                            violation = Gn;
                    }
                    else if (_beta[i] > 0)
                        violation = Math.Abs(Gp);
                    else
                        violation = Math.Abs(Gn);

                    GmaxNew = Math.Max(GmaxNew, violation);
                    Gnorm1New += violation;

                    // Newton direction
                    double H = QD[i] + 0.5 / _complexity;
                    double d;
                    if (Gp < H * _beta[i])
                        d = -Gp / H;
                    else if (Gn > H * _beta[i])
                        d = -Gn / H;
                    else
                        d = -_beta[i];

                    if (Math.Abs(d) < 1e-12)
                        continue;

                    _beta[i] += d;
                    UpdateWeights(_weights, inputs[i], d);
                }

                if (iter == 0)
                    Gnorm1Init = Gnorm1New;

                iter++;
                if (Gnorm1New <= _tolerance * Gnorm1Init)
                    break;

                GmaxOld = GmaxNew;
            }

            Debug.Log($"Optimization completed in {iter} iterations.");
            UpdateModel(_weights, _bias);
        }

        private int GetFeatureCount(TInput[] inputs)
        {
            MethodInfo getLengthMethod = _kernel.GetType().GetMethod("GetLength");
            if (getLengthMethod == null)
                throw new MissingMethodException("Kernel does not implement GetLength method.");
            return (int)getLengthMethod.Invoke(_kernel, new object[] { inputs });
        }

        private double KernelFunction(TInput x, TInput y)
        {
            MethodInfo kernelFunction = _kernel.GetType().GetMethod("Function");
            if (kernelFunction == null)
                throw new MissingMethodException("Kernel does not implement Function method.");
            return (double)kernelFunction.Invoke(_kernel, new object[] { x, y });
        }

        private double KernelDotProduct(double[] weights, TInput x)
        {
            MethodInfo kernelProduct = _kernel.GetType().GetMethod("Product");
            if (kernelProduct == null)
                throw new MissingMethodException("Kernel does not implement Product method.");
            return (double)kernelProduct.Invoke(_kernel, new object[] { weights, x });
        }

        private void UpdateWeights(double[] weights, TInput x, double d)
        {
            MethodInfo kernelProduct = _kernel.GetType().GetMethod("Product");
            if (kernelProduct == null)
                throw new MissingMethodException("Kernel does not implement Product method.");
            kernelProduct.Invoke(_kernel, new object[] { d, x, true });
        }

        private void UpdateModel(double[] weights, double bias)
        {
            PropertyInfo supportVectorsProperty = _model.GetType().GetProperty("SupportVectors");
            PropertyInfo weightsProperty = _model.GetType().GetProperty("Weights");
            PropertyInfo thresholdProperty = _model.GetType().GetProperty("Threshold");

            if (supportVectorsProperty != null)
                supportVectorsProperty.SetValue(_model, new[] { weights });

            if (weightsProperty != null)
                weightsProperty.SetValue(_model, new[] { 1.0 });

            if (thresholdProperty != null)
                thresholdProperty.SetValue(_model, bias);

            Debug.Log("Model updated with learned parameters.");
        }
    }
}
