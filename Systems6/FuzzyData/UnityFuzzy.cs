using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UnityFuzzy
{
    /// <summary>
    /// Implements the centroid defuzzification method using Unity APIs and reflection.
    /// </summary>
    public class CentroidDefuzzifier
    {
        private int intervals;

        public CentroidDefuzzifier(int intervals)
        {
            this.intervals = intervals;
        }

        public float Defuzzify(object fuzzyOutput, object normOperator)
        {
            float weightSum = 0, membershipSum = 0;

            // Using reflection to dynamically access OutputVariable properties
            var outputVariable = GetPropertyValue(fuzzyOutput, "OutputVariable");
            float start = GetPropertyValue<float>(outputVariable, "Start");
            float end = GetPropertyValue<float>(outputVariable, "End");

            float increment = (end - start) / intervals;

            // Accessing OutputList dynamically
            var outputList = GetPropertyValue<IEnumerable<object>>(fuzzyOutput, "OutputList");

            for (float x = start; x < end; x += increment)
            {
                foreach (var outputConstraint in outputList)
                {
                    var label = GetPropertyValue(outputConstraint, "Label");
                    float firingStrength = GetPropertyValue<float>(outputConstraint, "FiringStrength");

                    // Evaluate membership using reflection
                    MethodInfo getMembershipMethod = outputVariable.GetType().GetMethod("GetLabelMembership");
                    float membership = (float)getMembershipMethod.Invoke(outputVariable, new object[] { label, x });

                    // Apply norm operation using reflection
                    MethodInfo evaluateMethod = normOperator.GetType().GetMethod("Evaluate");
                    float constrainedMembership = (float)evaluateMethod.Invoke(normOperator, new object[] { membership, firingStrength });

                    weightSum += x * constrainedMembership;
                    membershipSum += constrainedMembership;
                }
            }

            if (Mathf.Approximately(membershipSum, 0))
                throw new Exception("The numerical output is unavailable. All memberships are zero.");

            return weightSum / membershipSum;
        }

        /// <summary>
        /// Helper to get property values dynamically.
        /// </summary>
        private T GetPropertyValue<T>(object obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);
            if (property == null) throw new Exception($"Property '{propertyName}' not found.");
            return (T)property.GetValue(obj);
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            return GetPropertyValue<object>(obj, propertyName);
        }
    }
}
