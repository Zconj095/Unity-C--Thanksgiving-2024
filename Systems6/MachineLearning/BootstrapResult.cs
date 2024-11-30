using System;
using System.IO;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Stores the results of a Bootstrap computation.
    /// </summary>
    [Serializable]
    public class BootstrapResult
    {
        /// <summary>
        /// The Bootstrap object used to generate this result.
        /// </summary>
        public Bootstrap Settings { get; private set; }

        /// <summary>
        /// Performance statistics for the training set.
        /// </summary>
        public CrossValidationStatistics Training { get; private set; }

        /// <summary>
        /// Performance statistics for the validation set.
        /// </summary>
        public CrossValidationStatistics Validation { get; private set; }

        /// <summary>
        /// The 0.632 bootstrap estimate.
        /// </summary>
        public double Estimate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the BootstrapResult class.
        /// </summary>
        /// <param name="owner">The Bootstrap object creating this result.</param>
        /// <param name="models">The models created during the bootstrap runs.</param>
        public BootstrapResult(Bootstrap owner, BootstrapValues[] models)
        {
            var trainingValues = new double[models.Length];
            var trainingVariances = new double[models.Length];
            var trainingCounts = new int[models.Length];

            var validationValues = new double[models.Length];
            var validationVariances = new double[models.Length];
            var validationCounts = new int[models.Length];

            for (int i = 0; i < models.Length; i++)
            {
                trainingValues[i] = models[i].TrainingValue;
                trainingVariances[i] = models[i].TrainingVariance;

                validationValues[i] = models[i].ValidationValue;
                validationVariances[i] = models[i].ValidationVariance;

                owner.GetPartitionSize(i, out trainingCounts[i], out validationCounts[i]);
            }

            Settings = owner;
            Training = new CrossValidationStatistics(trainingCounts, trainingValues, trainingVariances);
            Validation = new CrossValidationStatistics(validationCounts, validationValues, validationVariances);

            Estimate = 0.632 * Validation.Mean + 0.368 * Training.Mean;
        }

        /// <summary>
        /// Saves the result to a file in JSON format.
        /// </summary>
        /// <param name="path">The file path where the result should be saved.</param>
        public void Save(string path)
        {
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Loads a BootstrapResult from a file.
        /// </summary>
        /// <param name="path">The path to the file to be loaded.</param>
        /// <returns>The deserialized BootstrapResult.</returns>
        public static BootstrapResult Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"The file at path {path} does not exist.");

            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<BootstrapResult>(json);
        }
    }
}
