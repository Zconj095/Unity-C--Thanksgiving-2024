using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PegasosQSVC : MonoBehaviour
{
    public enum FitStatus { UNFITTED, FITTED }

    private float C = 1.0f;
    private int numSteps = 1000;
    private bool precomputed = false;
    private System.Random random;
    private Dictionary<int, int> alphas;
    private float[,] xTrain;
    private float[] yTrain;
    private int nSamples;
    private int labelPos;
    private int labelNeg;
    private Dictionary<float, int> labelMap;
    private FitStatus fitStatus = FitStatus.UNFITTED;

    // Constructor
    public void Initialize(float C = 1.0f, int numSteps = 1000, bool precomputed = false, int? seed = null)
    {
        if (C <= 0)
        {
            throw new ArgumentException($"C must be positive, but got {C}.");
        }

        this.C = C;
        this.numSteps = numSteps;
        this.precomputed = precomputed;
        this.random = seed.HasValue ? new System.Random(seed.Value) : new System.Random();
        ResetState();
    }

    public void Fit(float[,] X, float[] y)
    {
        ValidateInput(X, y);

        // Map labels to {+1, -1}
        labelPos = (int)y.Distinct().First();
        labelNeg = (int)y.Distinct().Last();
        labelMap = new Dictionary<float, int> { { labelPos, +1 }, { labelNeg, -1 } };

        xTrain = X;
        yTrain = y;
        nSamples = X.GetLength(0);

        alphas = new Dictionary<int, int>();

        // Training loop
        for (int step = 1; step <= numSteps; step++)
        {
            int i = random.Next(0, nSamples);
            float value = ComputeWeightedKernelSum(i, training: true);

            if ((labelMap[y[i]] * C / step) * value < 1)
            {
                alphas[i] = alphas.ContainsKey(i) ? alphas[i] + 1 : 1;
            }
        }

        fitStatus = FitStatus.FITTED;
    }

    public int[] Predict(float[,] X)
    {
        ValidatePredictInput(X);

        int mSamples = X.GetLength(0);
        int[] predictions = new int[mSamples];

        for (int i = 0; i < mSamples; i++)
        {
            float value = ComputeWeightedKernelSum(i, training: false, xPredict: X);
            predictions[i] = value > 0 ? labelPos : labelNeg;
        }

        return predictions;
    }

    private float ComputeWeightedKernelSum(int index, bool training, float[,] xPredict = null)
    {
        float sum = 0;
        foreach (var kvp in alphas)
        {
            int supportIndex = kvp.Key;
            int alpha = kvp.Value;

            float[] x_i = GetRow(training ? xTrain : xPredict, index);
            float[] x_supp = GetRow(xTrain, supportIndex);

            float kernelValue = VectorDot(x_i, x_supp);
            sum += alpha * labelMap[yTrain[supportIndex]] * kernelValue;
        }

        return sum;
    }

    private void ValidateInput(float[,] X, float[] y)
    {
        if (X.GetLength(0) != y.Length)
        {
            throw new ArgumentException("'X' and 'y' must have the same number of samples.");
        }

        if (y.Distinct().Count() != 2)
        {
            throw new ArgumentException("Only binary classification is supported.");
        }
    }

    private void ValidatePredictInput(float[,] X)
    {
        if (fitStatus == FitStatus.UNFITTED)
        {
            throw new InvalidOperationException("The model must be fitted before prediction.");
        }

        if (precomputed && X.GetLength(0) != xTrain.GetLength(0))
        {
            throw new ArgumentException("For precomputed kernel, prediction input dimensions must match training data.");
        }
    }

    private void ResetState()
    {
        alphas = null;
        xTrain = null;
        yTrain = null;
        nSamples = 0;
        labelMap = null;
        labelPos = 0;
        labelNeg = 0;
        fitStatus = FitStatus.UNFITTED;
    }

    private float[] GetRow(float[,] matrix, int row)
    {
        int cols = matrix.GetLength(1);
        float[] rowData = new float[cols];
        for (int i = 0; i < cols; i++)
        {
            rowData[i] = matrix[row, i];
        }
        return rowData;
    }

    private float VectorDot(float[] vec1, float[] vec2)
    {
        float result = 0;
        for (int i = 0; i < vec1.Length; i++)
        {
            result += vec1[i] * vec2[i];
        }
        return result;
    }
}
