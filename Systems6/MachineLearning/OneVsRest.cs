using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Base class for multi-class classifiers based on the "one-vs-rest" construction using binary classifiers.
    /// </summary>
    /// <typeparam name="TModel">The type for the inner binary classifiers.</typeparam>
    /// <typeparam name="TInput">The input type handled by the classifiers. Default is double[].</typeparam>
    [Serializable]
    public class OneVsRest<TModel, TInput> where TModel : IClassifier<TInput, bool>
    {
        [SerializeField] private List<TModel> models;

        /// <summary>
        ///   Initializes a new instance of the <see cref="OneVsRest{TModel, TInput}"/> class.
        /// </summary>
        /// <param name="classes">The number of classes in the multi-class classification problem.</param>
        /// <param name="initializer">A function to create the inner binary classifiers.</param>
        public OneVsRest(int classes, Func<TModel> initializer)
        {
            Initialize(classes, initializer);
        }

        private void Initialize(int classes, Func<TModel> initializer)
        {
            if (classes <= 1)
                throw new ArgumentException("Number of classes must be greater than 1.", nameof(classes));

            models = new List<TModel>();
            for (int i = 0; i < classes; i++)
            {
                models.Add(initializer());
            }
        }

        /// <summary>
        ///   Gets the binary classifier for a particular class index.
        /// </summary>
        /// <param name="classIndex">The index of the class.</param>
        public TModel GetClassifierForClass(int classIndex)
        {
            return models[classIndex];
        }

        /// <summary>
        ///   Gets or sets the inner binary classifiers.
        /// </summary>
        public List<TModel> Models => models;

        /// <summary>
        ///   Computes whether a class label applies to an input vector.
        /// </summary>
        /// <param name="input">The input vector to classify.</param>
        /// <param name="classIndex">The class label index to test.</param>
        /// <returns>True if the input belongs to the given class; otherwise, false.</returns>
        public bool Decide(TInput input, int classIndex)
        {
            return models[classIndex].Decide(input);
        }

        /// <summary>
        ///   Computes a numerical score for the association between an input vector and a given class.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <param name="classIndex">The index of the class to compute the score for.</param>
        /// <returns>The computed score.</returns>
        public double Score(TInput input, int classIndex)
        {
            if (models[classIndex] is IBinaryScoreClassifier<TInput> scoreModel)
            {
                bool decision;
                return scoreModel.Score(input, out decision);
            }
            throw new InvalidOperationException("The model does not support scoring.");
        }

        /// <summary>
        ///   Returns an enumerator to iterate through all binary classifiers.
        /// </summary>
        public IEnumerator<KeyValuePair<int, TModel>> GetEnumerator()
        {
            for (int i = 0; i < models.Count; i++)
            {
                yield return new KeyValuePair<int, TModel>(i, models[i]);
            }
        }
    }

    /// <summary>
    ///   Simplified version of `OneVsRest` for double[] inputs.
    /// </summary>
    /// <typeparam name="TModel">The type for the inner binary classifiers.</typeparam>
    public class OneVsRest<TModel> : OneVsRest<TModel, double[]> where TModel : IClassifier<double[], bool>
    {
        public OneVsRest(int classes, Func<TModel> initializer) : base(classes, initializer) { }
    }
    
    /// <summary>
    ///   Interface for classifiers that can provide numerical scores.
    /// </summary>
    public interface IBinaryScoreClassifier<TInput> : IClassifier<TInput, bool>
    {
        double Score(TInput input, out bool decision);
    }
}
