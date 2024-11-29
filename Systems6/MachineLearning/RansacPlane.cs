using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class RansacPlane
    {
        private object ransacInstance; // Dynamic RANSAC instance
        private int[] inliers;

        private Vector3[] points;
        private float[] distances;

        // Dynamically load methods using reflection
        private MethodInfo defineMethod;
        private MethodInfo distanceMethod;
        private MethodInfo degenerateMethod;

        public RansacPlane(float threshold, float probability)
        {
            // Dynamically create an instance of a generic RANSAC class
            var ransacType = Type.GetType("EdgeLoreMachineLearning.RANSAC");
            if (ransacType == null) throw new Exception("RANSAC class not found.");

            var genericType = ransacType.MakeGenericType(typeof(Plane));
            ransacInstance = Activator.CreateInstance(genericType, new object[] { 3, threshold, probability });

            // Dynamically assign RANSAC fitting and distance methods
            defineMethod = ransacType.GetMethod("SetFitting");
            distanceMethod = ransacType.GetMethod("SetDistances");
            degenerateMethod = ransacType.GetMethod("SetDegenerate");

            // Assign our custom methods
            defineMethod.Invoke(ransacInstance, new object[] { (Func<int[], Plane>)Define });
            distanceMethod.Invoke(ransacInstance, new object[] { (Func<Plane, float, int[]>)Distance });
            degenerateMethod.Invoke(ransacInstance, new object[] { (Func<int[], bool>)Degenerate });
        }

        public Plane Estimate(IEnumerable<Vector3> inputPoints)
        {
            points = inputPoints.ToArray();
            if (points.Length < 3)
                throw new ArgumentException("At least three points are required to fit a plane");

            distances = new float[points.Length];
            var computeMethod = ransacInstance.GetType().GetMethod("Compute");
            object[] parameters = new object[] { points.Length, null }; // 'null' for 'out inliers'
            computeMethod.Invoke(ransacInstance, parameters);

            // Retrieve the 'out' parameter value
            inliers = (int[])parameters[1];


            if (inliers.Length == 0)
                throw new Exception("No inliers found for the plane");

            return Fit(points.Where((_, idx) => inliers.Contains(idx)).ToArray());
        }

        private Plane Define(int[] indices)
        {
            if (indices.Length != 3)
                throw new ArgumentException("Three points are required to define a plane");
            return Plane.FromPoints(points[indices[0]], points[indices[1]], points[indices[2]]);
        }

        private int[] Distance(Plane plane, float threshold)
        {
            for (int i = 0; i < points.Length; i++)
                distances[i] = plane.DistanceToPoint(points[i]);
            return distances.Select((d, idx) => d < threshold ? idx : -1).Where(idx => idx >= 0).ToArray();
        }

        private bool Degenerate(int[] indices)
        {
            if (indices.Length != 3)
                throw new ArgumentException("Three points are required to check degeneracy");

            var p1 = points[indices[0]];
            var p2 = points[indices[1]];
            var p3 = points[indices[2]];

            // Check for collinearity
            var crossProduct = Vector3.Cross(p2 - p1, p3 - p1);
            return crossProduct.sqrMagnitude < Mathf.Epsilon;
        }

        private Plane Fit(Vector3[] selectedPoints)
        {
            if (selectedPoints.Length == 3)
                return Plane.FromPoints(selectedPoints[0], selectedPoints[1], selectedPoints[2]);

            // Using least-squares to fit a plane
            int n = selectedPoints.Length;
            float sumX = 0, sumY = 0, sumZ = 0;
            float sumXX = 0, sumXY = 0, sumXZ = 0;
            float sumYY = 0, sumYZ = 0;

            foreach (var p in selectedPoints)
            {
                sumX += p.x;
                sumY += p.y;
                sumZ += p.z;
                sumXX += p.x * p.x;
                sumXY += p.x * p.y;
                sumXZ += p.x * p.z;
                sumYY += p.y * p.y;
                sumYZ += p.y * p.z;
            }

            // Normal vector of the plane (Ax + By + Cz + D = 0)
            var normal = new Vector3(
                sumYZ * sumY - sumXY * sumZ,
                sumXZ * sumZ - sumXX * sumY,
                sumXX * sumYZ - sumXY * sumXZ
            ).normalized;

            // Plane offset (D)
            var center = new Vector3(sumX / n, sumY / n, sumZ / n);
            var offset = -Vector3.Dot(normal, center);

            return new Plane(normal, offset);
        }
    }

    public struct Plane
    {
        public Vector3 Normal;
        public float Offset;

        public Plane(Vector3 normal, float offset)
        {
            Normal = normal;
            Offset = offset;
        }

        public static Plane FromPoints(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var normal = Vector3.Cross(p2 - p1, p3 - p1).normalized;
            var offset = -Vector3.Dot(normal, p1);
            return new Plane(normal, offset);
        }

        public float DistanceToPoint(Vector3 point)
        {
            return Mathf.Abs(Vector3.Dot(Normal, point) + Offset) / Normal.magnitude;
        }
    }
}
