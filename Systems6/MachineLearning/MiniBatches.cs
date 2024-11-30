using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public enum ShuffleMethod
    {
        None,
        OnlyOnce,
        EveryEpoch
    }

    public static class MiniBatches
    {
        public static Batches<TInput, TOutput> Create<TInput, TOutput>(
            TInput[] input, TOutput[] output, double[] weights = null,
            int batchSize = 32, int maxIterations = 0, int maxEpochs = 0,
            ShuffleMethod shuffle = ShuffleMethod.EveryEpoch)
        {
            return new Batches<TInput, TOutput>(input, output, weights)
            {
                MiniBatchSize = batchSize,
                MaxIterations = maxIterations,
                MaxEpochs = maxEpochs,
                Shuffle = shuffle
            };
        }
    }

    public abstract class BaseBatches<TBatch, TInput> : IEnumerable<TBatch>
        where TBatch : MiniBatchesDataSubset<TInput>
    {
        protected TInput[] Inputs { get; set; }
        protected double[] Weights { get; set; }
        protected int[] ShuffledIndices { get; private set; }

        public int MiniBatchSize { get; set; } = 32;
        public ShuffleMethod Shuffle { get; set; } = ShuffleMethod.OnlyOnce;

        public int MaxIterations { get; set; } = 0; // Added property
        public int MaxEpochs { get; set; } = 0;     // Added property

        protected BaseBatches(TInput[] inputs, double[] weights = null)
        {
            Inputs = inputs;
            Weights = weights ?? new double[inputs.Length];
            for (int i = 0; i < Weights.Length; i++) Weights[i] = 1.0; // Default weights
        }

        protected abstract TBatch InitBatch();

        public IEnumerator<TBatch> GetEnumerator()
        {
            TBatch batch = InitBatch();
            ShuffledIndices = Shuffle == ShuffleMethod.None
                ? GenerateRange(Inputs.Length)
                : ShuffleArray(Inputs.Length);

            int currentSample = 0;
            int iteration = 0;

            while (true)
            {
                for (int i = 0; i < batch.Inputs.Length; i++)
                {
                    if (currentSample >= Inputs.Length)
                    {
                        if (Shuffle == ShuffleMethod.EveryEpoch)
                        {
                            ShuffledIndices = ShuffleArray(Inputs.Length);
                        }
                        currentSample = 0;
                    }

                    int idx = ShuffledIndices[currentSample];
                    batch.Inputs[i] = Inputs[idx];
                    batch.Weights[i] = Weights[idx];
                    currentSample++;
                }

                yield return batch;

                iteration++;
                if ((MaxIterations > 0 && iteration >= MaxIterations) || iteration >= MaxEpochs * (Inputs.Length / MiniBatchSize))
                    yield break;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int[] GenerateRange(int length)
        {
            int[] range = new int[length];
            for (int i = 0; i < length; i++) range[i] = i;
            return range;
        }

        private int[] ShuffleArray(int length)
        {
            int[] array = GenerateRange(length);
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
            return array;
        }
    }

    public class Batches<TInput, TOutput> : BaseBatches<MiniBatchesDataSubset<TInput, TOutput>, TInput>
    {
        private readonly TOutput[] Outputs;

        public Batches(TInput[] inputs, TOutput[] outputs, double[] weights = null)
            : base(inputs, weights)
        {
            Outputs = outputs ?? throw new ArgumentNullException(nameof(outputs));
            if (inputs.Length != outputs.Length)
                throw new ArgumentException("Inputs and outputs must have the same length.");
        }

        protected override MiniBatchesDataSubset<TInput, TOutput> InitBatch()
        {
            return new MiniBatchesDataSubset<TInput, TOutput>(MiniBatchSize);
        }
    }

    public class MiniBatchesDataSubset<TInput>
    {
        public TInput[] Inputs { get; }
        public double[] Weights { get; }

        public MiniBatchesDataSubset(int batchSize)
        {
            Inputs = new TInput[batchSize];
            Weights = new double[batchSize];
        }
    }

    public class MiniBatchesDataSubset<TInput, TOutput> : MiniBatchesDataSubset<TInput>
    {
        public TOutput[] Outputs { get; }

        public MiniBatchesDataSubset(int batchSize) : base(batchSize)
        {
            Outputs = new TOutput[batchSize];
        }
    }
}
