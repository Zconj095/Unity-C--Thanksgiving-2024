using System.IO;

public class IsoAccess
{
    public static void ReadFileFromISO(string filePath)
    {
        string isoPath = @"Z:\" + filePath;

        if (File.Exists(isoPath))
        {
            string content = File.ReadAllText(isoPath);
            UnityEngine.Debug.Log($"File Content: {content}");
        }
        else
        {
            UnityEngine.Debug.Log($"File {filePath} not found on the ISO.");
        }
    }
}
