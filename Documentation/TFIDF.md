# TFIDF

## Overview
The `TFIDF` class implements the Term Frequency-Inverse Document Frequency (TF-IDF) algorithm for text analysis within a Unity environment. TF-IDF is a statistical measure used to evaluate the importance of a word in a document relative to a collection of documents (corpus). This class allows for the learning of word frequencies from a set of documents and computes the TF-IDF values for given text inputs. It interacts with a `BagOfWords` class (assumed to be defined elsewhere) to manage the vocabulary and its corresponding indices.

## Variables
- `bow`: An instance of the `BagOfWords` class that manages the vocabulary and word occurrences.
- `counts`: An array of integers that stores the count of occurrences for each word in the vocabulary across all documents.
- `numberOfDocuments`: An integer representing the total number of documents processed.
- `tf`: An instance of `TermFrequency` that defines the term frequency calculation method to be used.
- `idf`: An instance of `InverseDocumentFrequency` that defines the inverse document frequency calculation method to be used.
- `inverseDocumentFrequency`: An array of doubles that stores the computed IDF values for each word in the vocabulary.

### Properties
- `Counts`: Returns the counts array.
- `NumberOfDocuments`: Returns the number of documents processed.
- `NumberOfWords`: Returns the total number of words in the vocabulary.
- `IDFValues`: Returns the array of inverse document frequency values.
- `UpdateDictionary`: A boolean indicating whether the vocabulary should be updated.
- `TermFrequencyType`: Gets or sets the type of term frequency calculation.
- `InverseDocumentFrequencyType`: Gets or sets the type of inverse document frequency calculation.

## Functions
- `TFIDF()`: Constructor that initializes the `BagOfWords` and sets the `UpdateDictionary` property to true.
- `TFIDF(string[][] texts)`: Constructor that initializes the class and learns the vocabulary from the provided array of text documents.
- `void Learn(string[][] inputs)`: Processes the input documents to learn word frequencies and calculate the IDF values. It updates the vocabulary if `UpdateDictionary` is true.
- `private void CalculateIDF(int totalDocuments)`: Calculates the inverse document frequency values based on the counts of words across all documents. It supports different IDF calculation methods.
- `double[] Transform(string[] input)`: Transforms a single array of words into a TF-IDF vector based on the learned vocabulary and selected term frequency method.
- `double[][] Transform(string[][] inputs)`: Transforms an array of text documents into a 2D array of TF-IDF vectors, where each vector corresponds to a document.

## Enums
- `TermFrequency`: Defines the types of term frequency calculations available:
  - `Binary`: Each word is counted as either present (1) or absent (0).
  - `Default`: Counts the number of occurrences of each word.
  - `Log`: Applies logarithmic scaling to the term frequency.
  - `DoubleNormalization`: Normalizes the term frequency to a range between 0.5 and 1.

- `InverseDocumentFrequency`: Defines the types of inverse document frequency calculations available:
  - `Unary`: Default value (not used in calculations).
  - `Default`: Uses the standard IDF formula.
  - `Smooth`: Applies smoothing to the IDF calculation to avoid division by zero.
  - `Max`: Uses the maximum count of any word for IDF calculation.
  - `Probabilistic`: A probabilistic approach to calculate IDF.

This documentation provides a clear understanding of the `TFIDF` class, its purpose, and how it operates within the codebase, making it accessible to developers of all levels.