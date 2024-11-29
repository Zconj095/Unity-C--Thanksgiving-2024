# Tokenizer

## Overview
The `Tokenizer` class is designed to convert a string of text into a sequence of integer tokens and vice versa. This functionality is essential for various applications in natural language processing, game development, or any context where text needs to be represented in a more manageable form. The class maintains a vocabulary that maps unique words to integer indices, allowing for efficient tokenization and detokenization processes.

## Variables

- `vocabulary`: A `Dictionary<string, int>` that stores the mapping of unique words (as keys) to their corresponding integer indices (as values). This serves as the vocabulary for the tokenizer.
- `vocabIndex`: An `int` that keeps track of the next available index to assign to a new word in the vocabulary.

## Functions

- `public int[] Tokenize(string input)`: 
  - This method takes a string input, splits it into words, and converts each word into its corresponding token (integer index). If a word is not already in the vocabulary, it adds it with a new index. The method returns an array of integers representing the tokens of the input string.

- `public string Detokenize(int[] tokens)`:
  - This method takes an array of integer tokens and converts them back into the original string. It first creates a reverse mapping from indices back to words using the existing vocabulary. It then constructs the result string by concatenating the corresponding words for each token, returning the final string after trimming any extra spaces.