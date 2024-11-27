using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Common interface for collections of clusters (i.e., KMeans, MeanShift, etc.).
    /// </summary>
    public interface IClusterCollectionEx<TData, TCluster> : IEnumerable<TCluster>
    {
        /// <summary>
        ///   Gets the number of clusters in the collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        ///   Gets the collection of clusters currently modeled by the clustering algorithm.
        /// </summary>
        TCluster[] Clusters { get; }

        /// <summary>
        /// Gets the proportion of samples in each cluster.
        /// </summary>
        double[] Proportions { get; }

        /// <summary>
        ///   Gets the cluster at the given index.
        /// </summary>
        /// <param name="index">The index of the cluster. This should also be the class label of the cluster.</param>
        /// <returns>An object holding information about the selected cluster.</returns>
        TCluster this[int index] { get; }
    }

    /// <summary>
    ///   Common interface for clusters that contain centroids, where the centroid data type
    ///   might differ from the data type of the clustered data.
    /// </summary>
    public interface ICentroidClusterCollection<TData, TCentroids, TCluster> : IClusterCollectionEx<TData, TCluster>
    {
        /// <summary>
        ///   Gets or sets the clusters' centroids.
        /// </summary>
        TCentroids[] Centroids { get; set; }

        /// <summary>
        ///   Gets or sets the distance function used to measure the distance
        ///   between a point and the cluster centroid in this clustering definition.
        /// </summary>
        Func<TData, TCentroids, float> Distance { get; set; }

        /// <summary>
        ///   Calculates the average square distance from the data points 
        ///   to the nearest clusters' centroids.
        /// </summary>
        /// <param name="data">The data points to calculate distortion.</param>
        /// <param name="labels">Optional cluster labels for data points.</param>
        /// <param name="weights">Optional weights for each data point.</param>
        /// <returns>The average square distance from data points to nearest centroids.</returns>
        float Distortion(TData[] data, int[] labels = null, float[] weights = null);

        /// <summary>
        ///   Transform data points into feature vectors containing the 
        ///   distance between each point and each of the clusters.
        /// </summary>
        /// <param name="points">The input points.</param>
        /// <param name="weights">Optional weights for each point.</param>
        /// <param name="result">An optional matrix to store the computed transformation.</param>
        /// <returns>A matrix containing the distances from each point to each cluster centroid.</returns>
        float[][] Transform(TData[] points, float[] weights = null, float[][] result = null);

        /// <summary>
        ///   Transform data points into feature vectors containing the 
        ///   distance between each point and each of the clusters.
        /// </summary>
        /// <param name="points">The input points.</param>
        /// <param name="labels">Optional labels for each input point.</param>
        /// <param name="weights">Optional weights for each point.</param>
        /// <param name="result">An optional array to store the computed transformation.</param>
        /// <returns>An array containing the distances from each point to each cluster centroid.</returns>
        float[] Transform(TData[] points, int[] labels, float[] weights = null, float[] result = null);
    }

    /// <summary>
    ///   Common interface for clusters with centroids of the same data type as the clustered data.
    /// </summary>
    public interface ICentroidClusterCollection<TData, TCluster> : ICentroidClusterCollection<TData, TData, TCluster>
    {
    }
}
