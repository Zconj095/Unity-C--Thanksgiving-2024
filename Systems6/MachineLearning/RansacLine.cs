using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class RansacLine
    {
        private object ransacInstance; // Dynamic RANSAC instance
        private int[] inliers;

        private Vector2[] points;
        private float[] distances;

        // Dynamically load methods using reflection
        private MethodInfo defineMethod;
        private MethodInfo distanceMethod;
        private MethodInfo degenerateMethod;

        public RansacLine(float threshold, float probability)
        {
            // Dynamically create an instance of a generic RANSAC class
            var ransacType = Type.GetType("EdgeLoreMachineLearning.RANSAC");
            if (ransacType == null) throw new Exception("RANSAC class not found.");

            var genericType = ransacType.MakeGenericType(typeof(Line));
            ransacInstance = Activator.CreateInstance(genericType, new object[] { 2, threshold, probability });

            // Dynamically assign RANSAC fitting and distance methods
            defineMethod = ransacType.GetMethod("SetFitting");
            distanceMethod = ransacType.GetMethod("SetDistances");
            degenerateMethod = ransacType.GetMethod("SetDegenerate");

            // Assign our custom methods
            defineMethod.Invoke(ransacInstance, new object[] { (Func<int[], Line>)DefineLine });
            distanceMethod.Invoke(ransacInstance, new object[] { (Func<Line, float, int[]>)Distance });
            degenerateMethod.Invoke(ransacInstance, new object[] { (Func<int[], bool>)Degenerate });
        }

        public Line Estimate(IEnumerable<Vector2> inputPoints)
        {
            points = inputPoints.ToArray();
            if (points.Length < 2)
                throw new ArgumentException("At least two points are required to fit a line");

            distances = new float[points.Length];
            var computeMethod = ransacInstance.GetType().GetMethod("Compute");
            object[] parameters = new object[] { points.Length, null }; // 'null' placeholder for 'out inliers'
            computeMethod.Invoke(ransacInstance, parameters);

            // Retrieve the 'out' parameter value after invocation
            inliers = (int[])parameters[1];


            return Fit(points.Where((_, idx) => inliers.Contains(idx)).ToArray());
        }

        private Line DefineLine(int[] indices)
        {
            if (indices.Length != 2)
                throw new ArgumentException("Two points are required to define a line");
            return Line.FromPoints(points[indices[0]], points[indices[1]]);
        }

        private int[] Distance(Line line, float threshold)
        {
            for (int i = 0; i < points.Length; i++)
                distances[i] = line.DistanceToPoint(points[i]);
            return distances.Select((d, idx) => d < threshold ? idx : -1).Where(idx => idx >= 0).ToArray();
        }

        private bool Degenerate(int[] indices)
        {
            if (indices.Length != 2)
                throw new ArgumentException("Two points are required to check degeneracy");
            return points[indices[0]] == points[indices[1]];
        }

        private Line Fit(Vector2[] selectedPoints)
        {
            if (selectedPoints.Length == 2)
                return Line.FromPoints(selectedPoints[0], selectedPoints[1]);

            // Using least-squares to fit a line: y = mx + b
            float xSum = 0, ySum = 0, xySum = 0, xxSum = 0;
            int n = selectedPoints.Length;

            foreach (var p in selectedPoints)
            {
                xSum += p.x;
                ySum += p.y;
                xySum += p.x * p.y;
                xxSum += p.x * p.x;
            }

            float slope = (n * xySum - xSum * ySum) / (n * xxSum - xSum * xSum);
            float intercept = (ySum - slope * xSum) / n;

            return Line.FromSlopeIntercept(slope, intercept);
        }
    }

    public struct Line
    {
        public float Slope;
        public float Intercept;

        private Line(float slope, float intercept)
        {
            Slope = slope;
            Intercept = intercept;
        }

        public static Line FromPoints(Vector2 p1, Vector2 p2)
        {
            float slope = (p2.y - p1.y) / (p2.x - p1.x);
            float intercept = p1.y - slope * p1.x;
            return new Line(slope, intercept);
        }

        public static Line FromSlopeIntercept(float slope, float intercept)
        {
            return new Line(slope, intercept);
        }

        public float DistanceToPoint(Vector2 point)
        {
            // Distance from point to line: |Ax + By + C| / sqrt(A^2 + B^2)
            float A = Slope;
            float B = -1;
            float C = Intercept;

            return Mathf.Abs(A * point.x + B * point.y + C) / Mathf.Sqrt(A * A + B * B);
        }
    }
}
