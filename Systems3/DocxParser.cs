using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Collections.Generic;

public class DocxParser : FileParser
{
    public override string Parse(string filePath)
    {
        try
        {
            // Open the DOCX file as a ZIP archive
            using (ZipArchive zip = ZipFile.OpenRead(filePath))
            {
                // Extract text content
                string textContent = ExtractText(zip);

                // Extract image data
                List<string> imagePaths = ExtractImages(zip, "ExtractedImages");

                // Combine text and image data in the output
                StringBuilder result = new StringBuilder();
                result.AppendLine("=== Text Content ===");
                result.AppendLine(textContent);
                result.AppendLine("=== Images Extracted ===");
                foreach (var imagePath in imagePaths)
                {
                    result.AppendLine(imagePath);
                }

                return result.ToString();
            }
        }
        catch (Exception ex)
        {
            return $"Error while parsing DOCX: {ex.Message}";
        }
    }

    public override string GetFileType()
    {
        return "DOCX";
    }

    private string ExtractText(ZipArchive zip)
    {
        ZipArchiveEntry documentXmlEntry = zip.GetEntry("word/document.xml");
        if (documentXmlEntry == null)
        {
            throw new FileNotFoundException("The DOCX file does not contain a document.xml file.");
        }

        using (Stream stream = documentXmlEntry.Open())
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);

            // Extract text content
            StringBuilder content = new StringBuilder();
            XmlNodeList paragraphs = xmlDoc.GetElementsByTagName("w:t");

            foreach (XmlNode node in paragraphs)
            {
                content.AppendLine(node.InnerText);
            }

            return content.ToString();
        }
    }

    private List<string> ExtractImages(ZipArchive zip, string outputFolder)
    {
        List<string> imagePaths = new List<string>();

        // Ensure the output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        foreach (var entry in zip.Entries)
        {
            if (entry.FullName.StartsWith("word/media/") && IsImageFile(entry.FullName))
            {
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(entry.FullName));
                using (Stream inputStream = entry.Open())
                using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    inputStream.CopyTo(outputStream);
                }
                imagePaths.Add(outputPath);
            }
        }

        return imagePaths;
    }

    private bool IsImageFile(string fileName)
    {
        string extension = Path.GetExtension(fileName).ToLower();
        return extension == ".jpeg" || extension == ".jpg" || extension == ".png" || extension == ".gif" || extension == ".bmp";
    }
}
