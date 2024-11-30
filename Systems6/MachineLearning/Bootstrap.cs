using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Implements Bootstrap resampling for model validation in Unity.
    /// </summary>
    public class Bootstrap
    {
        private readonly int[][] _subsampleIndices;
        private readonly int _samples;
        private bool _parallel = true;
        private Func<int[], int[], BootstrapValues> _fitting;

        /// <summary>
        /// Gets the number of bootstrap samplings performed.
        /// </summary>
        public int B => _subsampleIndices.Length;

        /// <summary>
        /// Gets the total number of samples in the dataset.
        /// </summary>
        public int Samples => _samples;

        /// <summary>
        /// Gets the bootstrap samples as indices.
        /// </summary>
        public int[][] Subsamples => _subsampleIndices;

        /// <summary>
        /// Sets the model fitting function.
        /// </summary>
        public Func<int[], int[], BootstrapValues> Fitting
        {
            get => _fitting;
            set => _fitting = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Enables or disables parallel processing.
        /// </summary>
        public bool RunInParallel
        {
            get => _parallel;
            set => _parallel = value;
        }

        public Bootstrap(int size, int resamples) : this(size, resamples, size) { }

        public Bootstrap(int size, int resamples, int subsampleSize)
        {
            _samples = size;
            _subsampleIndices = GenerateSubsamples(size, resamples, subsampleSize);
        }

        public Bootstrap(int size, int[][] resamplings)
        {
            _samples = size;
            _subsampleIndices = resamplings ?? throw new ArgumentNullException(nameof(resamplings));
        }

        public void CreatePartitions(int index, out int[] trainingSet, out int[] validationSet)
        {
            if (index < 0 || index >= Subsamples.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            trainingSet = Subsamples[index];
            validationSet = Enumerable.Range(0, Samples).Except(trainingSet).ToArray();
        }

        public BootstrapResult Compute()
        {
            if (_fitting == null)
                throw new InvalidOperationException("Fitting function must be defined.");

            var results = new BootstrapValues[Subsamples.Length];

            if (_parallel)
            {
                Parallel.For(0, Subsamples.Length, i =>
                {
                    CreatePartitions(i, out var trainingSet, out var validationSet);
                    results[i] = _fitting(trainingSet, validationSet);
                });
            }
            else
            {
                for (int i = 0; i < Subsamples.Length; i++)
                {
                    CreatePartitions(i, out var trainingSet, out var validationSet);
                    results[i] = _fitting(trainingSet, validationSet);
                }
            }

            return new BootstrapResult(this, results);
        }

        public void GetPartitionSize(int index, out int trainingCount, out int validationCount)
        {
            if (index < 0 || index >= _subsampleIndices.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            trainingCount = _subsampleIndices[index].Length;
            validationCount = _samples - trainingCount;
        }

        private static int[][] GenerateSubsamples(int size, int resamples, int subsampleSize)
        {
            var subsamples = new int[resamples][];
            var random = new System.Random();

            for (int i = 0; i < resamples; i++)
            {
                subsamples[i] = new int[subsampleSize];
                for (int j = 0; j < subsampleSize; j++)
                    subsamples[i][j] = random.Next(0, size);
            }

            return subsamples;
        }
    }
}
