using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Pair of class labels.
    /// </summary>
    [Serializable]
    public struct ClassPair : IEquatable<ClassPair>
    {
        [SerializeField] private int class1;
        [SerializeField] private int class2;

        /// <summary>
        ///   Gets the first class in the pair.
        /// </summary>
        public int Class1 => class1;

        /// <summary>
        ///   Gets the second class in the pair.
        /// </summary>
        public int Class2 => class2;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ClassPair"/> struct.
        /// </summary>
        public ClassPair(int i, int j)
        {
            class1 = i;
            class2 = j;
        }

        /// <summary>
        ///   Converts to a tuple (class1, class2) equivalent.
        /// </summary>
        public (int, int) ToTuple()
        {
            return (Class1, Class2);
        }

        /// <summary>
        ///   Returns a string representation of this instance.
        /// </summary>
        public override string ToString()
        {
            return $"{Class1},{Class2}";
        }

        /// <summary>
        ///   Determines if the current object is equal to another object of the same type.
        /// </summary>
        public bool Equals(ClassPair other)
        {
            return class1 == other.class1 && class2 == other.class2;
        }

        /// <summary>
        ///   Determines if the specified object is equal to this instance.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is ClassPair other && Equals(other);
        }

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return class1 * 47 + class2;
        }
    }

    /// <summary>
    ///   Decision between two class labels.
    /// </summary>
    [Serializable]
    public struct Decision
    {
        [SerializeField] private ClassPair pair;
        [SerializeField] private int winner;

        /// <summary>
        ///   Gets the adversarial classes.
        /// </summary>
        public ClassPair Pair => pair;

        /// <summary>
        ///   Gets the class label of the winner.
        /// </summary>
        public int Winner => winner;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Decision"/> struct.
        /// </summary>
        public Decision(int i, int j, int winner)
        {
            if (winner != i && winner != j)
                throw new ArgumentOutOfRangeException(nameof(winner), "Winner must be one of the provided classes.");

            pair = new ClassPair(i, j);
            this.winner = winner;
        }

        /// <summary>
        ///   Converts to a triplet (class1, class2, winner).
        /// </summary>
        public (int, int, int) ToTuple()
        {
            return (Pair.Class1, Pair.Class2, Winner);
        }

        /// <summary>
        ///   Returns a string representation of this instance.
        /// </summary>
        public override string ToString()
        {
            return $"{Pair.Class1},{Pair.Class2}>{Winner}";
        }
    }
}
