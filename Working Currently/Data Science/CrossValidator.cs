using System;
using System.Collections.Generic;
using UnityEngine;
public class CrossValidator : MonoBehaviour
{
    /// <summary>
    /// Performs k-fold cross-validation on a dataset using the provided model training and evaluation function.
    /// </summary>
    /// <param name="dataset">The dataset to perform cross-validation on.</param>
    /// <param name="k">The number of folds.</param>
    /// <param name="modelTrainer">A function that trains a model and returns an accuracy score.</param>
    /// <returns>The average accuracy across all folds.</returns>
    public static float KFoldCrossValidation(Dataset dataset, int k, Func<Dataset, float> modelTrainer)
    {
        if (dataset == null || dataset.DataPoints.Count < k)
        {
            throw new ArgumentException("Dataset is null or does not have enough data points for the specified number of folds.");
        }

        // Split the data into k folds
        List<List<DataPoint>> folds = CreateKFolds(dataset, k);

        float totalAccuracy = 0.0f;

        // Iterate over each fold
        for (int i = 0; i < k; i++)
        {
            // Use the ith fold as the validation set, others as the training set
            List<DataPoint> trainingSet = new List<DataPoint>();
            List<DataPoint> validationSet = folds[i];

            for (int j = 0; j < k; j++)
            {
                if (i != j)
                {
                    trainingSet.AddRange(folds[j]);
                }
            }

            // Train the model and evaluate it on the validation set
            Dataset trainingDataset = new Dataset(trainingSet);
            float accuracy = modelTrainer(trainingDataset);

            totalAccuracy += accuracy;
        }

        // Return the average accuracy across all folds
        return totalAccuracy / k;
    }

    /// <summary>
    /// Splits the dataset into k folds.
    /// </summary>
    /// <param name="dataset">The dataset to split.</param>
    /// <param name="k">The number of folds.</param>
    /// <returns>A list of k folds, each containing a subset of the data points.</returns>
    private static List<List<DataPoint>> CreateKFolds(Dataset dataset, int k)
    {
        List<List<DataPoint>> folds = new List<List<DataPoint>>();
        List<DataPoint> dataPoints = dataset.DataPoints;

        // Shuffle the data points to ensure random distribution in folds
        System.Random rng = new System.Random();
        for (int i = dataPoints.Count - 1; i > 0; i--)
        {
            int swapIndex = rng.Next(i + 1);
            (dataPoints[i], dataPoints[swapIndex]) = (dataPoints[swapIndex], dataPoints[i]);
        }

        // Initialize k empty folds
        for (int i = 0; i < k; i++)
        {
            folds.Add(new List<DataPoint>());
        }

        // Distribute data into folds
        for (int i = 0; i < dataPoints.Count; i++)
        {
            int foldIndex = i % k;
            folds[foldIndex].Add(dataPoints[i]);
        }

        return folds;
    }
}

