using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class NaiveBayes
    {
        private int[] symbols;
        private double[] priors;
        private double[,][] distributions;

        public NaiveBayes(int classes, params int[] symbols)
        {
            if (classes < 2)
                throw new ArgumentOutOfRangeException(nameof(classes), "Number of classes must be at least 2.");

            if (symbols == null || symbols.Length == 0)
                throw new ArgumentNullException(nameof(symbols), "Symbols array cannot be null or empty.");

            this.symbols = symbols;
            this.priors = Enumerable.Repeat(1.0 / classes, classes).ToArray();
            this.distributions = new double[classes, symbols.Length][];
            
            // Initialize distributions
            for (int i = 0; i < classes; i++)
            {
                for (int j = 0; j < symbols.Length; j++)
                {
                    distributions[i, j] = new double[Math.Max(symbols[j], 2)];
                }
            }
        }

        public int[] NumberOfSymbols => symbols;

        public double[] Priors
        {
            get => priors;
            set
            {
                if (value == null || value.Length != priors.Length)
                    throw new ArgumentException("Priors array size mismatch.");
                priors = value;
            }
        }

        public double[,][] Distributions => distributions;

        public static NaiveBayes CreateNormal(int classes, int inputs)
        {
            return new NaiveBayes(classes, Enumerable.Repeat(2, inputs).ToArray());
        }

        public static NaiveBayes CreateNormal(int classes, int inputs, double[] classPriors)
        {
            var naiveBayes = new NaiveBayes(classes, Enumerable.Repeat(2, inputs).ToArray())
            {
                Priors = classPriors
            };
            return naiveBayes;
        }

        public double ComputeLikelihood(int[] input, int classIndex)
        {
            if (classIndex < 0 || classIndex >= priors.Length)
                throw new ArgumentOutOfRangeException(nameof(classIndex), "Invalid class index.");

            double logLikelihood = Math.Log(priors[classIndex]);
            for (int i = 0; i < input.Length; i++)
            {
                int symbol = input[i];
                if (symbol < 0 || symbol >= distributions[classIndex, i].Length)
                    throw new ArgumentOutOfRangeException($"Symbol {symbol} is out of range for input variable {i}.");

                logLikelihood += Math.Log(distributions[classIndex, i][symbol] + 1e-9); // Adding a small value to prevent log(0)
            }
            return logLikelihood;
        }

        public int Predict(int[] input)
        {
            double maxLikelihood = double.NegativeInfinity;
            int predictedClass = -1;

            for (int i = 0; i < priors.Length; i++)
            {
                double likelihood = ComputeLikelihood(input, i);
                if (likelihood > maxLikelihood)
                {
                    maxLikelihood = likelihood;
                    predictedClass = i;
                }
            }
            return predictedClass;
        }

        public void Train(int[][] inputs, int[] outputs)
        {
            if (inputs.Length != outputs.Length)
                throw new ArgumentException("Inputs and outputs must have the same length.");

            int classes = priors.Length;
            int inputsLength = inputs[0].Length;

            // Initialize frequency counts
            double[,][] frequencies = new double[classes, inputsLength][];
            for (int i = 0; i < classes; i++)
            {
                for (int j = 0; j < inputsLength; j++)
                {
                    frequencies[i, j] = new double[symbols[j]];
                }
            }

            // Count frequencies
            for (int i = 0; i < inputs.Length; i++)
            {
                int outputClass = outputs[i];
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    frequencies[outputClass, j][inputs[i][j]]++;
                }
            }

            // Transform frequencies into probabilities
            for (int i = 0; i < classes; i++)
            {
                for (int j = 0; j < inputsLength; j++)
                {
                    double total = frequencies[i, j].Sum();
                    for (int k = 0; k < frequencies[i, j].Length; k++)
                    {
                        distributions[i, j][k] = frequencies[i, j][k] / total;
                    }
                }
            }

            // Update priors
            double totalSamples = outputs.Length;
            for (int i = 0; i < classes; i++)
            {
                priors[i] = outputs.Count(y => y == i) / totalSamples;
            }
        }
    }
}
