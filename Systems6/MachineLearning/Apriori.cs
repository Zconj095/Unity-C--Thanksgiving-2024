using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning.Rules
{
    /// <summary>
    /// A-priori algorithm for association rule mining in Unity.
    /// </summary>
    public class Apriori<T>
    {
        private int supportMin;
        private double confidence;
        private Dictionary<HashSet<T>, int> frequentItemsets;

        /// <summary>
        /// Gets the set of most frequent itemsets and their counts in the training dataset.
        /// </summary>
        public Dictionary<HashSet<T>, int> FrequentItemsets => frequentItemsets;

        /// <summary>
        /// Initializes a new instance of the Apriori class.
        /// </summary>
        /// <param name="minSupport">Minimum support threshold for an itemset to be considered frequent.</param>
        /// <param name="minConfidence">Minimum confidence threshold for association rules.</param>
        public Apriori(int minSupport, double minConfidence)
        {
            supportMin = minSupport;
            confidence = minConfidence;
            frequentItemsets = new Dictionary<HashSet<T>, int>(new SetComparer());
        }

        /// <summary>
        /// Learns association rules from the provided dataset.
        /// </summary>
        /// <param name="transactions">The dataset to mine.</param>
        /// <returns>A list of association rules generated from the dataset.</returns>
        public List<AssociationRule<T>> Learn(List<HashSet<T>> transactions)
        {
            frequentItemsets.Clear();

            var candidates = new HashSet<HashSet<T>>(new SetComparer());
            var counts = new Dictionary<HashSet<T>, int>(new SetComparer());

            // Generate single-item candidate sets
            foreach (var transaction in transactions)
            {
                foreach (var item in transaction)
                {
                    var candidate = new HashSet<T> { item };
                    candidates.Add(candidate);
                }
            }

            while (candidates.Count > 0)
            {
                counts.Clear();

                // Count support for each candidate
                foreach (var transaction in transactions)
                {
                    foreach (var candidate in candidates)
                    {
                        if (candidate.IsSubsetOf(transaction))
                        {
                            if (!counts.ContainsKey(candidate))
                                counts[candidate] = 0;
                            counts[candidate]++;
                        }
                    }
                }

                candidates.Clear();

                // Filter candidates by support threshold
                foreach (var pair in counts)
                {
                    if (pair.Value >= supportMin)
                    {
                        frequentItemsets[pair.Key] = pair.Value;

                        // Generate larger candidates
                        foreach (var other in frequentItemsets.Keys)
                        {
                            var union = new HashSet<T>(pair.Key);
                            union.UnionWith(other);

                            if (union.Count == pair.Key.Count + 1)
                                candidates.Add(union);
                        }
                    }
                }
            }

            return GenerateRules(transactions);
        }

        /// <summary>
        /// Generates association rules from the frequent itemsets.
        /// </summary>
        /// <param name="transactions">The original dataset to calculate confidence and support.</param>
        /// <returns>A list of association rules.</returns>
        private List<AssociationRule<T>> GenerateRules(List<HashSet<T>> transactions)
        {
            var rules = new List<AssociationRule<T>>();

            foreach (var itemset in frequentItemsets.Keys)
            {
                var subsets = GetSubsets(itemset);

                foreach (var subset in subsets)
                {
                    if (subset.Count == 0 || subset.Count == itemset.Count)
                        continue;

                    var remaining = new HashSet<T>(itemset);
                    remaining.ExceptWith(subset);

                    if (remaining.Count == 0)
                        continue;

                    double subsetSupport = GetSupport(subset, transactions);
                    double itemsetSupport = GetSupport(itemset, transactions);

                    double conf = itemsetSupport / subsetSupport;

                    if (conf >= confidence)
                    {
                        rules.Add(new AssociationRule<T>
                        {
                            X = subset,
                            Y = remaining,
                            Confidence = conf,
                            Support = itemsetSupport
                        });
                    }
                }
            }

            return rules;
        }

        /// <summary>
        /// Calculates the support of a set in the dataset.
        /// </summary>
        /// <param name="set">The itemset to calculate support for.</param>
        /// <param name="transactions">The dataset.</param>
        /// <returns>The support value.</returns>
        private double GetSupport(HashSet<T> set, List<HashSet<T>> transactions)
        {
            int count = 0;

            foreach (var transaction in transactions)
            {
                if (set.IsSubsetOf(transaction))
                    count++;
            }

            return (double)count / transactions.Count;
        }

        /// <summary>
        /// Generates all non-empty subsets of a set.
        /// </summary>
        /// <param name="set">The set to generate subsets from.</param>
        /// <returns>A list of subsets.</returns>
        private List<HashSet<T>> GetSubsets(HashSet<T> set)
        {
            var subsets = new List<HashSet<T>>();
            var items = new List<T>(set);

            int subsetCount = (1 << items.Count);

            for (int i = 1; i < subsetCount; i++)
            {
                var subset = new HashSet<T>();

                for (int j = 0; j < items.Count; j++)
                {
                    if ((i & (1 << j)) > 0)
                        subset.Add(items[j]);
                }

                subsets.Add(subset);
            }

            return subsets;
        }

        /// <summary>
        /// Custom comparer for hash sets to enable dictionary storage.
        /// </summary>
        private class SetComparer : IEqualityComparer<HashSet<T>>
        {
            public bool Equals(HashSet<T> x, HashSet<T> y) => x.SetEquals(y);

            public int GetHashCode(HashSet<T> obj)
            {
                int hash = 0;
                foreach (var item in obj)
                    hash ^= item.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// Represents an association rule.
    /// </summary>
    public class AssociationRule<T>
    {
        public HashSet<T> X { get; set; }
        public HashSet<T> Y { get; set; }
        public double Confidence { get; set; }
        public double Support { get; set; }
    }
}
