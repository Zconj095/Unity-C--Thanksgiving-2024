using UnityEngine;
using System;
using System.Collections.Generic;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Unity-compatible implementation of the Fast Mean Shift clustering algorithm.
    /// </summary>
    [Serializable]
    public class FastMeanShift
    {
        [SerializeField] private float bandwidth = 1.0f;
        [SerializeField] private float tolerance = 1e-3f;
        [SerializeField] private int maxIterations = 100;

        private List<Vector3> seeds = new List<Vector3>();
        private List<Vector3> modes = new List<Vector3>();
        private bool useParallelProcessing = false;

        /// <summary>
        /// Bandwidth parameter controlling the smoothness of the clustering.
        /// </summary>
        public float Bandwidth
        {
            get => bandwidth;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Bandwidth must be greater than zero.");
                bandwidth = value;
            }
        }

        /// <summary>
        /// Computes clusters using Fast Mean Shift on the given points.
        /// </summary>
        /// <param name="points">Points to cluster.</param>
        /// <returns>Cluster labels for each input point.</returns>
        public int[] Compute(Vector3[] points)
        {
            if (points == null || points.Length == 0)
            {
                Debug.LogError("Input points cannot be null or empty.");
                return Array.Empty<int>();
            }

            // Initialize seeds by binning points
            seeds = InitializeSeeds(points);

            // Run the Fast Mean Shift algorithm
            if (useParallelProcessing)
                PerformMeanShiftParallel(points);
            else
                PerformMeanShift(points);

            // Assign labels to the original points based on modes
            return AssignLabels(points);
        }

        /// <summary>
        /// Initializes seeds by binning points into a grid.
        /// </summary>
        /// <param name="points">Input points.</param>
        private List<Vector3> InitializeSeeds(Vector3[] points)
        {
            var seedSet = new HashSet<Vector3>();

            foreach (var point in points)
            {
                // Bin points to the nearest grid
                Vector3 rounded = RoundVector(point, Bandwidth);
                seedSet.Add(rounded);
            }

            return new List<Vector3>(seedSet);
        }

        /// <summary>
        /// Perform the Mean Shift algorithm iteratively to find modes.
        /// </summary>
        /// <param name="points">Input points.</param>
        private void PerformMeanShift(Vector3[] points)
        {
            foreach (var seed in seeds)
            {
                Vector3 mode = ShiftToMode(seed, points);
                if (!modes.Contains(mode))
                    modes.Add(mode);
            }
        }

        /// <summary>
        /// Perform the Mean Shift algorithm in parallel to find modes.
        /// </summary>
        /// <param name="points">Input points.</param>
        private void PerformMeanShiftParallel(Vector3[] points)
        {
            var modeSet = new HashSet<Vector3>();

            System.Threading.Tasks.Parallel.ForEach(seeds, seed =>
            {
                Vector3 mode = ShiftToMode(seed, points);
                lock (modeSet)
                {
                    if (!modeSet.Contains(mode))
                        modeSet.Add(mode);
                }
            });

            modes = new List<Vector3>(modeSet);
        }

        /// <summary>
        /// Shifts a point iteratively toward the nearest mode.
        /// </summary>
        /// <param name="point">Starting point.</param>
        /// <param name="points">All input points.</param>
        /// <returns>The mode to which the point converges.</returns>
        private Vector3 ShiftToMode(Vector3 point, Vector3[] points)
        {
            Vector3 mean = Vector3.zero;
            Vector3 shift = Vector3.zero;

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                mean = ComputeMean(point, points);

                shift = mean - point;
                if (shift.magnitude < tolerance)
                    break;

                point += shift;
            }

            return RoundVector(point, Bandwidth);
        }

        /// <summary>
        /// Computes the mean of points within the bandwidth radius.
        /// </summary>
        /// <param name="center">Center of the bandwidth region.</param>
        /// <param name="points">All input points.</param>
        /// <returns>The mean of the nearby points.</returns>
        private Vector3 ComputeMean(Vector3 center, Vector3[] points)
        {
            Vector3 sum = Vector3.zero;
            int count = 0;

            foreach (var point in points)
            {
                if (Vector3.Distance(center, point) < Bandwidth)
                {
                    sum += point;
                    count++;
                }
            }

            return count > 0 ? sum / count : center;
        }

        /// <summary>
        /// Assigns cluster labels to each point based on the closest mode.
        /// </summary>
        /// <param name="points">All input points.</param>
        /// <returns>Cluster labels for each point.</returns>
        private int[] AssignLabels(Vector3[] points)
        {
            int[] labels = new int[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                float minDistance = float.MaxValue;
                int label = -1;

                for (int j = 0; j < modes.Count; j++)
                {
                    float distance = Vector3.Distance(points[i], modes[j]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        label = j;
                    }
                }

                labels[i] = label;
            }

            return labels;
        }

        /// <summary>
        /// Rounds a vector to the nearest grid defined by the bandwidth.
        /// </summary>
        /// <param name="vector">Input vector.</param>
        /// <param name="gridSize">Grid size (bandwidth).</param>
        /// <returns>Rounded vector.</returns>
        private Vector3 RoundVector(Vector3 vector, float gridSize)
        {
            return new Vector3(
                Mathf.Round(vector.x / gridSize) * gridSize,
                Mathf.Round(vector.y / gridSize) * gridSize,
                Mathf.Round(vector.z / gridSize) * gridSize
            );
        }
    }
}
