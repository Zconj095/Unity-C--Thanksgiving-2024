using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PBFGS : MonoBehaviour
{
    private readonly Dictionary<string, object> _settings = new Dictionary<string, object>();
    private readonly object lockObject = new object();

    public PBFGS(int maxFun = 1000, double ftol = 1e-8, int iprint = -1, int? maxProcesses = null)
    {
        // Initialize parameters
        _settings["maxFun"] = maxFun;
        _settings["ftol"] = ftol;
        _settings["iprint"] = iprint;
        _settings["maxProcesses"] = maxProcesses ?? Environment.ProcessorCount - 1;
    }

    public void SetSetting(string name, object value)
    {
        if (_settings.ContainsKey(name))
        {
            _settings[name] = value;
        }
        else
        {
            throw new ArgumentException($"Setting '{name}' does not exist.");
        }
    }

    public object GetSetting(string name)
    {
        if (_settings.ContainsKey(name))
        {
            return _settings[name];
        }
        throw new ArgumentException($"Setting '{name}' does not exist.");
    }

    public object InvokePrivateMethod(string methodName, object[] parameters)
    {
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (method != null)
        {
            return method.Invoke(this, parameters);
        }
        throw new ArgumentException($"Method '{methodName}' does not exist.");
    }

    public OptimizerResult Minimize(Func<double[], double> objectiveFunction, double[] initialPoint, Tuple<double, double>[]? bounds = null)
    {
        int maxProcesses = (int)_settings["maxProcesses"];
        int numProcs = Math.Max(0, maxProcesses);

        double[] low, high;

        if (bounds != null)
        {
            low = Array.ConvertAll(bounds, b => b.Item1);
            high = Array.ConvertAll(bounds, b => b.Item2);
        }
        else
        {
            low = GenerateBounds(-2 * Math.PI, initialPoint.Length);
            high = GenerateBounds(2 * Math.PI, initialPoint.Length);
        }

        List<Task<OptimizerResult>> tasks = new List<Task<OptimizerResult>>();
        CancellationTokenSource cts = new CancellationTokenSource();

        for (int i = 0; i < numProcs; i++)
        {
            double[] randomPoint = GenerateRandomPointWithinBounds(low, high);
            tasks.Add(Task.Run(() => RunOptimization(objectiveFunction, randomPoint, bounds), cts.Token));
        }

        OptimizerResult mainResult = RunOptimization(objectiveFunction, initialPoint, bounds);

        foreach (var task in tasks)
        {
            task.Wait(cts.Token);
            if (task.Result.Fun < mainResult.Fun)
            {
                mainResult = task.Result;
            }
        }

        return mainResult;
    }

    private OptimizerResult RunOptimization(Func<double[], double> objectiveFunction, double[] initialPoint, Tuple<double, double>[]? bounds)
    {
        // Simulated single optimization iteration using L-BFGS-B logic
        double value = objectiveFunction(initialPoint);
        OptimizerResult result = new OptimizerResult
        {
            X = initialPoint,
            Fun = value,
            Nfev = 1
        };
        return result;
    }

    private double[] GenerateBounds(double value, int size)
    {
        double[] bounds = new double[size];
        for (int i = 0; i < size; i++)
        {
            bounds[i] = value;
        }
        return bounds;
    }

    private double[] GenerateRandomPointWithinBounds(double[] low, double[] high)
    {
        double[] randomPoint = new double[low.Length];
        lock (lockObject)
        {
            System.Random random = new System.Random();
            for (int i = 0; i < low.Length; i++)
            {
                randomPoint[i] = low[i] + (high[i] - low[i]) * random.NextDouble();
            }
        }
        return randomPoint;
    }

    public class OptimizerResult
    {
        public double[] X { get; set; }
        public double Fun { get; set; }
        public int Nfev { get; set; }
    }
}
