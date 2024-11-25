using System.IO;

public class TxtParser : FileParser
{
    public override string Parse(string filePath)
    {
        return File.ReadAllText(filePath);
    }

    public override string GetFileType()
    {
        return "TXT";
    }
}
