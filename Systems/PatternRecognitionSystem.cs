using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatternRecognitionSystem : MonoBehaviour
{
    private class VectorizedPattern
    {
        public float[] Vector { get; set; }
        public List<string> Tags { get; set; }
        public string MetaTag { get; set; }

        public VectorizedPattern(float[] vector, List<string> tags, string metaTag)
        {
            Vector = vector;
            Tags = tags;
            MetaTag = metaTag;
        }
    }

    private List<VectorizedPattern> patterns = new List<VectorizedPattern>();
    private readonly System.Random random = new System.Random();

    // Generate random vectors and tags for patterns
    public void GeneratePatterns(int numPatterns, int vectorSize)
    {
        for (int i = 0; i < numPatterns; i++)
        {
            float[] randomVector = new float[vectorSize];
            for (int j = 0; j < vectorSize; j++)
            {
                randomVector[j] = (float)random.NextDouble();
            }

            List<string> tags = new List<string> { $"Tag-{random.Next(1, 5)}", $"Tag-{random.Next(5, 10)}" };
            string metaTag = $"Meta-{random.Next(1, 3)}";

            patterns.Add(new VectorizedPattern(randomVector, tags, metaTag));
            Debug.Log($"Generated Pattern: {string.Join(", ", randomVector)} | Tags: {string.Join(", ", tags)} | MetaTag: {metaTag}");
        }
    }

    // Perform cross-correlation between two vectors
    private float CrossCorrelate(float[] vectorA, float[] vectorB)
    {
        float dotProduct = 0f;
        float magnitudeA = 0f;
        float magnitudeB = 0f;

        for (int i = 0; i < vectorA.Length; i++)
        {
            dotProduct += vectorA[i] * vectorB[i];
            magnitudeA += vectorA[i] * vectorA[i];
            magnitudeB += vectorB[i] * vectorB[i];
        }

        magnitudeA = Mathf.Sqrt(magnitudeA);
        magnitudeB = Mathf.Sqrt(magnitudeB);

        return dotProduct / (magnitudeA * magnitudeB); // Cosine similarity as correlation
    }

    // Classify a memory from the Memory Retrieval System based on pattern matches
    public string ClassifyMemory(string memoryContent, float correlationThreshold = 0.8f)
    {
        // Convert memory content to a vector representation (e.g., using length or ASCII values)
        float[] memoryVector = memoryContent.Select(c => (float)c).ToArray();

        // Find the best matching pattern based on cross-correlation
        VectorizedPattern bestMatch = null;
        float bestScore = 0f;

        foreach (var pattern in patterns)
        {
            float score = CrossCorrelate(memoryVector, pattern.Vector);
            if (score > bestScore && score >= correlationThreshold)
            {
                bestScore = score;
                bestMatch = pattern;
            }
        }

        if (bestMatch != null)
        {
            Debug.Log($"Memory '{memoryContent}' classified as MetaTag: {bestMatch.MetaTag} | Tags: {string.Join(", ", bestMatch.Tags)}");
            return bestMatch.MetaTag;
        }
        else
        {
            Debug.Log($"Memory '{memoryContent}' could not be classified.");
            return "Unclassified";
        }
    }

    // Cross-validation for patterns
    public void PerformCrossValidation(int numFolds = 5)
    {
        int foldSize = patterns.Count / numFolds;
        float totalAccuracy = 0f;

        for (int fold = 0; fold < numFolds; fold++)
        {
            var testSet = patterns.Skip(fold * foldSize).Take(foldSize).ToList();
            var trainingSet = patterns.Except(testSet).ToList();

            int correct = 0;

            foreach (var testPattern in testSet)
            {
                string predictedMetaTag = ClassifyPattern(testPattern, trainingSet);
                if (predictedMetaTag == testPattern.MetaTag)
                {
                    correct++;
                }
            }

            float accuracy = (float)correct / testSet.Count;
            totalAccuracy += accuracy;
            Debug.Log($"Fold {fold + 1} Accuracy: {accuracy * 100:F2}%");
        }

        Debug.Log($"Average Cross-Validation Accuracy: {(totalAccuracy / numFolds) * 100:F2}%");
    }

    void ConnectToPatternRecognition(string memoryContent)
    {
        PatternRecognitionSystem patternSystem = new PatternRecognitionSystem();
        patternSystem.GeneratePatterns(20, 10); // Ensure patterns are initialized
        string metaTag = patternSystem.ClassifyMemory(memoryContent);
        Debug.Log($"Memory classified as MetaTag: {metaTag}");
    }


    // Helper to classify a pattern using a training set
    private string ClassifyPattern(VectorizedPattern pattern, List<VectorizedPattern> trainingSet)
    {
        VectorizedPattern bestMatch = null;
        float bestScore = 0f;

        foreach (var trainPattern in trainingSet)
        {
            float score = CrossCorrelate(pattern.Vector, trainPattern.Vector);
            if (score > bestScore)
            {
                bestScore = score;
                bestMatch = trainPattern;
            }
        }

        return bestMatch?.MetaTag ?? "Unclassified";
    }

    void Start()
    {
        // Generate random patterns
        GeneratePatterns(20, 10);

        // Simulate a memory from the memory system
        string simulatedMemory = "Learned Unity scripting";
        ClassifyMemory(simulatedMemory);

        // Perform cross-validation on the patterns
        PerformCrossValidation();
    }
}
