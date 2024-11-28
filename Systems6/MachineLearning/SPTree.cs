using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Space-Partitioning Tree for Unity.
    /// </summary>
    public class SPTree
    {
        private readonly int dimension;

        /// <summary>
        ///   Gets the dimension of the space covered by this tree.
        /// </summary>
        public int Dimension => dimension;

        /// <summary>
        ///   Root node of the tree.
        /// </summary>
        public SPTreeNode Root { get; private set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="SPTree"/> class.
        /// </summary>
        /// <param name="dimensions">The dimensions of the space partitioned by the tree.</param>
        public SPTree(int dimensions)
        {
            if (dimensions <= 0)
                throw new ArgumentException("Dimension must be greater than zero.", nameof(dimensions));

            this.dimension = dimensions;
        }

        /// <summary>
        ///   Creates a new space-partitioning tree from the given points.
        /// </summary>
        /// <param name="points">The points to be added to the tree.</param>
        /// <returns>A <see cref="SPTree"/> populated with the given data points.</returns>
        public static SPTree FromData(double[][] points)
        {
            if (points == null || points.Length == 0)
                throw new ArgumentException("Points cannot be null or empty.", nameof(points));

            int D = points[0].Length;
            int N = points.Length;
            var tree = new SPTree(D);

            // Compute mean, width, and height of the space covered by the points
            var mean_Y = new double[D];
            var min_Y = new double[D];
            var max_Y = new double[D];

            for (int d = 0; d < D; d++)
            {
                min_Y[d] = double.MaxValue;
                max_Y[d] = double.MinValue;
            }

            foreach (var point in points)
            {
                for (int d = 0; d < D; d++)
                {
                    mean_Y[d] += point[d];
                    min_Y[d] = Math.Min(min_Y[d], point[d]);
                    max_Y[d] = Math.Max(max_Y[d], point[d]);
                }
            }

            for (int d = 0; d < D; d++)
                mean_Y[d] /= N;

            var width = new double[D];
            for (int d = 0; d < D; d++)
                width[d] = Math.Max(max_Y[d] - mean_Y[d], mean_Y[d] - min_Y[d]) + 1e-5;

            tree.Root = new SPTreeNode(tree, null, 0, mean_Y, width);

            foreach (var point in points)
                tree.Root.Add(point);

            return tree;
        }

        /// <summary>
        ///   Inserts a point in the Space-Partitioning tree.
        /// </summary>
        /// <param name="point">The point to add.</param>
        /// <returns>True if the point was successfully added; otherwise, false.</returns>
        public bool Add(double[] point)
        {
            if (point == null || point.Length != dimension)
                throw new ArgumentException("Point dimensions must match the tree dimensions.", nameof(point));

            return Root.Add(point);
        }

        /// <summary>
        ///   Computes non-edge forces using the Barnes-Hut algorithm.
        /// </summary>
        /// <param name="point">The point for which to compute forces.</param>
        /// <param name="theta">Threshold parameter for approximations.</param>
        /// <param name="neg_f">Output: Negative forces.</param>
        /// <param name="sum_Q">Output: Sum of the Q-values.</param>
        public void ComputeNonEdgeForces(double[] point, double theta, double[] neg_f, ref double sum_Q)
        {
            if (point == null || point.Length != dimension)
                throw new ArgumentException("Point dimensions must match the tree dimensions.", nameof(point));

            Root.ComputeNonEdgeForces(point, theta, neg_f, ref sum_Q);
        }

        /// <summary>
        ///   Computes edge forces.
        /// </summary>
        /// <param name="points">Points in the space.</param>
        /// <param name="row_P">Row indices for sparse matrix representation.</param>
        /// <param name="col_P">Column indices for sparse matrix representation.</param>
        /// <param name="val_P">Values for sparse matrix representation.</param>
        /// <param name="pos_f">Output: Positive forces.</param>
        public void ComputeEdgeForces(double[][] points, int[] row_P, int[] col_P, double[] val_P, double[][] pos_f)
        {
            if (points == null || points.Length == 0 || points[0].Length != dimension)
                throw new ArgumentException("Points array must be non-empty and match tree dimensions.", nameof(points));

            double[] buff = new double[dimension];
            for (int n = 0; n < points.Length; n++)
            {
                for (int i = row_P[n]; i < row_P[n + 1]; i++)
                {
                    int j = col_P[i];
                    double D = 1.0;

                    for (int d = 0; d < dimension; d++)
                        buff[d] = points[n][d] - points[j][d];

                    for (int d = 0; d < dimension; d++)
                        D += buff[d] * buff[d];

                    D = val_P[i] / D;

                    for (int d = 0; d < dimension; d++)
                        pos_f[n][d] += D * buff[d];
                }
            }
        }
    }
}
