using System.Threading.Tasks;

public class HyperthreadedOptimizer
{
    public static void OptimizeParallel(MultiStateVector[] vectors, float[] feedback, float learningRate)
    {
        Parallel.For(0, vectors.Length, i =>
        {
            vectors[i].Optimize(feedback, learningRate); // Optimize each vector in parallel
        });
    }
}
