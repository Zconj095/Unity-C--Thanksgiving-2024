using System.IO; // Required for File and Path

public class CodeParser : FileParser
{
    private string lastFilePath; // Store the last parsed file path if needed

    public override string Parse(string filePath)
    {
        lastFilePath = filePath;
        return File.ReadAllText(filePath);
    }

    public override string GetFileType()
    {
        if (string.IsNullOrEmpty(lastFilePath))
        {
            return "Unknown File Type";
        }

        string extension = Path.GetExtension(lastFilePath).ToLower();
        return extension switch
        {
            ".cs" => "C# Code",
            ".py" => "Python Code",
            ".java" => "Java Code",
            ".cpp" => "C++ Code",
            _ => "Unknown Code"
        };
    }
}
