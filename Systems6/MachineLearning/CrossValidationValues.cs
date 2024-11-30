using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Stores the results for a single model during cross-validation.
    /// </summary>
    [Serializable]
    public class CrossValidationValues<TModel> where TModel : class
    {
        public TModel Model { get; private set; }
        public double TrainingValue { get; private set; }
        public double ValidationValue { get; private set; }
        public double TrainingVariance { get; private set; }
        public double ValidationVariance { get; private set; }

        /// <summary>
        /// Creates a new instance of CrossValidationValues.
        /// </summary>
        /// <param name="trainingValue">The training value for the model.</param>
        /// <param name="validationValue">The validation value for the model.</param>
        public CrossValidationValues(double trainingValue, double validationValue)
        {
            TrainingValue = trainingValue;
            ValidationValue = validationValue;
        }

        /// <summary>
        /// Creates a new instance of CrossValidationValues.
        /// </summary>
        /// <param name="trainingValue">The training value for the model.</param>
        /// <param name="trainingVariance">The variance of the training values.</param>
        /// <param name="validationValue">The validation value for the model.</param>
        /// <param name="validationVariance">The variance of the validation values.</param>
        public CrossValidationValues(double trainingValue, double trainingVariance, double validationValue, double validationVariance)
        {
            TrainingValue = trainingValue;
            TrainingVariance = trainingVariance;
            ValidationValue = validationValue;
            ValidationVariance = validationVariance;
        }

        /// <summary>
        /// Creates a new instance of CrossValidationValues with a model.
        /// </summary>
        /// <param name="model">The fitted model.</param>
        /// <param name="trainingValue">The training value for the model.</param>
        /// <param name="validationValue">The validation value for the model.</param>
        public CrossValidationValues(TModel model, double trainingValue, double validationValue)
        {
            Model = model;
            TrainingValue = trainingValue;
            ValidationValue = validationValue;
        }

        /// <summary>
        /// Creates a new instance of CrossValidationValues with a model.
        /// </summary>
        /// <param name="model">The fitted model.</param>
        /// <param name="trainingValue">The training value for the model.</param>
        /// <param name="trainingVariance">The variance of the training values.</param>
        /// <param name="validationValue">The validation value for the model.</param>
        /// <param name="validationVariance">The variance of the validation values.</param>
        public CrossValidationValues(TModel model, double trainingValue, double trainingVariance, double validationValue, double validationVariance)
        {
            Model = model;
            TrainingValue = trainingValue;
            TrainingVariance = trainingVariance;
            ValidationValue = validationValue;
            ValidationVariance = validationVariance;
        }

        /// <summary>
        /// Factory method to create a new instance of CrossValidationValues.
        /// </summary>
        /// <param name="model">The fitted model.</param>
        /// <param name="trainingValue">The training value for the model.</param>
        /// <param name="trainingVariance">The variance of the training values.</param>
        /// <param name="validationValue">The validation value for the model.</param>
        /// <param name="validationVariance">The variance of the validation values.</param>
        public static CrossValidationValues<TModel> Create(TModel model, double trainingValue, double trainingVariance, double validationValue, double validationVariance)
        {
            return new CrossValidationValues<TModel>(model, trainingValue, trainingVariance, validationValue, validationVariance);
        }

        /// <summary>
        /// Factory method to create a new instance of CrossValidationValues.
        /// </summary>
        /// <param name="model">The fitted model.</param>
        /// <param name="trainingValue">The training value for the model.</param>
        /// <param name="trainingVariance">The variance of the training values.</param>
        public static CrossValidationValues<TModel> Create(TModel model, double trainingValue, double trainingVariance)
        {
            return new CrossValidationValues<TModel>(model, trainingValue, trainingVariance, 0, 0);
        }
    }
}
