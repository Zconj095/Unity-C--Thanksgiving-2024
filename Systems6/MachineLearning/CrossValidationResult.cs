using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Stores results of k-Fold Cross-Validation for a model.
    /// </summary>
    [Serializable]
    public class CrossValidationResult<TModel> where TModel : class
    {
        public CrossValidation Settings { get; private set; } // Uses CrossValidation class
        public CrossValidationStatistics Training { get; private set; }
        public CrossValidationStatistics Validation { get; private set; }
        public List<TModel> Models { get; private set; }
        public object Tag { get; set; }

        public CrossValidationResult(CrossValidation owner, List<CrossValidationFoldResult<TModel>> models)
        {
            var trainingValues = new List<double>();
            var trainingVariances = new List<double>();
            var trainingCounts = new List<int>();

            var validationValues = new List<double>();
            var validationVariances = new List<double>();
            var validationCounts = new List<int>();

            foreach (var model in models)
            {
                trainingValues.Add(model.TrainingValue);
                trainingVariances.Add(model.TrainingVariance);
                trainingCounts.Add(model.TrainingCount);

                validationValues.Add(model.ValidationValue);
                validationVariances.Add(model.ValidationVariance);
                validationCounts.Add(model.ValidationCount);
            }

            this.Settings = owner;
            this.Models = models.ConvertAll(m => m.Model);
            this.Training = new CrossValidationStatistics(trainingCounts.ToArray(), trainingValues.ToArray(), trainingVariances.ToArray());
            this.Validation = new CrossValidationStatistics(validationCounts.ToArray(), validationValues.ToArray(), validationVariances.ToArray());
        }

        public void Save(string path)
        {
            string json = JsonUtility.ToJson(this);
            File.WriteAllText(path, json);
        }

        public static CrossValidationResult<TModel> Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found at the specified path.", path);

            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<CrossValidationResult<TModel>>(json);
        }
    }


    /// <summary>
    /// Represents the result of a single fold in Cross-Validation.
    /// </summary>
    [Serializable]
    public class CrossValidationFoldResult<TModel> where TModel : class
    {
        public TModel Model { get; private set; }
        public double TrainingValue { get; private set; }
        public double TrainingVariance { get; private set; }
        public int TrainingCount { get; private set; }

        public double ValidationValue { get; private set; }
        public double ValidationVariance { get; private set; }
        public int ValidationCount { get; private set; }

        public CrossValidationFoldResult(TModel model, double trainingValue, double trainingVariance, int trainingCount, double validationValue, double validationVariance, int validationCount)
        {
            Model = model;
            TrainingValue = trainingValue;
            TrainingVariance = trainingVariance;
            TrainingCount = trainingCount;
            ValidationValue = validationValue;
            ValidationVariance = validationVariance;
            ValidationCount = validationCount;
        }
    }
}
