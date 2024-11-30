using System;
using System.Collections.Generic;
using System.Linq; // Include LINQ for Max()
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Term Frequency - Inverse Document Frequency (TF-IDF) implementation for Unity.
    /// </summary>
    public class TFIDF
    {
        private BagOfWords bow;
        private int[] counts;
        private int numberOfDocuments;

        private TermFrequency tf = TermFrequency.Default;
        private InverseDocumentFrequency idf = InverseDocumentFrequency.Default;

        private double[] inverseDocumentFrequency;

        public int[] Counts => counts;
        public int NumberOfDocuments => numberOfDocuments;
        public int NumberOfWords => bow.NumberOfWords;
        public double[] IDFValues => inverseDocumentFrequency; // Renamed property
        public bool UpdateDictionary { get; set; }

        public TermFrequency TermFrequencyType // Renamed property
        {
            get => tf;
            set => tf = value;
        }

        public InverseDocumentFrequency InverseDocumentFrequencyType // Renamed property
        {
            get => idf;
            set => idf = value;
        }

        public TFIDF()
        {
            bow = new BagOfWords { MaximumOccurrence = int.MaxValue };
            UpdateDictionary = true;
        }

        public TFIDF(string[][] texts) : this()
        {
            Learn(texts);
            UpdateDictionary = false;
        }

        public void Learn(string[][] inputs)
        {
            if (UpdateDictionary)
                bow.Learn(inputs);

            counts = new int[NumberOfWords];
            numberOfDocuments = inputs.Length;

            for (int i = 0; i < inputs.Length; i++)
            {
                var seenWords = new HashSet<int>();
                foreach (var word in inputs[i])
                {
                    if (bow.StringToCode.TryGetValue(word, out int index))
                        seenWords.Add(index);
                }

                foreach (var index in seenWords)
                    counts[index]++;
            }

            CalculateIDF(inputs.Length);
        }

        private void CalculateIDF(int totalDocuments)
        {
            inverseDocumentFrequency = new double[NumberOfWords];

            var maxCount = counts.Max(); // Calculate once for efficiency

            for (int i = 0; i < counts.Length; i++)
            {
                switch (idf)
                {
                    case InverseDocumentFrequency.Default:
                        inverseDocumentFrequency[i] = counts[i] > 0
                            ? Math.Log(totalDocuments / (double)counts[i])
                            : 0;
                        break;
                    case InverseDocumentFrequency.Smooth:
                        inverseDocumentFrequency[i] = Math.Log(totalDocuments / (1.0 + counts[i]));
                        break;
                    case InverseDocumentFrequency.Max:
                        inverseDocumentFrequency[i] = Math.Log(maxCount / (1.0 + counts[i]));
                        break;
                    case InverseDocumentFrequency.Probabilistic:
                        inverseDocumentFrequency[i] = counts[i] > 0
                            ? Math.Log((totalDocuments - counts[i]) / (double)counts[i])
                            : 0;
                        break;
                    default:
                        inverseDocumentFrequency[i] = 1;
                        break;
                }
            }
        }

        public double[] Transform(string[] input)
        {
            var result = new double[NumberOfWords];
            foreach (var word in input)
            {
                if (bow.StringToCode.TryGetValue(word, out int index))
                {
                    switch (tf)
                    {
                        case TermFrequency.Binary:
                            result[index] = 1;
                            break;
                        case TermFrequency.Default:
                            result[index]++;
                            break;
                        case TermFrequency.Log:
                            result[index] = 1 + Math.Log(result[index]);
                            break;
                        case TermFrequency.DoubleNormalization:
                            result[index]++;
                            break;
                    }
                }
            }

            if (tf == TermFrequency.DoubleNormalization)
            {
                var max = result.Max();
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = 0.5 + 0.5 * (result[i] / max);
                }
            }

            for (int i = 0; i < result.Length; i++)
                result[i] *= inverseDocumentFrequency[i];

            return result;
        }

        public double[][] Transform(string[][] inputs)
        {
            var results = new double[inputs.Length][];
            for (int i = 0; i < inputs.Length; i++)
            {
                results[i] = Transform(inputs[i]);
            }

            return results;
        }
    }

    /// <summary>
    /// Term Frequency variants.
    /// </summary>
    public enum TermFrequency
    {
        Binary,
        Default,
        Log,
        DoubleNormalization
    }

    /// <summary>
    /// Inverse Document Frequency variants.
    /// </summary>
    public enum InverseDocumentFrequency
    {
        Unary,
        Default,
        Smooth,
        Max,
        Probabilistic
    }

    // Assume that BagOfWords class and other dependencies are defined elsewhere
}
