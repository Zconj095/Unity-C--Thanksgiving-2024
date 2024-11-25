using System;
using System.IO;
using System.Text;

public class PdfParser : FileParser
{
    public override string Parse(string filePath)
    {
        try
        {
            // Read the raw bytes of the PDF file
            byte[] pdfBytes = File.ReadAllBytes(filePath);

            // Convert raw bytes to a string (may include raw data and not clean text)
            string rawContent = Encoding.ASCII.GetString(pdfBytes);

            // Extract potential readable content (basic text filtering)
            StringBuilder extractedText = new StringBuilder();
            bool insideTextObject = false;

            foreach (string line in rawContent.Split('\n'))
            {
                if (line.Contains("BT")) // Begin Text Object
                {
                    insideTextObject = true;
                }
                else if (line.Contains("ET")) // End Text Object
                {
                    insideTextObject = false;
                }
                else if (insideTextObject)
                {
                    // Extract and clean the text (may need regex or additional parsing)
                    extractedText.AppendLine(CleanText(line));
                }
            }

            return extractedText.ToString();
        }
        catch (Exception ex)
        {
            return $"Error while parsing PDF: {ex.Message}";
        }
    }

    public override string GetFileType()
    {
        return "PDF";
    }

    private string CleanText(string rawText)
    {
        // Basic cleaning for raw PDF data (you may need advanced logic here)
        StringBuilder cleanText = new StringBuilder();

        foreach (char c in rawText)
        {
            // Filter printable ASCII characters
            if (c >= 32 && c <= 126) // Printable ASCII range
            {
                cleanText.Append(c);
            }
        }

        return cleanText.ToString();
    }
}
