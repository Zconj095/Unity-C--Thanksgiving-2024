# OdtParser

## Overview
The `OdtParser` class is designed to parse ODT (Open Document Text) files, which are essentially ZIP archives containing XML files. This class extends the `FileParser` base class and implements the functionality to extract and read the text content from the `content.xml` file within the ODT archive. It fits into a larger codebase that likely requires different file types to be parsed, providing a specialized implementation for ODT files.

## Variables
- **filePath**: A string representing the path to the ODT file that needs to be parsed.
- **zip**: An instance of `ZipArchive` that represents the opened ODT file as a ZIP archive.
- **contentXmlEntry**: A `ZipArchiveEntry` object that references the `content.xml` file within the ODT archive.
- **stream**: A `Stream` object used to read the contents of the `content.xml` file.
- **xmlDoc**: An `XmlDocument` object that loads the XML structure from the `content.xml` file.
- **content**: A `StringBuilder` object that accumulates the extracted text content from the XML nodes.
- **textNodes**: An `XmlNodeList` that contains all the paragraph nodes (`text:p`) found in the XML document.
- **node**: An `XmlNode` used in the foreach loop to iterate over the paragraph nodes.

## Functions
- **Parse(string filePath)**: This method overrides the base class method to implement the parsing logic for ODT files. It opens the specified ODT file as a ZIP archive, extracts the `content.xml` file, and reads the text content from it. If the `content.xml` file is not found, or if any other error occurs during parsing, it returns an error message.

- **GetFileType()**: This method overrides the base class method to return the string "ODT", indicating the type of file that this parser is designed to handle.