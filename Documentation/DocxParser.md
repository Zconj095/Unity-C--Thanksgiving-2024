# DocxParser

## Overview
The `DocxParser` class is designed to parse DOCX files, which are essentially ZIP archives containing various files, including XML documents that represent the text content and images. This class extends the `FileParser` base class and provides methods to extract both text and image data from a specified DOCX file. The extracted content is then formatted into a string that includes both the text and paths to the extracted images. This functionality is crucial for applications that need to read and process DOCX file contents without requiring a full-fledged word processor.

## Variables
- `zip`: An instance of `ZipArchive`, used to read the contents of the DOCX file as a ZIP archive.
- `textContent`: A string that holds the extracted text content from the DOCX file.
- `imagePaths`: A list of strings that stores the paths of the extracted images.
- `result`: A `StringBuilder` that accumulates the final output string, combining text content and image paths.

## Functions
- `Parse(string filePath)`: This method takes the path of a DOCX file as input, opens it as a ZIP archive, extracts the text and images, and returns a formatted string containing the text and the paths of the extracted images. If an error occurs during parsing, it returns an error message.

- `GetFileType()`: This method returns a string indicating the type of file being parsed, which in this case is "DOCX".

- `ExtractText(ZipArchive zip)`: This private method extracts the text content from the DOCX file by locating the `document.xml` entry within the ZIP archive. It reads the XML content, retrieves all text nodes, and concatenates them into a single string.

- `ExtractImages(ZipArchive zip, string outputFolder)`: This private method extracts images from the DOCX file. It checks each entry in the ZIP archive to see if it is an image, saves it to a specified output folder, and returns a list of paths to the extracted images.

- `IsImageFile(string fileName)`: This private helper method checks if a given file name corresponds to a supported image format (JPEG, PNG, GIF, BMP) based on its file extension. It returns a boolean indicating whether the file is an image.