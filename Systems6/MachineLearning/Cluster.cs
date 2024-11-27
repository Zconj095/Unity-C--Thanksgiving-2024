using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Base class for a data cluster in Unity.
    /// </summary>
    /// <typeparam name="TCollection">The cluster collection type.</typeparam>
    /// <typeparam name="TData">The data type stored in the cluster.</typeparam>
    /// <typeparam name="TCluster">The cluster type.</typeparam>
    [Serializable]
    public abstract class Cluster<TCollection, TData, TCluster>
        where TCollection : IClusterCollection<TData, TCluster>
        where TCluster : Cluster<TCollection, TData, TCluster>, new()
    {
        private TCollection owner;
        private int index;

        /// <summary>
        ///   Gets the collection to which this cluster belongs.
        /// </summary>
        public TCollection Owner => owner;

        /// <summary>
        ///   Gets the index of this cluster.
        /// </summary>
        public int Index => index;

        /// <summary>
        ///   Gets the proportion of samples contained in this cluster.
        /// </summary>
        public float Proportion => owner.GetProportion(index);

        /// <summary>
        ///   Initializes the cluster with its owner and index.
        /// </summary>
        /// <param name="owner">The owner collection.</param>
        /// <param name="index">The cluster index.</param>
        public void Initialize(TCollection owner, int index)
        {
            this.owner = owner;
            this.index = index;
        }

        /// <summary>
        ///   Represents a collection of clusters.
        /// </summary>
        [Serializable]
        public class ClusterCollection : IClusterCollection<TData, TCluster>
        {
            private readonly TCollection owner;
            private readonly List<float> proportions;
            private readonly List<TCluster> clusters;

            /// <summary>
            ///   Initializes a new instance of the <see cref="ClusterCollection"/> class.
            /// </summary>
            /// <param name="owner">The owning collection.</param>
            /// <param name="clusterCount">The number of clusters.</param>
            public ClusterCollection(TCollection owner, int clusterCount)
            {
                this.owner = owner;
                this.proportions = new List<float>(new float[clusterCount]);
                this.clusters = new List<TCluster>();

                for (int i = 0; i < clusterCount; i++)
                {
                    var cluster = new TCluster();
                    cluster.Initialize(owner, i);
                    clusters.Add(cluster);
                }
            }

            /// <summary>
            ///   Gets the proportion of samples in each cluster.
            /// </summary>
            public List<float> Proportions => proportions;

            /// <summary>
            ///   Gets the list of clusters.
            /// </summary>
            public List<TCluster> Clusters => clusters;

            /// <summary>
            ///   Gets the number of clusters in the collection.
            /// </summary>
            public int Count => clusters.Count;

            /// <summary>
            ///   Gets the cluster at the specified index.
            /// </summary>
            /// <param name="index">The index of the cluster.</param>
            /// <returns>The cluster at the specified index.</returns>
            public TCluster GetCluster(int index) => clusters[index];

            /// <summary>
            ///   Updates the proportion of a specific cluster.
            /// </summary>
            /// <param name="index">The index of the cluster.</param>
            /// <param name="proportion">The new proportion value.</param>
            public void SetProportion(int index, float proportion)
            {
                proportions[index] = proportion;
            }

            /// <summary>
            ///   Retrieves the proportion of a specific cluster.
            /// </summary>
            /// <param name="index">The index of the cluster.</param>
            /// <returns>The proportion of the specified cluster.</returns>
            public float GetProportion(int index)
            {
                return proportions[index];
            }

            /// <summary>
            ///   Gets an enumerator for iterating through the clusters.
            /// </summary>
            /// <returns>An enumerator for the clusters.</returns>
            public IEnumerator<TCluster> GetEnumerator() => clusters.GetEnumerator();

            /// <summary>
            ///   Gets a non-generic enumerator for the clusters.
            /// </summary>
            /// <returns>A non-generic enumerator for the clusters.</returns>
            IEnumerator IEnumerable.GetEnumerator() => clusters.GetEnumerator();
        }
    }

    /// <summary>
    ///   Interface for cluster collections.
    /// </summary>
    /// <typeparam name="TData">The data type stored in clusters.</typeparam>
    /// <typeparam name="TCluster">The cluster type.</typeparam>
    public interface IClusterCollection<TData, TCluster> : IEnumerable<TCluster>
    {
        int Count { get; }
        List<float> Proportions { get; }
        TCluster GetCluster(int index);
        void SetProportion(int index, float proportion);
        float GetProportion(int index); // Added the missing method.
    }
}
