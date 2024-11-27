using System;
using System.Linq;
using System.Collections.Generic;

namespace EdgeLoreMachineLearning
{
    public class BalancedKMeans
    {
        public int K { get; private set; }
        public int[] Labels { get; private set; }
        public int MaxIterations { get; set; } = 100;
        public double Tolerance { get; set; } = 1e-3;
        public List<double[]> Centroids { get; private set; }

        private readonly Func<double[], double[], double> distanceFunction;

        public BalancedKMeans(int k, Func<double[], double[], double> distance = null)
        {
            if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k), "Number of clusters must be greater than zero.");
            K = k;
            distanceFunction = distance ?? DefaultDistance;
        }

        public void Fit(double[][] data)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Input data cannot be null or empty.");
            if (data.Length < K) throw new ArgumentException("Number of data points must be greater than or equal to the number of clusters.");

            // Initialize centroids randomly
            var random = new Random();
            Centroids = data.OrderBy(_ => random.Next()).Take(K).ToList();

            Labels = new int[data.Length];
            bool converged = false;
            int iterations = 0;

            while (!converged && iterations < MaxIterations)
            {
                // Step 1: Assign clusters using the Hungarian algorithm for balanced clustering
                AssignClusters(data);

                // Step 2: Recalculate centroids
                var newCentroids = RecalculateCentroids(data);

                // Step 3: Check for convergence
                converged = CheckConvergence(Centroids, newCentroids);
                Centroids = newCentroids;

                iterations++;
            }
        }

        private void AssignClusters(double[][] data)
        {
            // Create the cost matrix based on distances
            var costMatrix = CalculateCostMatrix(data);

            // Solve the assignment problem using the Hungarian algorithm
            var solver = new HungarianAlgorithm(costMatrix);
            var assignments = solver.Solve();

            // Assign clusters based on the solution
            for (int i = 0; i < data.Length; i++)
            {
                Labels[i] = assignments[i];
            }
        }

        private double[,] CalculateCostMatrix(double[][] data)
        {
            int n = data.Length;
            double[,] costMatrix = new double[n, K];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < K; j++)
                {
                    costMatrix[i, j] = distanceFunction(data[i], Centroids[j]);
                }
            }

            return costMatrix;
        }

        private List<double[]> RecalculateCentroids(double[][] data)
        {
            var newCentroids = new List<double[]>(K);

            for (int i = 0; i < K; i++)
            {
                var clusterPoints = data.Where((_, index) => Labels[index] == i).ToArray();

                if (clusterPoints.Length > 0)
                {
                    var newCentroid = new double[data[0].Length];
                    foreach (var point in clusterPoints)
                    {
                        for (int d = 0; d < point.Length; d++)
                        {
                            newCentroid[d] += point[d];
                        }
                    }

                    for (int d = 0; d < newCentroid.Length; d++)
                    {
                        newCentroid[d] /= clusterPoints.Length;
                    }

                    newCentroids.Add(newCentroid);
                }
                else
                {
                    throw new InvalidOperationException("Empty cluster detected. Adjust data or initialization.");
                }
            }

            return newCentroids;
        }

        private bool CheckConvergence(List<double[]> oldCentroids, List<double[]> newCentroids)
        {
            for (int i = 0; i < K; i++)
            {
                if (distanceFunction(oldCentroids[i], newCentroids[i]) > Tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        private double DefaultDistance(double[] pointA, double[] pointB)
        {
            double sum = 0;
            for (int i = 0; i < pointA.Length; i++)
            {
                sum += Math.Pow(pointA[i] - pointB[i], 2);
            }
            return Math.Sqrt(sum);
        }
    }

    public class HungarianAlgorithm
    {
        private readonly double[,] costMatrix;
        private readonly int n, m;
        private readonly bool[] rowCover, colCover;
        private readonly int[,] mask;

        public HungarianAlgorithm(double[,] costMatrix)
        {
            this.costMatrix = costMatrix;
            n = costMatrix.GetLength(0);
            m = costMatrix.GetLength(1);
            mask = new int[n, m];
            rowCover = new bool[n];
            colCover = new bool[m];
        }

        public int[] Solve()
        {
            Step1();
            Step2();
            while (true)
            {
                var step = Step3();
                if (step == 0) break;
            }

            var assignments = new int[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (mask[i, j] == 1)
                    {
                        assignments[i] = j;
                        break;
                    }
                }
            }
            return assignments;
        }

        private void Step1()
        {
            for (int i = 0; i < n; i++)
            {
                double minValue = double.MaxValue;
                for (int j = 0; j < m; j++)
                {
                    if (costMatrix[i, j] < minValue)
                    {
                        minValue = costMatrix[i, j];
                    }
                }
                for (int j = 0; j < m; j++)
                {
                    costMatrix[i, j] -= minValue;
                }
            }
        }

        private void Step2()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (costMatrix[i, j] == 0 && !rowCover[i] && !colCover[j])
                    {
                        mask[i, j] = 1;
                        rowCover[i] = true;
                        colCover[j] = true;
                    }
                }
            }

            Array.Clear(rowCover, 0, n);
            Array.Clear(colCover, 0, m);
        }

        private int Step3()
        {
            int rowCount = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (mask[i, j] == 1)
                    {
                        colCover[j] = true;
                    }
                }
            }
            for (int i = 0; i < m; i++)
            {
                if (colCover[i])
                {
                    rowCount++;
                }
            }
            return rowCount == n ? 0 : 1;
        }
    }
}
