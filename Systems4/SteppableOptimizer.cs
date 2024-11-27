using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteppableOptimizer
{
    public OptimizerState State { get; private set; }
    public int MaxIterations { get; private set; }

    protected SteppableOptimizer(int maxIterations = 100)
    {
        MaxIterations = maxIterations;
    }

    public void Start(Func<double[], double> function, double[] initialPoint, Func<double[], double[]>? gradient = null, Tuple<double, double>[]? bounds = null)
    {
        State = new OptimizerState
        {
            X = initialPoint,
            Fun = function,
            Jac = gradient,
            Nfev = 0,
            Njev = 0,
            Nit = 0
        };

        InitializeState(function, initialPoint, gradient, bounds);
    }

    protected abstract void InitializeState(Func<double[], double> function, double[] initialPoint, Func<double[], double[]>? gradient, Tuple<double, double>[]? bounds);

    public abstract AskData Ask();

    public abstract void Tell(AskData askData, TellData tellData);

    public abstract TellData Evaluate(AskData askData);

    public void Step()
    {
        var askData = Ask();
        var tellData = Evaluate(askData);
        Tell(askData, tellData);
    }

    public OptimizerResult Minimize(Func<double[], double> function, double[] initialPoint, Func<double[], double[]>? gradient = null, Tuple<double, double>[]? bounds = null)
    {
        Start(function, initialPoint, gradient, bounds);

        while (ShouldContinue())
        {
            Step();
            OnIterationEnd();
        }

        return CreateResult();
    }

    protected virtual void OnIterationEnd()
    {
        // Optional hook for derived classes to execute code after each iteration.
    }

    protected virtual bool ShouldContinue()
    {
        return State.Nit < MaxIterations;
    }

    public abstract OptimizerResult CreateResult();

    public class OptimizerState
    {
        public double[] X { get; set; }
        public Func<double[], double>? Fun { get; set; }
        public Func<double[], double[]>? Jac { get; set; }
        public int Nfev { get; set; }
        public int Njev { get; set; }
        public int Nit { get; set; }
    }

    public class AskData
    {
        public double[] XFun { get; set; }
        public double[] XJac { get; set; }

        public AskData(double[] xFun, double[] xJac)
        {
            XFun = xFun;
            XJac = xJac;
        }
    }

    public class TellData
    {
        public double EvalFun { get; set; }
        public double[] EvalJac { get; set; }

        public TellData(double evalFun, double[] evalJac)
        {
            EvalFun = evalFun;
            EvalJac = evalJac;
        }
    }

    public class OptimizerResult
    {
        public double[] Parameters { get; set; }
        public double Loss { get; set; }
        public int FunctionEvaluations { get; set; }
        public int GradientEvaluations { get; set; }
        public int Iterations { get; set; }

        public OptimizerResult(double[] parameters, double loss, int functionEvaluations, int gradientEvaluations, int iterations)
        {
            Parameters = parameters;
            Loss = loss;
            FunctionEvaluations = functionEvaluations;
            GradientEvaluations = gradientEvaluations;
            Iterations = iterations;
        }
    }
}
