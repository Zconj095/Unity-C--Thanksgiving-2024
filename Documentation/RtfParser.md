# RtfParser

## Overview
The `RtfParser` class is designed to read and extract content from RTF (Rich Text Format) files. It inherits from the `FileParser` base class, providing a specific implementation for parsing RTF files. The main function of this script is to extract plain text and embedded images from an RTF file, then compile this information into a formatted string. This functionality fits into a larger codebase that likely includes various file parsers for different formats, allowing for consistent handling and extraction of content from multiple file types.

## Variables
- `rtfContent`: A string that holds the raw content of the RTF file read from the specified file path.
- `plainText`: A string that contains the plain text extracted from the RTF content, free of RTF control words and formatting.
- `imagePaths`: A list of strings that stores the paths to the images extracted from the RTF content.
- `result`: A `StringBuilder` object used to construct the final output string that includes both the plain text and the paths of the extracted images.
- `matches`: A `MatchCollection` that holds the results of the regular expression search for embedded image data within the RTF content.
- `imageCounter`: An integer used to uniquely name each extracted image file.

## Functions
- `Parse(string filePath)`: 
  - This method overrides the base class method to read the content of the RTF file specified by `filePath`. It extracts plain text and images, then combines them into a formatted string for output. If an error occurs during parsing, it returns an error message.
  
- `GetFileType()`: 
  - This method overrides the base class method to return the string "RTF", indicating the type of file this parser is designed to handle.
  
- `ExtractPlainText(string rtfContent)`: 
  - A private method that processes the RTF content to remove control words and groups using regular expressions, normalizing the newlines, and returning the cleaned plain text.
  
- `ExtractImages(string rtfContent, string outputFolder)`: 
  - A private method that identifies and extracts embedded images from the RTF content. It ensures that the specified output folder exists, converts the hex image data to binary, saves the images as BMP files, and returns a list of the paths to the saved images.
  
- `HexToBytes(string hex)`: 
  - A private method that converts a hexadecimal string representation of binary data into a byte array, which is used to save the extracted images.