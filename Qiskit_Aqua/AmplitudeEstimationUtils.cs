using System;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeEstimationUtils
{
    public AmplitudeEstimationUtils() { }

    public float BisectMax(Func<float, float> f, float a, float b, int steps = 50, float minWidth = 1e-12f)
    {
        int iterations = 0;
        float m = (a + b) / 2f;
        float fm = 0f;

        while (iterations < steps && (b - a > minWidth))
        {
            float l = (a + m) / 2f;
            float r = (m + b) / 2f;

            float fl = f(l);
            fm = f(m);
            float fr = f(r);

            if (fl > fm && fl > fr)
            {
                b = m;
                m = l;
            }
            else if (fr > fm && fr > fl)
            {
                a = m;
                m = r;
            }
            else
            {
                a = l;
                b = r;
            }

            iterations++;
        }

        if (iterations == steps)
        {
            Debug.LogWarning("-- Warning, BisectMax did not converge after maximum steps");
        }

        return m;
    }

    public float CircDist(float x, float p)
    {
        float t = p - x;
        float[] z = { -1f, 0f, 1f };
        float minDist = float.MaxValue;

        foreach (float shift in z)
        {
            float distance = Mathf.Abs(shift + t);
            if (distance < minDist)
            {
                minDist = distance;
            }
        }

        return minDist;
    }

    public float DerivativeCircDist(float x, float p)
    {
        float t = p - x;

        if (t < -0.5f || (0 < t && t < 0.5f)) return -1f;
        if (t > 0.5f || (-0.5f < t && t < 0f)) return 1f;

        return 0f;
    }

    public float Omega(float a)
    {
        return Mathf.Asin(Mathf.Sqrt(a)) / Mathf.PI;
    }

    public float DerivativeOmega(float a)
    {
        return 1f / (2f * Mathf.PI * Mathf.Sqrt((1f - a) * a));
    }

    public float Alpha(float x, float p)
    {
        return Mathf.PI * CircDist(Omega(x), Omega(p));
    }

    public float DerivativeAlpha(float x, float p)
    {
        return Mathf.PI * DerivativeCircDist(Omega(x), Omega(p)) * DerivativeOmega(p);
    }

    public float Beta(float x, float p)
    {
        return Mathf.PI * CircDist(1f - Omega(x), Omega(p));
    }

    public float DerivativeBeta(float x, float p)
    {
        return Mathf.PI * DerivativeCircDist(1f - Omega(x), Omega(p)) * DerivativeOmega(p);
    }

    public float PdfASingleAngle(float x, float p, int m, Func<float, float, float> piDelta)
    {
        int M = 1 << m; // 2^m
        float d = piDelta(x, p);
        return (d != 0) ? Mathf.Pow(Mathf.Sin(M * d), 2) / Mathf.Pow(M * Mathf.Sin(d), 2) : 1f;
    }

    public float PdfA(float x, float p, int m)
    {
        float alphaVal = PdfASingleAngle(x, p, m, Alpha);
        float betaVal = PdfASingleAngle(x, p, m, Beta);

        return (x != 0 && x != 1) ? alphaVal + betaVal : alphaVal;
    }

    public float DerivativeLogPdfA(float x, float p, int m)
    {
        int M = 1 << m;

        if (x != 0 && x != 1)
        {
            float numeratorP1 = 0f;
            float denominatorP1 = 0f;

            Func<float, float, float>[] A = { Alpha, Beta };
            Func<float, float, float>[] dA = { DerivativeAlpha, DerivativeBeta };
            Func<float, float, float>[] B = { Beta, Alpha };
            Func<float, float, float>[] dB = { DerivativeBeta, DerivativeAlpha };

            for (int i = 0; i < 2; i++)
            {
                numeratorP1 += 2 * M * Mathf.Sin(M * A[i](x, p)) * Mathf.Cos(M * A[i](x, p)) * dA[i](x, p) * Mathf.Pow(Mathf.Sin(B[i](x, p)), 2);
                numeratorP1 += 2 * Mathf.Pow(Mathf.Sin(M * A[i](x, p)), 2) * Mathf.Sin(B[i](x, p)) * Mathf.Cos(B[i](x, p)) * dB[i](x, p);

                denominatorP1 += Mathf.Pow(Mathf.Sin(M * A[i](x, p)), 2) * Mathf.Pow(Mathf.Sin(B[i](x, p)), 2);
            }

            float numeratorP2 = 0f;
            float denominatorP2 = Mathf.Sin(Alpha(x, p)) * Mathf.Sin(Beta(x, p));

            for (int i = 0; i < 2; i++)
            {
                numeratorP2 += 2 * Mathf.Cos(A[i](x, p)) * dA[i](x, p) * Mathf.Sin(B[i](x, p));
            }

            return numeratorP1 / denominatorP1 - numeratorP2 / denominatorP2;
        }

        return 2f * DerivativeAlpha(x, p) * ((M / Mathf.Tan(M * Alpha(x, p))) - (1 / Mathf.Tan(Alpha(x, p))));
    }
}
