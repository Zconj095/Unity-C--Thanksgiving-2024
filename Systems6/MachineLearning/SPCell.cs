using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Region of space in a Space-Partitioning Tree.
    ///   Represents an axis-aligned bounding box stored as a center with half-dimensions
    ///   to represent the boundaries of this space cell.
    /// </summary>
    [Serializable]
    public class SPCell
    {
        [SerializeField]
        private double[] corner;

        [SerializeField]
        private double[] width;

        /// <summary>
        ///   Gets the number of dimensions of the space delimited by this spatial cell.
        /// </summary>
        public int Dimension => corner.Length;

        /// <summary>
        ///   Gets or sets the starting point (corner) of this spatial cell.
        /// </summary>
        public double[] Corner
        {
            get => (double[])corner.Clone();
            set
            {
                if (value == null || value.Length != corner.Length)
                    throw new ArgumentException("Corner array must match the dimensions of the cell.");
                corner = (double[])value.Clone();
            }
        }

        /// <summary>
        ///   Gets or sets the width (half-dimensions) of this spatial cell.
        /// </summary>
        public double[] Width
        {
            get => (double[])width.Clone();
            set
            {
                if (value == null || value.Length != width.Length)
                    throw new ArgumentException("Width array must match the dimensions of the cell.");
                width = (double[])value.Clone();
            }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="SPCell"/> class.
        /// </summary>
        /// <param name="dimension">The number of dimensions of the space.</param>
        public SPCell(int dimension)
        {
            if (dimension <= 0)
                throw new ArgumentException("Dimension must be greater than zero.", nameof(dimension));

            corner = new double[dimension];
            width = new double[dimension];
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="SPCell"/> class.
        /// </summary>
        /// <param name="corner">The starting point of this spatial cell.</param>
        /// <param name="width">The widths (half-dimensions) of this spatial cell.</param>
        public SPCell(double[] corner, double[] width)
        {
            if (corner == null || width == null || corner.Length != width.Length)
                throw new ArgumentException("Corner and width arrays must have the same dimensions.");

            this.corner = (double[])corner.Clone();
            this.width = (double[])width.Clone();
        }

        /// <summary>
        ///   Determines whether a point lies inside this cell.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>True if the point is contained inside this cell; otherwise, false.</returns>
        public bool Contains(double[] point)
        {
            if (point == null || point.Length != corner.Length)
                throw new ArgumentException("Point must match the dimensions of the cell.", nameof(point));

            for (int d = 0; d < corner.Length; d++)
            {
                if (point[d] < corner[d] - width[d] || point[d] > corner[d] + width[d])
                    return false;
            }

            return true;
        }

        /// <summary>
        ///   Returns a string representation of the cell for debugging purposes.
        /// </summary>
        public override string ToString()
        {
            return $"SPCell(Corner: {ArrayToString(corner)}, Width: {ArrayToString(width)})";
        }

        /// <summary>
        ///   Helper method to convert an array to a string.
        /// </summary>
        private static string ArrayToString(double[] array)
        {
            return array == null ? "null" : $"[{string.Join(", ", array)}]";
        }
    }
}
