using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class EffectiveDimension
{
    private object _model;
    private float[,] _weightSamples;
    private float[,] _inputSamples;
    private int _numWeightSamples;
    private int _numInputSamples;

    public EffectiveDimension(object model, int weightSampleCount = 1, int inputSampleCount = 1)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        SetWeightSamples(weightSampleCount);
        SetInputSamples(inputSampleCount);
    }

    public float[,] WeightSamples
    {
        get => _weightSamples;
        set
        {
            int numWeights = GetProperty<int>(_model, "NumWeights");
            if (value.GetLength(1) != numWeights)
                throw new ArgumentException("Weight samples must match model's weight dimension.");
            _weightSamples = value;
            _numWeightSamples = _weightSamples.GetLength(0);
        }
    }

    public float[,] InputSamples
    {
        get => _inputSamples;
        set
        {
            int numInputs = GetProperty<int>(_model, "NumInputs");
            if (value.GetLength(1) != numInputs)
                throw new ArgumentException("Input samples must match model's input dimension.");
            _inputSamples = value;
            _numInputSamples = _inputSamples.GetLength(0);
        }
    }

    private void SetWeightSamples(int count)
    {
        int numWeights = GetProperty<int>(_model, "NumWeights");
        _weightSamples = GenerateRandomArray(count, numWeights);
        _numWeightSamples = count;
    }

    private void SetInputSamples(int count)
    {
        int numInputs = GetProperty<int>(_model, "NumInputs");
        _inputSamples = GenerateRandomArray(count, numInputs);
        _numInputSamples = count;
    }

    public (float[,,], float[,]) RunMonteCarlo()
    {
        int outputSize = GetProperty<int>(_model, "OutputShape");
        float[,,] gradients = new float[_numInputSamples * _numWeightSamples, outputSize, _numWeightSamples];
        float[,] outputs = new float[_numInputSamples * _numWeightSamples, outputSize];

        for (int idx = 0; idx < _weightSamples.GetLength(0); idx++)
        {
            float[] paramSet = GetRow(_weightSamples, idx);
            float[,] forwardPass = InvokeMethod<float[,]>(_model, "Forward", _inputSamples, paramSet);
            float[,,] backwardPass = InvokeMethod<float[,,]>(_model, "Backward", _inputSamples, paramSet);

            for (int i = 0; i < _numInputSamples; i++)
            {
                int globalIdx = _numInputSamples * idx + i;
                SetRow(outputs, globalIdx, GetRow(forwardPass, i));
                SetSlice(gradients, globalIdx, GetSlice(backwardPass, i));
            }
        }

        return (gradients, outputs);
    }

    private static T GetProperty<T>(object obj, string propertyName)
    {
        PropertyInfo property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
            throw new MissingMemberException($"Property {propertyName} not found on type {obj.GetType()}.");
        return (T)property.GetValue(obj);
    }

    private static T InvokeMethod<T>(object obj, string methodName, params object[] args)
    {
        MethodInfo method = obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        if (method == null)
            throw new MissingMethodException($"Method {methodName} not found on type {obj.GetType()}.");
        return (T)method.Invoke(obj, args);
    }

    private static float[,] GenerateRandomArray(int rows, int cols)
    {
        var array = new float[rows, cols];
        var random = new System.Random();
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                array[i, j] = (float)random.NextDouble();
        return array;
    }

    private static float[] GetRow(float[,] matrix, int rowIndex)
    {
        int cols = matrix.GetLength(1);
        float[] row = new float[cols];
        for (int j = 0; j < cols; j++)
        {
            row[j] = matrix[rowIndex, j];
        }
        return row;
    }

    private static void SetRow(float[,] matrix, int rowIndex, float[] row)
    {
        int cols = matrix.GetLength(1);
        for (int j = 0; j < cols; j++)
        {
            matrix[rowIndex, j] = row[j];
        }
    }

    private static float[,] GetSlice(float[,,] array, int index)
    {
        int rows = array.GetLength(1);
        int cols = array.GetLength(2);
        float[,] slice = new float[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                slice[i, j] = array[index, i, j];
            }
        }
        return slice;
    }

    private static void SetSlice(float[,,] array, int index, float[,] slice)
    {
        int rows = slice.GetLength(0);
        int cols = slice.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[index, i, j] = slice[i, j];
            }
        }
    }
}
