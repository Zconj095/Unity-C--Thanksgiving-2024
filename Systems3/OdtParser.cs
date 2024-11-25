using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;

public class OdtParser : FileParser
{
    public override string Parse(string filePath)
    {
        try
        {
            // Open the ODT file as a ZIP archive
            using (ZipArchive zip = ZipFile.OpenRead(filePath))
            {
                // Locate the content.xml file in the archive
                ZipArchiveEntry contentXmlEntry = zip.GetEntry("content.xml");
                if (contentXmlEntry == null)
                {
                    throw new FileNotFoundException("The ODT file does not contain a content.xml file.");
                }

                // Extract and parse the content.xml file
                using (Stream stream = contentXmlEntry.Open())
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(stream);

                    // Extract text content from the XML
                    StringBuilder content = new StringBuilder();
                    XmlNodeList textNodes = xmlDoc.GetElementsByTagName("text:p");

                    foreach (XmlNode node in textNodes)
                    {
                        content.AppendLine(node.InnerText);
                    }

                    return content.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            return $"Error while parsing ODT: {ex.Message}";
        }
    }

    public override string GetFileType()
    {
        return "ODT";
    }
}
