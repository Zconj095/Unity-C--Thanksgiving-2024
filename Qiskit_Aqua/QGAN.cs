using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class QGAN
{
    private double[,] data;
    private double[,] bounds;
    private int[] numQubits;
    private int batchSize;
    private int numEpochs;
    private int seed;
    private DiscriminativeNetwork discriminator;
    private GenerativeNetwork generator;
    private double? tolRelEnt;
    private string snapshotDir;
    private QuantumInstance quantumInstance;

    private List<double> generatorLoss = new List<double>();
    private List<double> discriminatorLoss = new List<double>();
    private List<double> relativeEntropy = new List<double>();
    private Dictionary<string, object> results = new Dictionary<string, object>();
    private Random random;

    public QGAN(
        double[,] data,
        double[,] bounds = null,
        int[] numQubits = null,
        int batchSize = 500,
        int numEpochs = 3000,
        int seed = 7,
        DiscriminativeNetwork discriminator = null,
        GenerativeNetwork generator = null,
        double? tolRelEnt = null,
        string snapshotDir = null,
        QuantumInstance quantumInstance = null)
    {
        this.data = data ?? throw new ArgumentException("Training data not provided.");
        this.bounds = bounds ?? EstimateBounds(data);
        this.numQubits = numQubits ?? Enumerable.Repeat(3, this.bounds.GetLength(0)).ToArray();
        this.batchSize = Math.Max(batchSize, 1);
        this.numEpochs = numEpochs;
        this.seed = seed;
        this.tolRelEnt = tolRelEnt;
        this.snapshotDir = snapshotDir;
        this.quantumInstance = quantumInstance;

        InitializeNetworks(generator, discriminator);
        random = new Random(seed);
    }

    private void InitializeNetworks(GenerativeNetwork generator, DiscriminativeNetwork discriminator)
    {
        this.generator = generator ?? new QuantumGenerator(bounds, numQubits);
        this.discriminator = discriminator ?? new NumPyDiscriminator(numQubits.Length);
    }

    private double[,] EstimateBounds(double[,] data)
    {
        int dimensions = data.GetLength(1);
        double[,] bounds = new double[dimensions, 2];
        for (int i = 0; i < dimensions; i++)
        {
            double[] column = Enumerable.Range(0, data.GetLength(0))
                                        .Select(row => data[row, i])
                                        .ToArray();
            bounds[i, 0] = column.Min();
            bounds[i, 1] = column.Max();
        }
        return bounds;
    }

    public void Train()
    {
        if (snapshotDir != null)
        {
            InitializeSnapshot();
        }

        for (int epoch = 0; epoch < numEpochs; epoch++)
        {
            ShuffleData();
            TrainEpoch(epoch);

            if (tolRelEnt.HasValue && relativeEntropy.Last() <= tolRelEnt)
            {
                break;
            }
        }
    }

    private void TrainEpoch(int epoch)
    {
        int start = 0;
        while (start + batchSize <= data.GetLength(0))
        {
            var realBatch = GetBatch(data, start, batchSize);
            start += batchSize;

            var (generatedBatch, generatedProb) = generator.GenerateSamples(quantumInstance, batchSize);

            // Train Discriminator
            double discLoss = discriminator.Train(realBatch, generatedBatch, generatedProb);
            discriminatorLoss.Add(discLoss);

            // Train Generator
            generator.SetDiscriminator(discriminator);
            double genLoss = generator.Train(quantumInstance, batchSize);
            generatorLoss.Add(genLoss);

            // Log results
            double relEntropy = CalculateRelativeEntropy();
            relativeEntropy.Add(relEntropy);
            LogEpoch(epoch, discLoss, genLoss, relEntropy);
        }
    }

    private void ShuffleData()
    {
        // Shuffle data rows for randomized batches
        int rows = data.GetLength(0);
        int cols = data.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            int swapIndex = random.Next(rows);
            for (int j = 0; j < cols; j++)
            {
                (data[i, j], data[swapIndex, j]) = (data[swapIndex, j], data[i, j]);
            }
        }
    }

    private double[,] GetBatch(double[,] data, int start, int size)
    {
        int cols = data.GetLength(1);
        double[,] batch = new double[size, cols];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                batch[i, j] = data[start + i, j];
            }
        }
        return batch;
    }

    private double CalculateRelativeEntropy()
    {
        var (samplesGen, probGen) = generator.GenerateSamples(quantumInstance);
        // Compare generated probabilities to the data probabilities
        return Entropy(probGen, generator.GetDataProbabilities());
    }

    private double Entropy(double[] genProb, double[] dataProb)
    {
        double entropy = 0.0;
        for (int i = 0; i < genProb.Length; i++)
        {
            entropy += genProb[i] * Math.Log(genProb[i] / dataProb[i]);
        }
        return entropy;
    }

    private void InitializeSnapshot()
    {
        Directory.CreateDirectory(snapshotDir);
        string outputFile = Path.Combine(snapshotDir, "output.csv");
        using (var writer = new StreamWriter(outputFile))
        {
            writer.WriteLine("Epoch,DiscriminatorLoss,GeneratorLoss,RelativeEntropy");
        }
    }

    private void LogEpoch(int epoch, double discLoss, double genLoss, double relEntropy)
    {
        string log = $"Epoch {epoch + 1}: DiscLoss={discLoss:F4}, GenLoss={genLoss:F4}, RelEntropy={relEntropy:F4}";
        Console.WriteLine(log);

        if (snapshotDir != null)
        {
            string outputFile = Path.Combine(snapshotDir, "output.csv");
            using (var writer = new StreamWriter(outputFile, true))
            {
                writer.WriteLine($"{epoch + 1},{discLoss:F4},{genLoss:F4},{relEntropy:F4}");
            }
        }
    }
}
