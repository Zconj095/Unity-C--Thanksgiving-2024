# ImageProcessor

## Overview
The `ImageProcessor` class is designed to handle the conversion of image data into a fixed-size embedding, which is a numerical representation of the image. This functionality is essential for applications that require image analysis, such as machine learning models that process visual data. The main function of this script, `ConvertImageToEmbedding`, takes raw image data in byte format, processes it, and returns an array of floats that represent the features of the image.

## Variables
- **texture**: A `Texture2D` object that temporarily holds the original image after loading the image data.
- **resizedTexture**: A `Texture2D` object that holds the resized version of the original image, set to a fixed resolution of 32x32 pixels.
- **embedding**: An array of floats that contains the normalized pixel features extracted from the resized image.
- **rt**: A `RenderTexture` used for rendering the original texture to a new size.
- **resized**: A `Texture2D` object that stores the resized image after reading pixels from the `RenderTexture`.
- **pixels**: An array of `Color` objects that contains the pixel data from the `Texture2D`.
- **features**: An array of floats that holds the normalized grayscale pixel values.

## Functions
- **ConvertImageToEmbedding(byte[] imageData)**: 
  - This public method takes an array of bytes representing image data as input. It loads the image into a `Texture2D`, resizes it to a fixed resolution of 32x32 pixels, and then extracts normalized pixel features to return them as an array of floats.
  
- **ResizeTexture(Texture2D original, int width, int height)**: 
  - This private method accepts a `Texture2D` object and the desired width and height for resizing. It uses a `RenderTexture` to perform the resizing operation and returns a new `Texture2D` object that represents the resized image.

- **ExtractNormalizedPixelFeatures(Texture2D texture)**: 
  - This private method takes a `Texture2D` as input and extracts its pixel data. It converts the colors of the pixels to grayscale, normalizes them to a range of [0, 1], and returns an array of floats containing these normalized pixel values.