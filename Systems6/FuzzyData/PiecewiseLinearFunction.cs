using System;
using UnityEngine;

namespace UnityFuzzy
{
    /// <summary>
    /// Membership function composed of several connected linear functions.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The piecewise linear function represents a generic function using a sequence of 
    /// connected lines. It can approximate trapezoidal functions or any shape that can be 
    /// described by connected linear segments.
    /// </para>
    /// </remarks>
    
    public class PiecewiseLinearFunction : IMembershipFunction
    {
        
        /// <summary>
        /// Struct to hold (X, Y) coordinates.
        /// </summary>
        public struct Point
        {
            public float X;
            public float Y;

            public Point(float x, float y)
            {
                X = x;
                Y = y;
            }
        }

        /// <summary>
        /// Vector of (X, Y) coordinates for the start/end of each line.
        /// </summary>
        public Point[] points;

        /// <summary>
        /// The leftmost x value of the membership function.
        /// </summary>
        public float LeftLimit
        {
            get
            {
                if (points == null || points.Length == 0)
                    throw new NullReferenceException("Points are not initialized.");
                return points[0].X;
            }
        }

        /// <summary>
        /// The rightmost x value of the membership function.
        /// </summary>
        public float RightLimit
        {
            get
            {
                if (points == null || points.Length == 0)
                    throw new NullReferenceException("Points are not initialized.");
                return points[points.Length - 1].X;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PiecewiseLinearFunction"/> class.
        /// </summary>
        /// <param name="points">Array of (X, Y) coordinates for the piecewise function.</param>
        public PiecewiseLinearFunction(Point[] points)
        {
            if (points == null || points.Length < 2)
                throw new ArgumentException("At least two points are required.");

            this.points = points;

            // Validate points
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].Y < 0 || points[i].Y > 1)
                    throw new ArgumentException("Y values must be in the range [0, 1].");

                if (i > 0 && points[i].X <= points[i - 1].X)
                    throw new ArgumentException("X values must be in ascending order.");
            }
        }

        /// <summary>
        /// Calculates the membership of a given value to the piecewise function.
        /// </summary>
        /// <param name="x">Value for which membership is calculated.</param>
        /// <returns>Degree of membership [0..1] of the value to the fuzzy set.</returns>
        public float GetMembership(float x)
        {
            if (points == null || points.Length == 0)
                throw new NullReferenceException("Points are not initialized.");

            if (x < points[0].X)
                return points[0].Y;

            for (int i = 1; i < points.Length; i++)
            {
                if (x < points[i].X)
                {
                    float x0 = points[i - 1].X, y0 = points[i - 1].Y;
                    float x1 = points[i].X, y1 = points[i].Y;

                    // Linear interpolation
                    float slope = (y1 - y0) / (x1 - x0);
                    return slope * (x - x0) + y0;
                }
            }

            return points[points.Length - 1].Y;
        }
    }
}
