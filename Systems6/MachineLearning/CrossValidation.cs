using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Unity-compatible k-Fold Cross-Validation using Reflection.
    /// </summary>
    public class CrossValidation
    {
        private readonly int _folds;
        private readonly List<int>[] _partitions;

        public CrossValidation(int size, int folds)
        {
            if (size < folds)
                throw new ArgumentException("Number of folds cannot exceed dataset size.");

            _folds = folds;
            _partitions = CreatePartitions(size, folds);
        }

        private List<int>[] CreatePartitions(int size, int folds)
        {
            var indices = Enumerable.Range(0, size).ToArray();
            var shuffled = indices.OrderBy(x => Guid.NewGuid()).ToArray(); // Unity equivalent of random shuffle
            var partitions = new List<int>[folds];

            for (int i = 0; i < folds; i++)
                partitions[i] = new List<int>();

            for (int i = 0; i < shuffled.Length; i++)
                partitions[i % folds].Add(shuffled[i]);

            return partitions;
        }

        public void PerformCrossValidation<TModel, TInput, TOutput>(object learnerInstance, TInput[] inputs, TOutput[] outputs, MethodInfo learnMethod, MethodInfo evaluateMethod)
        {
            for (int i = 0; i < _folds; i++)
            {
                var trainingSet = new List<int>();
                var validationSet = _partitions[i];

                for (int j = 0; j < _folds; j++)
                {
                    if (j != i)
                        trainingSet.AddRange(_partitions[j]);
                }

                var trainingInputs = trainingSet.Select(index => inputs[index]).ToArray();
                var trainingOutputs = trainingSet.Select(index => outputs[index]).ToArray();
                var validationInputs = validationSet.Select(index => inputs[index]).ToArray();
                var validationOutputs = validationSet.Select(index => outputs[index]).ToArray();

                var model = learnMethod.Invoke(learnerInstance, new object[] { trainingInputs, trainingOutputs });
                var validationResult = evaluateMethod.Invoke(model, new object[] { validationInputs, validationOutputs });

                UnityEngine.Debug.Log($"Fold {i + 1}: Validation Result - {validationResult}");
            }
        }
    }
}
