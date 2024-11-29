using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class RansacCircle
    {
        private object ransacInstance; // Dynamic RANSAC instance
        private int[] inliers;

        private Vector2[] points;
        private float[] distances;

        // Dynamically load methods using reflection
        private MethodInfo defineMethod;
        private MethodInfo distanceMethod;
        private MethodInfo degenerateMethod;

        public RansacCircle(float threshold, float probability)
        {
            // Dynamically create an instance of a generic RANSAC class
            var ransacType = Type.GetType("EdgeLoreMachineLearning.RANSAC");
            if (ransacType == null) throw new Exception("RANSAC class not found.");

            var genericType = ransacType.MakeGenericType(typeof(Circle));
            ransacInstance = Activator.CreateInstance(genericType, new object[] { 3, threshold, probability });

            // Dynamically assign RANSAC fitting and distance methods
            defineMethod = ransacType.GetMethod("SetFitting");
            distanceMethod = ransacType.GetMethod("SetDistances");
            degenerateMethod = ransacType.GetMethod("SetDegenerate");

            // Assign our custom methods
            defineMethod.Invoke(ransacInstance, new object[] { (Func<int[], Circle>)(Define) });
            distanceMethod.Invoke(ransacInstance, new object[] { (Func<Circle, float, int[]>)Distance });
            degenerateMethod.Invoke(ransacInstance, new object[] { (Func<int[], bool>)Degenerate });
        }

        public Circle Estimate(IEnumerable<Vector2> inputPoints)
        {
            points = inputPoints.ToArray();
            if (points.Length < 3)
                throw new ArgumentException("At least three points are required to fit a circle.");

            distances = new float[points.Length];
            var computeMethod = ransacInstance.GetType().GetMethod("Compute");
            object[] parameters = new object[] { points.Length, null }; // 'null' as placeholder for 'out inliers'
            computeMethod.Invoke(ransacInstance, parameters);

            // Retrieve the 'out' parameter value
            inliers = (int[])parameters[1];

            return Fit(points.Where((_, idx) => inliers.Contains(idx)).ToArray());
        }

        private Circle Define(int[] indices)
        {
            if (indices.Length != 3)
                throw new ArgumentException("Exactly three indices are required to define a circle.");

            return new Circle(points[indices[0]], points[indices[1]], points[indices[2]]);
        }

        private int[] Distance(Circle c, float threshold)
        {
            for (int i = 0; i < points.Length; i++)
                distances[i] = c.DistanceToPoint(points[i]);
            return distances.Select((d, idx) => d < threshold ? idx : -1).Where(idx => idx >= 0).ToArray();
        }

        private bool Degenerate(int[] indices)
        {
            if (indices.Length != 3)
                throw new ArgumentException("Exactly three indices are required to check degeneracy.");

            var p1 = points[indices[0]];
            var p2 = points[indices[1]];
            var p3 = points[indices[2]];

            // Check for collinearity
            var crossProduct = Vector3.Cross(p2 - p1, p3 - p1);
            return crossProduct.sqrMagnitude < Mathf.Epsilon;
        }

        private Circle Fit(Vector2[] selectedPoints)
        {
            if (selectedPoints.Length != 3)
                throw new ArgumentException("Exactly three points are required to fit a circle.");

            // Construct matrices
            float[,] A = new float[selectedPoints.Length, 3];
            float[] Y = new float[selectedPoints.Length];

            for (int i = 0; i < selectedPoints.Length; i++)
            {
                A[i, 0] = selectedPoints[i].x;
                A[i, 1] = selectedPoints[i].y;
                A[i, 2] = 1;
                Y[i] = selectedPoints[i].x * selectedPoints[i].x + selectedPoints[i].y * selectedPoints[i].y;
            }

            // Least squares computation
            float[,] AT = Transpose(A);
            float[,] B = Multiply(AT, A);
            float[] Z = Multiply(AT, Y);
            float[] c = Solve(B, Z);

            // Extract circle parameters
            float cx = c[0] / 2;
            float cy = c[1] / 2;
            float r = Mathf.Sqrt(c[2] + cx * cx + cy * cy);

            return new Circle(new Vector2(cx, cy), r);
        }

        private float[,] Transpose(float[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            float[,] transposed = new float[cols, rows];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    transposed[j, i] = matrix[i, j];

            return transposed;
        }

        private float[,] Multiply(float[,] A, float[,] B)
        {
            int rowsA = A.GetLength(0);
            int colsA = A.GetLength(1);
            int rowsB = B.GetLength(0);
            int colsB = B.GetLength(1);

            if (colsA != rowsB)
                throw new ArgumentException("Matrix dimensions are not compatible for multiplication.");

            float[,] result = new float[rowsA, colsB];
            for (int i = 0; i < rowsA; i++)
                for (int j = 0; j < colsB; j++)
                    for (int k = 0; k < colsA; k++)
                        result[i, j] += A[i, k] * B[k, j];

            return result;
        }

        private float[] Multiply(float[,] A, float[] B)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);

            if (cols != B.Length)
                throw new ArgumentException("Matrix and vector dimensions are not compatible for multiplication.");

            float[] result = new float[rows];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i] += A[i, j] * B[j];

            return result;
        }

        private float[] Solve(float[,] A, float[] B)
        {
            int n = A.GetLength(0);
            float[] x = new float[n];
            float[,] LU = (float[,])A.Clone();

            // LU decomposition
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    LU[j, i] /= LU[i, i];
                    for (int k = i + 1; k < n; k++)
                        LU[j, k] -= LU[j, i] * LU[i, k];
                }

            // Forward substitution
            for (int i = 0; i < n; i++)
            {
                x[i] = B[i];
                for (int j = 0; j < i; j++)
                    x[i] -= LU[i, j] * x[j];
            }

            // Backward substitution
            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = i + 1; j < n; j++)
                    x[i] -= LU[i, j] * x[j];
                x[i] /= LU[i, i];
            }

            return x;
        }
    }

    public struct Circle
    {
        public Vector2 Center;
        public float Radius;

        public Circle(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            if (Vector3.Cross(p2 - p1, p3 - p1).sqrMagnitude < Mathf.Epsilon)
                throw new ArgumentException("The points are collinear and cannot define a circle.");

            // Calculation for circle center and radius
            Center = (p1 + p2 + p3) / 3; // Simplified example
            Radius = Vector2.Distance(Center, p1); // Example radius
        }

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public float DistanceToPoint(Vector2 point)
        {
            return Mathf.Abs(Vector2.Distance(Center, point) - Radius);
        }
    }
}
