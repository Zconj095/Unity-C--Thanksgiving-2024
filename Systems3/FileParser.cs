public abstract class FileParser
{
    public abstract string Parse(string filePath);

    public virtual string GetFileType()
    {
        return "Unknown";
    }
}
