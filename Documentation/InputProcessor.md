# InputProcessor

## Overview
The `InputProcessor` class is responsible for transforming a string input into a numerical representation suitable for further processing, such as machine learning or natural language processing tasks. It achieves this by utilizing two main components: a `Tokenizer`, which breaks down the input string into tokens, and an `EmbeddingGenerator`, which converts those tokens into a numerical embedding. This class fits within a larger codebase that likely involves natural language understanding or processing, allowing other components to work with numerical data derived from textual input.

## Variables

- `tokenizer`: An instance of the `Tokenizer` class used to convert the input string into an array of tokens.
- `embeddingGenerator`: An instance of the `EmbeddingGenerator` class responsible for generating numerical embeddings from the tokens.

## Functions

- **InputProcessor(Tokenizer tokenizer, EmbeddingGenerator embeddingGenerator)**: 
  - Constructor that initializes the `InputProcessor` with instances of `Tokenizer` and `EmbeddingGenerator`. This sets up the necessary components for processing input.

- **float[] ProcessInput(string input)**: 
  - This method takes a string input, tokenizes it using the `tokenizer`, and then generates an embedding for each token using the `embeddingGenerator`. It returns an array of floats that represents the complete numerical embedding of the input string. The method performs the following steps:
    1. Calls the `Tokenize` method of the `tokenizer` to convert the input string into an array of tokens.
    2. Iterates over the array of tokens, generating embeddings for each token and adding them to a list.
    3. Converts the list of embeddings into an array and returns it.