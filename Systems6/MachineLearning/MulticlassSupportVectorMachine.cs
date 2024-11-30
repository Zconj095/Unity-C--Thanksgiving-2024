using System;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   One-against-one Multi-class Kernel Support Vector Machine Classifier for Unity.
    /// </summary>
    public class MulticlassSupportVectorMachine
    {
        private object[][] models;
        private Type kernelType;
        private MethodInfo kernelMethod;

        public int NumberOfInputs { get; private set; }
        public int NumberOfClasses { get; private set; }

        public MulticlassSupportVectorMachine(int inputs, int classes, string kernelTypeName)
        {
            NumberOfInputs = inputs;
            NumberOfClasses = classes;

            kernelType = Type.GetType(kernelTypeName);
            if (kernelType == null)
            {
                Debug.LogError($"Kernel type {kernelTypeName} not found.");
                return;
            }

            kernelMethod = kernelType.GetMethod("Compute");
            if (kernelMethod == null)
            {
                Debug.LogError($"Method 'Compute' not found in kernel type {kernelTypeName}.");
                return;
            }

            InitializeMachines();
        }

        private void InitializeMachines()
        {
            models = new object[NumberOfClasses][];

            for (int i = 0; i < NumberOfClasses; i++)
            {
                models[i] = new object[NumberOfClasses];
                for (int j = 0; j < NumberOfClasses; j++)
                {
                    if (i != j)
                    {
                        models[i][j] = Activator.CreateInstance(kernelType, NumberOfInputs);
                    }
                }
            }
        }

        public int Compute(float[] input, out float output)
        {
            if (input.Length != NumberOfInputs)
            {
                Debug.LogError("Input vector size does not match the expected number of inputs.");
                output = 0;
                return -1;
            }

            int decision = -1;
            output = float.MinValue;

            for (int i = 0; i < NumberOfClasses; i++)
            {
                for (int j = i + 1; j < NumberOfClasses; j++)
                {
                    var machine = models[i][j];
                    if (machine != null)
                    {
                        var methodOutput = kernelMethod.Invoke(machine, new object[] { input });
                        if (methodOutput is float distance && distance > output)
                        {
                            output = distance;
                            decision = distance > 0 ? i : j;
                        }
                    }
                }
            }

            return decision;
        }

        public object Clone()
        {
            var clone = new MulticlassSupportVectorMachine(NumberOfInputs, NumberOfClasses, kernelType.AssemblyQualifiedName);
            for (int i = 0; i < NumberOfClasses; i++)
            {
                for (int j = 0; j < NumberOfClasses; j++)
                {
                    if (models[i][j] != null)
                    {
                        var cloneMethod = models[i][j].GetType().GetMethod("Clone");
                        clone.models[i][j] = cloneMethod?.Invoke(models[i][j], null);
                    }
                }
            }
            return clone;
        }
    }
}
