using System.IO;

public class BinaryDataProcessor
{
    public static float[] ProcessBinaryData(string filePath)
    {
        byte[] binaryContent = File.ReadAllBytes(filePath);
        float[] embedding = new float[binaryContent.Length];

        for (int i = 0; i < binaryContent.Length; i++)
        {
            embedding[i] = binaryContent[i] / 255f; // Normalize to [0, 1]
        }

        return embedding;
    }
}
