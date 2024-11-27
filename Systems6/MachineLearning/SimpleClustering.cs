using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleClustering
{
    private List<Vector3> centroids;

    public SimpleClustering()
    {
        centroids = new List<Vector3>();
    }

    public EdgeLoreMachineLearning.IClusterCollection<Vector3, Vector3> Clusters
    {
        get { return new SimpleClusterCollection(centroids); }
    }

    public int[] Compute(Vector3[] points)
    {
        centroids.AddRange(points);
        int[] labels = new int[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            labels[i] = i; // Example logic
        }
        return labels;
    }

    private class SimpleClusterCollection : EdgeLoreMachineLearning.IClusterCollection<Vector3, Vector3>
    {
        private readonly List<Vector3> centroids;
        private readonly List<float> proportions; // Use List<float> as required by the interface

        public SimpleClusterCollection(List<Vector3> centroids)
        {
            this.centroids = centroids;
            proportions = new List<float>(new float[centroids.Count]); // Default proportions
        }

        public int Count => centroids.Count;

        public int Classify(Vector3 dataPoint)
        {
            return 0; // Placeholder classification
        }

        public Vector3 GetCluster(int index)
        {
            if (index < 0 || index >= centroids.Count)
                throw new System.IndexOutOfRangeException("Invalid cluster index.");
            return centroids[index];
        }

        public float GetProportion(int clusterIndex)
        {
            if (clusterIndex < 0 || clusterIndex >= proportions.Count)
                throw new System.IndexOutOfRangeException("Invalid cluster index.");
            return proportions[clusterIndex];
        }

        public void SetProportion(int clusterIndex, float proportion)
        {
            if (clusterIndex < 0 || clusterIndex >= proportions.Count)
                throw new System.IndexOutOfRangeException("Invalid cluster index.");
            proportions[clusterIndex] = proportion;
        }

        public List<float> Proportions => proportions; // Return type now matches the interface

        public IEnumerator<Vector3> GetEnumerator()
        {
            return centroids.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
