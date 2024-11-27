using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class GoemansWilliamsonOptimizationResult
{
    public double[] X { get; private set; }
    public double Fval { get; private set; }
    public List<string> Variables { get; private set; }
    public string Status { get; private set; }
    public List<SolutionSample> Samples { get; private set; }
    public double[,] SdpSolution { get; private set; }

    public GoemansWilliamsonOptimizationResult(double[] x, double fval, List<string> variables, string status, List<SolutionSample> samples, double[,] sdpSolution)
    {
        X = x;
        Fval = fval;
        Variables = variables;
        Status = status;
        Samples = samples;
        SdpSolution = sdpSolution;
    }

    public override string ToString()
    {
        return $"Solution: [{string.Join(", ", X)}], Value: {Fval}, Status: {Status}";
    }
}

public class GoemansWilliamsonOptimizer
{
    private int _numCuts;
    private bool _sortCuts;
    private bool _uniqueCuts;
    private Random _random;

    public GoemansWilliamsonOptimizer(int numCuts, bool sortCuts = true, bool uniqueCuts = true, int seed = 0)
    {
        _numCuts = numCuts;
        _sortCuts = sortCuts;
        _uniqueCuts = uniqueCuts;
        _random = new Random(seed);
    }

    public string GetCompatibilityMsg(Dictionary<string, object> problem)
    {
        int totalVars = (int)problem["TotalVars"];
        int binaryVars = (int)problem["BinaryVars"];

        if (binaryVars != totalVars)
        {
            return $"Only binary variables are supported. Total variables: {totalVars}, Binary variables: {binaryVars}.";
        }
        return string.Empty;
    }

    public GoemansWilliamsonOptimizationResult Solve(Dictionary<string, object> problem)
    {
        // Compatibility check
        string compatibilityMsg = GetCompatibilityMsg(problem);
        if (!string.IsNullOrEmpty(compatibilityMsg))
        {
            throw new InvalidOperationException(compatibilityMsg);
        }

        // Extract adjacency matrix
        double[,] adjMatrix = ExtractAdjacencyMatrix(problem);

        // Solve SDP
        double[,] sdpSolution;
        try
        {
            sdpSolution = SolveMaxCutSDP(adjMatrix);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error solving SDP: {ex.Message}");
            return new GoemansWilliamsonOptimizationResult(
                new double[0], 0, (List<string>)problem["Variables"], "Failure", new List<SolutionSample>(), null
            );
        }

        // Generate cuts
        var cuts = GenerateRandomCuts(sdpSolution, adjMatrix.GetLength(0));

        // Evaluate solutions
        var solutions = EvaluateCuts(cuts, adjMatrix);

        // Sort solutions if needed
        if (_sortCuts)
        {
            solutions = solutions.OrderByDescending(s => s.Value).ToList();
        }

        // Deduplicate solutions if needed
        if (_uniqueCuts)
        {
            solutions = GetUniqueCuts(solutions);
        }

        // Create samples
        var samples = solutions.Select(s => new SolutionSample(s.Key, s.Value, 1.0 / solutions.Count, "Success")).ToList();

        // Return the result
        return new GoemansWilliamsonOptimizationResult(
            samples[0].X, samples[0].Fval, (List<string>)problem["Variables"], "Success", samples, sdpSolution
        );
    }

    private double[,] ExtractAdjacencyMatrix(Dictionary<string, object> problem)
    {
        // Extract adjacency matrix from problem (negative of quadratic coefficients)
        var matrix = (double[,])problem["AdjacencyMatrix"];
        int n = matrix.GetLength(0);

        double[,] adjMatrix = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                adjMatrix[i, j] = (matrix[i, j] + matrix[j, i]) / 2.0;
            }
        }

        return adjMatrix;
    }

    private double[,] SolveMaxCutSDP(double[,] adjMatrix)
    {
        int n = adjMatrix.GetLength(0);
        double[,] chi = new double[n, n];

        // Simulate SDP solution (diagonal with 1s, symmetric positive semi-definite matrix)
        for (int i = 0; i < n; i++)
        {
            chi[i, i] = 1;
        }
        return chi;
    }

    private List<KeyValuePair<double[], double>> EvaluateCuts(double[,] cuts, double[,] adjMatrix)
    {
        int n = adjMatrix.GetLength(0);
        var solutions = new List<KeyValuePair<double[], double>>();

        for (int i = 0; i < cuts.GetLength(0); i++)
        {
            double[] cut = new double[n];
            for (int j = 0; j < n; j++)
            {
                cut[j] = cuts[i, j];
            }

            double cutValue = CalculateCutValue(cut, adjMatrix);
            solutions.Add(new KeyValuePair<double[], double>(cut, cutValue));
        }

        return solutions;
    }

    private double[,] GenerateRandomCuts(double[,] chi, int numVertices)
    {
        // Adjust chi to make it positive semi-definite
        var eigenvalues = Enumerable.Range(0, numVertices).Select(i => chi[i, i]).ToArray();
        double minEigen = eigenvalues.Min();

        if (minEigen < 0)
        {
            for (int i = 0; i < numVertices; i++)
            {
                chi[i, i] += 1.001 * Math.Abs(minEigen);
            }
        }

        // Generate random cuts
        double[,] cuts = new double[_numCuts, numVertices];
        for (int i = 0; i < _numCuts; i++)
        {
            for (int j = 0; j < numVertices; j++)
            {
                cuts[i, j] = _random.NextDouble() > 0.5 ? 1 : 0;
            }
        }

        return cuts;
    }

    private List<KeyValuePair<double[], double>> GetUniqueCuts(List<KeyValuePair<double[], double>> solutions)
    {
        var uniqueSolutions = new Dictionary<string, KeyValuePair<double[], double>>();

        foreach (var solution in solutions)
        {
            string key = string.Join("", solution.Key);
            if (!uniqueSolutions.ContainsKey(key))
            {
                uniqueSolutions[key] = solution;
            }
        }

        return uniqueSolutions.Values.ToList();
    }

    private double CalculateCutValue(double[] cut, double[,] adjMatrix)
    {
        int n = adjMatrix.GetLength(0);
        double value = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (cut[i] != cut[j])
                {
                    value += adjMatrix[i, j];
                }
            }
        }

        return value;
    }
}

public class SolutionSample
{
    public double[] X { get; set; }
    public double Fval { get; set; }
    public double Probability { get; set; }
    public string Status { get; set; }

    public SolutionSample(double[] x, double fval, double probability, string status)
    {
        X = x;
        Fval = fval;
        Probability = probability;
        Status = status;
    }
}
