using System.Collections.Generic;
using UnityEngine;

public class EvolutionaryOptimizer
{
    private int populationSize;
    private float mutationRate;

    public EvolutionaryOptimizer(int populationSize, float mutationRate)
    {
        this.populationSize = populationSize;
        this.mutationRate = mutationRate;
    }

    public List<float[,]> InitializePopulation(int size)
    {
        List<float[,]> population = new List<float[,]>();
        for (int i = 0; i < populationSize; i++)
        {
            float[,] weights = new float[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    weights[x, y] = Random.Range(-1f, 1f);
                }
            }
            population.Add(weights);
        }
        return population;
    }

    public float[,] Mutate(float[,] weights)
    {
        int size = weights.GetLength(0);
        float[,] mutated = (float[,])weights.Clone();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (Random.value < mutationRate)
                {
                    mutated[i, j] += Random.Range(-0.1f, 0.1f);
                }
            }
        }

        return mutated;
    }

    public float[,] Crossover(float[,] parent1, float[,] parent2)
    {
        int size = parent1.GetLength(0);
        float[,] child = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                child[i, j] = Random.value > 0.5f ? parent1[i, j] : parent2[i, j];
            }
        }
        return child;
    }

    public List<float[,]> Evolve(List<float[,]> population, List<float> fitnessScores)
    {
        List<float[,]> nextGeneration = new List<float[,]>();

        // Select the top performers
        List<int> topIndices = GetTopPerformers(fitnessScores, populationSize / 2);

        foreach (int index in topIndices)
        {
            nextGeneration.Add(population[index]);
        }

        // Generate new offspring through mutation and crossover
        while (nextGeneration.Count < populationSize)
        {
            int parent1Index = topIndices[Random.Range(0, topIndices.Count)];
            int parent2Index = topIndices[Random.Range(0, topIndices.Count)];

            float[,] child = Crossover(population[parent1Index], population[parent2Index]);
            child = Mutate(child);
            nextGeneration.Add(child);
        }

        return nextGeneration;
    }

    private List<int> GetTopPerformers(List<float> scores, int count)
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < scores.Count; i++)
        {
            indices.Add(i);
        }
        indices.Sort((a, b) => scores[b].CompareTo(scores[a]));
        return indices.GetRange(0, count);
    }
}
