using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Decision strategies for Multi-class Support Vector Machines.
    /// </summary>
    public enum MulticlassComputeMethod
    {
        /// <summary>
        ///   Max-voting method (also known as 1-vs-1 decision).
        /// </summary>
        Voting,

        /// <summary>
        ///   Elimination method (also known as DAG decision).
        /// </summary>
        Elimination,
    }

    /// <summary>
    ///   One-Vs-One construction for solving multi-class classification using a set of binary classifiers.
    /// </summary>
    /// <typeparam name="TBinary">The type for the binary classifier to be used.</typeparam>
    /// <typeparam name="TInput">The type for the classifier inputs. Default is double[].</typeparam>
    [Serializable]
    public class OneVsOne<TBinary, TInput> where TBinary : class, IClassifier<TInput, bool>, ICloneable
    {
        [SerializeField] private List<ClassPair> indices;
        [SerializeField] private List<List<TBinary>> models;
        [SerializeField] private MulticlassComputeMethod method = MulticlassComputeMethod.Elimination;

        /// <summary>
        ///   Gets the pair of class indices handled by each inner binary classification model.
        /// </summary>
        public List<ClassPair> Indices => indices;

        /// <summary>
        ///   Gets the inner binary classification models.
        /// </summary>
        public List<List<TBinary>> Models => models;

        /// <summary>
        ///   Gets or sets the multi-class classification method.
        /// </summary>
        public MulticlassComputeMethod Method
        {
            get => method;
            set => method = value;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="OneVsOne{TBinary, TInput}"/> class.
        /// </summary>
        /// <param name="classes">The number of classes in the multi-class classification problem.</param>
        /// <param name="initializer">A function to create the inner binary classifiers.</param>
        public OneVsOne(int classes, Func<TBinary> initializer)
        {
            Initialize(classes, initializer);
        }

        private void Initialize(int classes, Func<TBinary> initializer)
        {
            if (classes <= 1)
                throw new ArgumentException("Number of classes must be greater than 1.", nameof(classes));

            indices = new List<ClassPair>();
            models = new List<List<TBinary>>();

            for (int i = 0; i < classes - 1; i++)
            {
                models.Add(new List<TBinary>());
                for (int j = 0; j <= i; j++)
                {
                    indices.Add(new ClassPair(i + 1, j));
                    models[i].Add(initializer());
                }
            }
        }

        /// <summary>
        ///   Computes a class-label decision for a given input.
        /// </summary>
        /// <param name="input">The input vector to classify.</param>
        /// <returns>A class-label that best describes the input.</returns>
        public int Decide(TInput input)
        {
            return method == MulticlassComputeMethod.Voting
                ? DecideByVoting(input)
                : DecideByElimination(input);
        }

        private int DecideByVoting(TInput input)
        {
            var votes = new int[indices.Count];

            foreach (var pair in indices)
            {
                int i = pair.Class1;
                int j = pair.Class2;
                var model = Models[i - 1][j];

                if (model.Decide(input))
                    votes[i]++;
                else
                    votes[j]++;
            }

            return Array.IndexOf(votes, Mathf.Max(votes));
        }

        private int DecideByElimination(TInput input)
        {
            int i = Models.Count;
            int j = 0;

            while (i != j)
            {
                var model = Models[i - 1][j];
                if (model.Decide(input))
                    j++;
                else
                    i--;
            }

            return i;
        }

        /// <summary>
        ///   Returns a binary classification model for a specific pair of classes.
        /// </summary>
        public TBinary GetClassifierForClassPair(int classA, int classB)
        {
            if (classA == classB)
                throw new ArgumentException("Classes must be different.");

            return classA > classB ? Models[classA - 1][classB] : Models[classB - 1][classA];
        }
    }

    /// <summary>
    ///   Simplified One-Vs-One for <see cref="double[]"/> inputs.
    /// </summary>
    public class OneVsOne<TBinary> : OneVsOne<TBinary, double[]>
        where TBinary : class, IClassifier<double[], bool>, ICloneable
    {
        public OneVsOne(int classes, Func<TBinary> initializer) : base(classes, initializer) { }
    }
    /// <summary>
    ///   Generic interface for classifiers.
    /// </summary>
    public interface IClassifier<TInput, TOutput>
    {
        TOutput Decide(TInput input);
    }
}
