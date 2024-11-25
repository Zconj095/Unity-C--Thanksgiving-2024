using System;
using System.Collections.Generic;
using System.IO; // Required for Path

public class FileParserManager
{
    private Dictionary<string, FileParser> parsers = new Dictionary<string, FileParser>();

    public FileParserManager()
    {
        // Register parsers for supported formats
        parsers.Add(".pdf", new PdfParser());
        parsers.Add(".txt", new TxtParser());
        parsers.Add(".docx", new DocxParser());
        parsers.Add(".odt", new OdtParser());
        parsers.Add(".rtf", new RtfParser());
        parsers.Add(".cs", new CodeParser());
        parsers.Add(".py", new CodeParser());
    }

    public string ParseFile(string filePath)
    {
        string extension = Path.GetExtension(filePath).ToLower();
        if (parsers.ContainsKey(extension))
        {
            return parsers[extension].Parse(filePath);
        }
        throw new NotSupportedException($"File format {extension} is not supported.");
    }

    public string GetFileType(string filePath)
    {
        string extension = Path.GetExtension(filePath).ToLower();
        if (parsers.ContainsKey(extension))
        {
            return parsers[extension].GetFileType();
        }
        return "Unknown";
    }
}
