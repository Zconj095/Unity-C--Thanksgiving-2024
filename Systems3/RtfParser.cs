using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class RtfParser : FileParser
{
    public override string Parse(string filePath)
    {
        try
        {
            string rtfContent = File.ReadAllText(filePath);

            // Extract plain text from RTF
            string plainText = ExtractPlainText(rtfContent);

            // Extract images from RTF
            List<string> imagePaths = ExtractImages(rtfContent, "ExtractedImages");

            // Combine plain text and image information
            StringBuilder result = new StringBuilder();
            result.AppendLine("=== Text Content ===");
            result.AppendLine(plainText);
            result.AppendLine("=== Images Extracted ===");
            foreach (var imagePath in imagePaths)
            {
                result.AppendLine(imagePath);
            }

            return result.ToString();
        }
        catch (Exception ex)
        {
            return $"Error while parsing RTF: {ex.Message}";
        }
    }

    public override string GetFileType()
    {
        return "RTF";
    }

    private string ExtractPlainText(string rtfContent)
    {
        // Use a regular expression to remove RTF control words and groups
        string plainText = Regex.Replace(rtfContent, @"\\[a-z]+\d* ?", string.Empty); // Remove control words
        plainText = Regex.Replace(plainText, @"{.*?}", string.Empty); // Remove groups
        plainText = Regex.Replace(plainText, @"[\r\n]+", "\n"); // Normalize newlines
        return plainText.Trim();
    }

    private List<string> ExtractImages(string rtfContent, string outputFolder)
    {
        List<string> imagePaths = new List<string>();

        // Ensure the output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Find all embedded image data in the RTF content
        MatchCollection matches = Regex.Matches(rtfContent, @"\\pict([\s\S]*?)\\bliptag[\d\s]+([a-fA-F0-9]+)");

        int imageCounter = 1;
        foreach (Match match in matches)
        {
            string hexData = match.Groups[2].Value;

            // Convert hex data to binary
            byte[] imageBytes = HexToBytes(hexData);

            // Save image as a file
            string imagePath = Path.Combine(outputFolder, $"image{imageCounter}.bmp");
            File.WriteAllBytes(imagePath, imageBytes);
            imagePaths.Add(imagePath);

            imageCounter++;
        }

        return imagePaths;
    }

    private byte[] HexToBytes(string hex)
    {
        int length = hex.Length;
        byte[] bytes = new byte[length / 2];
        for (int i = 0; i < length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }
}
