using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class SupportVectorReduction : SupportVectorReductionBase<SupportVectorRegressionSVM<SupportVectorRegressionIKernel<double[]>, double[]>, SupportVectorRegressionIKernel<double[]>, double[]>
    {
        public SupportVectorReduction(SupportVectorRegressionSVM<SupportVectorRegressionIKernel<double[]>, double[]> machine) : base(machine) { }
    }

    public abstract class SupportVectorReductionBase<TModel, TKernel, TInput>
        where TModel : ISupportVectorRegressionSVM<TKernel, TInput>
        where TKernel : ISupportVectorRegressionKernel<TInput>
        where TInput : class
    {
        protected readonly TModel Model;
        private double threshold = 1e-12;

        public double Threshold
        {
            get => threshold;
            set => threshold = value;
        }

        protected SupportVectorReductionBase(TModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public TModel Reduce()
        {
            Debug.Log("Reducing support vectors...");
            var supportVectors = Model.SupportVectors;
            var alpha = (double[])Model.Weights.Clone();
            var kernel = Model.Kernel;

            Debug.Log("Creating Gram matrix...");
            double[][] gramMatrix = CreateGramMatrix(supportVectors, kernel);

            Debug.Log("Performing row-reduction...");
            var (rref, pivot) = ReducedRowEchelonForm(gramMatrix);

            Debug.Log("Identifying independent vectors...");
            for (int i = 0; i < supportVectors.Length; i++)
            {
                int row = pivot[i];
                if (row >= supportVectors.Length - rref.Length)
                {
                    double coeff = alpha[row];
                    for (int j = 0; j < rref.Length; j++)
                        alpha[j] += coeff * rref[j][row];
                    alpha[row] = 0;
                }
            }

            Debug.Log("Retaining non-zero weights...");
            int[] retainedIndices = RetainNonZero(alpha, threshold);

            Model.Weights = Extract(alpha, retainedIndices);
            Model.SupportVectors = Extract(supportVectors, retainedIndices);

            Debug.Log("Reduction complete.");
            return Model;
        }

        private double[][] CreateGramMatrix(TInput[] supportVectors, TKernel kernel)
        {
            int n = supportVectors.Length;
            double[][] gramMatrix = new double[n][];
            for (int i = 0; i < n; i++)
            {
                gramMatrix[i] = new double[n];
                for (int j = 0; j < n; j++)
                    gramMatrix[i][j] = kernel.Compute(supportVectors[i], supportVectors[j]);
            }
            return gramMatrix;
        }

        private (double[][], int[]) ReducedRowEchelonForm(double[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            double[][] rref = new double[rows][];
            for (int i = 0; i < rows; i++) rref[i] = (double[])matrix[i].Clone();

            int[] pivot = new int[cols];
            for (int i = 0; i < cols; i++) pivot[i] = -1;

            for (int col = 0, row = 0; col < cols && row < rows; col++)
            {
                int maxRow = row;
                for (int k = row + 1; k < rows; k++)
                    if (Math.Abs(rref[k][col]) > Math.Abs(rref[maxRow][col]))
                        maxRow = k;

                if (Math.Abs(rref[maxRow][col]) < 1e-10)
                    continue;

                (rref[row], rref[maxRow]) = (rref[maxRow], rref[row]);

                double scale = rref[row][col];
                for (int j = col; j < cols; j++) rref[row][j] /= scale;

                for (int i = 0; i < rows; i++)
                {
                    if (i != row && Math.Abs(rref[i][col]) > 1e-10)
                    {
                        double factor = rref[i][col];
                        for (int j = col; j < cols; j++) rref[i][j] -= factor * rref[row][j];
                    }
                }

                pivot[col] = row;
                row++;
            }

            return (rref, pivot);
        }

        private int[] RetainNonZero(double[] array, double threshold)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < array.Length; i++)
                if (Math.Abs(array[i]) > threshold)
                    indices.Add(i);
            return indices.ToArray();
        }

        private T[] Extract<T>(T[] array, int[] indices)
        {
            T[] result = new T[indices.Length];
            for (int i = 0; i < indices.Length; i++)
                result[i] = array[indices[i]];
            return result;
        }
    }

    public class SupportVectorRegressionSVM<TKernel, TInput> : ISupportVectorRegressionSVM<TKernel, TInput>
        where TKernel : ISupportVectorRegressionKernel<TInput>
    {
        public TInput[] SupportVectors { get; set; }
        public double[] Weights { get; set; }
        public TKernel Kernel { get; }

        public SupportVectorRegressionSVM(TKernel kernel)
        {
            Kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }
    }

    public class SupportVectorRegressionIKernel<TInput> : ISupportVectorRegressionKernel<TInput>
    {
        public double Compute(TInput a, TInput b)
        {
            // Implement the kernel computation logic
            return 0.0; // Replace with your specific computation
        }
    }

    public interface ISupportVectorRegressionSVM<TKernel, TInput>
        where TKernel : ISupportVectorRegressionKernel<TInput>
    {
        TInput[] SupportVectors { get; set; }
        double[] Weights { get; set; }
        TKernel Kernel { get; }
    }

    public interface ISupportVectorRegressionKernel<TInput>
    {
        double Compute(TInput a, TInput b);
    }
}
