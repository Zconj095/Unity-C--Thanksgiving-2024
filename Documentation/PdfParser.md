# PdfParser

## Overview
The `PdfParser` class is designed to read and extract text content from PDF files. It inherits from the `FileParser` base class, which likely provides a common interface for various file parsing implementations. The main function of this script is to parse a specified PDF file, extract any readable text from it, and return that text as a string. This functionality is essential for applications that require processing or analyzing the content of PDF documents.

## Variables
- `pdfBytes`: A byte array that holds the raw binary data read from the PDF file.
- `rawContent`: A string representation of the PDF's raw bytes, converted using ASCII encoding. This may contain both readable text and non-readable data.
- `extractedText`: A `StringBuilder` object used to accumulate the cleaned text extracted from the PDF.
- `insideTextObject`: A boolean flag indicating whether the parser is currently within a text object in the PDF structure.

## Functions
- `Parse(string filePath)`: 
  - This method takes the path of a PDF file as input, reads its contents, and attempts to extract readable text. It processes the raw byte data, identifies text objects, and cleans the text before returning it as a string. If an error occurs during parsing, it returns an error message.

- `GetFileType()`: 
  - This method returns a string indicating the type of file that this parser handles, which in this case is "PDF".

- `CleanText(string rawText)`: 
  - This private method is responsible for cleaning the raw text extracted from the PDF. It filters out non-printable ASCII characters and returns a string of cleaned text. The method currently uses a basic filtering approach, but it may require more advanced logic for comprehensive cleaning in practice.