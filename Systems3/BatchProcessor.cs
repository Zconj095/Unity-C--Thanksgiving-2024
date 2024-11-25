using System.Collections.Generic;
using System.Linq;

public class BatchProcessor
{
    public static List<float[][]> ProcessBatch(float[][] data, int batchSize)
    {
        List<float[][]> batches = new List<float[][]>();
        for (int i = 0; i < data.Length; i += batchSize)
        {
            float[][] batch = data.Skip(i).Take(batchSize).ToArray();
            batches.Add(batch);
        }
        return batches;
    }
}
