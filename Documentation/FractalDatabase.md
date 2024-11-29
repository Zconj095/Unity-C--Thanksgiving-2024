# FractalDatabase

## Overview
The `FractalDatabase` class is designed to manage and analyze a collection of fractal patterns represented as arrays of floating-point numbers (embeddings). It allows the addition of new patterns and computes a distortion score based on the similarity of a given embedding to the stored patterns. This class fits within a larger codebase that likely deals with fractal generation or manipulation, providing functionality to assess how closely a new pattern resembles existing ones.

## Variables

- `fractalPatterns`: A private list that stores arrays of floats. Each array represents a fractal pattern (embedding) that can be added to the database and used for distortion calculations.

## Functions

- `FractalDatabase()`: Constructor for the `FractalDatabase` class. It initializes the `fractalPatterns` list to hold the fractal patterns.

- `AddPattern(float[] embedding)`: This public method accepts a float array as an argument and adds it to the `fractalPatterns` list. It allows users to expand the database with new fractal patterns.

- `ComputeFractalDistortion(float[] embedding)`: This public method calculates a distortion score for a provided embedding. It iterates through all stored fractal patterns, computes the cosine similarity between the provided embedding and each pattern using the `EmbeddingSimilarity.ComputeCosineSimilarity` method, and accumulates the similarity scores. The final distortion score is normalized to provide a measure of how different the embedding is from the patterns in the database, where a lower score indicates greater similarity.