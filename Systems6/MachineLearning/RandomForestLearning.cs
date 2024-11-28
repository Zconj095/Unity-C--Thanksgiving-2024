using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class RandomForestLearning
    {
        private RandomForest forest;
        private List<DecisionVariable> attributes;

        public int NumberOfTrees { get; set; } = 100;
        public double SampleRatio { get; set; } = 0.632;
        public double CoverageRatio { get; set; } = 1.0;
        public int Join { get; set; } = 100;

        public RandomForestLearning() { }

        public RandomForestLearning(List<DecisionVariable> attributes)
        {
            this.attributes = attributes;
        }

        public RandomForest Learn(int[][] inputs, int[] outputs)
        {
            if (attributes == null)
                attributes = GenerateAttributesFromData(inputs);

            forest = new RandomForest(NumberOfTrees, attributes, outputs.Max() + 1);
            TrainForest(inputs, outputs);
            return forest;
        }

        public RandomForest Learn(double[][] inputs, int[] outputs)
        {
            if (attributes == null)
                attributes = GenerateAttributesFromData(inputs);

            forest = new RandomForest(NumberOfTrees, attributes, outputs.Max() + 1);
            TrainForest(inputs, outputs);
            return forest;
        }

        private void TrainForest<T>(T[][] inputs, int[] outputs)
        {
            var trees = forest.Trees.ToList(); // Convert to a list for indexing

            for (int i = 0; i < NumberOfTrees; i++)
            {
                var tree = trees[i];
                TrainTree(inputs, outputs, tree);
            }
        }

        private void TrainTree<T>(T[][] inputs, int[] outputs, DecisionTree tree)
        {
            int[] sampleIndices = GenerateSampleIndices(outputs.Length);
            var sampledInputs = sampleIndices.Select(idx => inputs[idx]).ToArray();
            var sampledOutputs = sampleIndices.Select(idx => outputs[idx]).ToArray();

            var teacher = new DecisionTreeLearning(tree)
            {
                MaxVariables = GetVariablesPerTree(inputs[0].Length),
                Join = this.Join
            };

            teacher.Learn(sampledInputs, sampledOutputs);
        }

        private int GetVariablesPerTree(int totalVariables)
        {
            return CoverageRatio <= 0
                ? (int)Math.Sqrt(totalVariables)
                : (int)(totalVariables * CoverageRatio);
        }

        private int[] GenerateSampleIndices(int dataSize)
        {
            int sampleSize = (int)(dataSize * SampleRatio);
            return Enumerable.Range(0, dataSize)
                             .OrderBy(_ => UnityEngine.Random.value)
                             .Take(sampleSize)
                             .ToArray();
        }

        private List<DecisionVariable> GenerateAttributesFromData<T>(T[][] inputs)
        {
            int columnCount = inputs[0].Length;
            var variables = new List<DecisionVariable>();

            for (int i = 0; i < columnCount; i++)
            {
                variables.Add(new DecisionVariable(
                    $"Feature_{i}",
                    new DoubleRange2(0, 1) // Assuming default range for numeric data
                ));
            }

            return variables;
        }
    }
}
