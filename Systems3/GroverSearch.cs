public class GroverSearch
{
    public int PerformSearch(float[][] databaseVectors, float[] queryVector, float similarityThreshold = 0.9f, int iterations = 3)
    {
        int size = databaseVectors.Length;
        QuantumSearchState quantumState = new QuantumSearchState(size);

        VectorDatabaseOracle oracle = new VectorDatabaseOracle(databaseVectors, queryVector, similarityThreshold);

        for (int i = 0; i < iterations; i++)
        {
            quantumState.ApplyOracle(oracle.IsTargetState);
            quantumState.ApplyDiffusion();
        }

        return quantumState.Measure(); // Return the index of the closest match
    }
}
