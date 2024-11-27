using System;
using System.Linq;
using UnityEngine;

public class LBFGSBOptimizer
{
    public int MaxFunctionEvaluations { get; private set; }
    public int MaxIterations { get; private set; }
    public float Tolerance { get; private set; }
    public float Epsilon { get; private set; }
    public bool Verbose { get; private set; }

    private const float MinStepSize = 1e-10f;

    public LBFGSBOptimizer(
        int maxFunctionEvaluations = 15000,
        int maxIterations = 15000,
        float tolerance = 1e-5f,
        float epsilon = 1e-8f,
        bool verbose = false)
    {
        MaxFunctionEvaluations = maxFunctionEvaluations;
        MaxIterations = maxIterations;
        Tolerance = tolerance;
        Epsilon = epsilon;
        Verbose = verbose;
    }

    public (float[] OptimizedParams, float FinalValue) Minimize(
        Func<float[], float> objectiveFunction,
        Func<float[], float[]> gradientFunction,
        float[] initialPoint,
        (float[] lower, float[] upper) bounds)
    {
        int dimensions = initialPoint.Length;
        float[] currentPoint = initialPoint.ToArray();
        float[] lowerBounds = bounds.lower;
        float[] upperBounds = bounds.upper;

        float[] gradient = new float[dimensions];
        float objectiveValue = objectiveFunction(currentPoint);
        float previousObjectiveValue = objectiveValue;

        int iteration = 0;
        int functionEvaluations = 1;

        float[,] s = new float[dimensions, dimensions];
        float[,] y = new float[dimensions, dimensions];
        float[] rho = new float[dimensions];
        int memoryIndex = 0;

        while (iteration < MaxIterations && functionEvaluations < MaxFunctionEvaluations)
        {
            gradient = gradientFunction != null
                ? gradientFunction(currentPoint)
                : ApproximateGradient(objectiveFunction, currentPoint);

            if (gradient.Max(Mathf.Abs) < Tolerance)
            {
                if (Verbose)
                    Debug.Log($"Converged at iteration {iteration} with objective value {objectiveValue}");
                break;
            }

            float[] searchDirection = ComputeSearchDirection(gradient, s, y, rho, memoryIndex);
            float stepSize = LineSearch(objectiveFunction, currentPoint, gradient, searchDirection);
            float[] nextPoint = currentPoint.Zip(searchDirection, (x, d) => Mathf.Clamp(x + stepSize * d, lowerBounds[0], upperBounds[0])).ToArray();

            UpdateMemory(currentPoint, nextPoint, gradient, ref s, ref y, ref rho, ref memoryIndex);

            previousObjectiveValue = objectiveValue;
            currentPoint = nextPoint;
            objectiveValue = objectiveFunction(currentPoint);
            iteration++;
            functionEvaluations++;

            if (Verbose)
                Debug.Log($"Iteration {iteration}: Objective={objectiveValue}, StepSize={stepSize}");
        }

        return (currentPoint, objectiveValue);
    }

    private float[] ApproximateGradient(Func<float[], float> objectiveFunction, float[] point)
    {
        int dimensions = point.Length;
        float[] gradient = new float[dimensions];

        for (int i = 0; i < dimensions; i++)
        {
            float[] forwardPoint = point.ToArray();
            forwardPoint[i] += Epsilon;

            float[] backwardPoint = point.ToArray();
            backwardPoint[i] -= Epsilon;

            gradient[i] = (objectiveFunction(forwardPoint) - objectiveFunction(backwardPoint)) / (2 * Epsilon);
        }

        return gradient;
    }

    private float[] ComputeSearchDirection(float[] gradient, float[,] s, float[,] y, float[] rho, int memoryIndex)
    {
        int dimensions = gradient.Length;
        float[] q = gradient.ToArray();
        float[] alpha = new float[memoryIndex];

        for (int i = memoryIndex - 1; i >= 0; i--)
        {
            float[] si = s.GetRow(i);
            float[] yi = y.GetRow(i);
            alpha[i] = rho[i] * DotProduct(si, q);
            q = q.Zip(yi, (qj, yj) => qj - alpha[i] * yj).ToArray();
        }

        float[] r = q;

        for (int i = 0; i < memoryIndex; i++)
        {
            float[] si = s.GetRow(i);
            float[] yi = y.GetRow(i);
            float beta = rho[i] * DotProduct(yi, r);
            r = r.Zip(si, (rj, sj) => rj + (alpha[i] - beta) * sj).ToArray();
        }

        return r.Select(v => -v).ToArray();
    }

    private float LineSearch(Func<float[], float> objectiveFunction, float[] currentPoint, float[] gradient, float[] direction)
    {
        float stepSize = 1.0f;
        float armijoParameter = 1e-4f;

        while (objectiveFunction(currentPoint.Zip(direction, (x, d) => x + stepSize * d).ToArray()) >
               objectiveFunction(currentPoint) + armijoParameter * stepSize * DotProduct(gradient, direction))
        {
            stepSize *= 0.5f;
            if (stepSize < MinStepSize)
                break;
        }

        return stepSize;
    }

    private void UpdateMemory(float[] previousPoint, float[] currentPoint, float[] gradient, ref float[,] s, ref float[,] y, ref float[] rho, ref int memoryIndex)
    {
        float[] sk = currentPoint.Zip(previousPoint, (x, xPrev) => x - xPrev).ToArray();
        float[] yk = gradient.Zip(ApproximateGradient(x => 0, currentPoint), (g, gPrev) => g - gPrev).ToArray();

        float curvature = DotProduct(yk, sk);
        if (curvature > 1e-10f)
        {
            s.SetRow(memoryIndex, sk);
            y.SetRow(memoryIndex, yk);
            rho[memoryIndex] = 1 / curvature;
            memoryIndex = (memoryIndex + 1) % s.GetLength(0);
        }
    }

    private float DotProduct(float[] a, float[] b) => a.Zip(b, (ai, bi) => ai * bi).Sum();
}

public static class ArrayExtensions
{
    public static float[] GetRow(this float[,] array, int rowIndex)
    {
        return Enumerable.Range(0, array.GetLength(1)).Select(x => array[rowIndex, x]).ToArray();
    }

    public static void SetRow(this float[,] array, int rowIndex, float[] rowValues)
    {
        for (int i = 0; i < rowValues.Length; i++)
        {
            array[rowIndex, i] = rowValues[i];
        }
    }
}
