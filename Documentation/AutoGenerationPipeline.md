# AutoGenerationPipeline

## Overview
The `AutoGenerationPipeline` class is responsible for generating output based on user input and associated memory embeddings. It integrates with two key components: `MemoryRetrievalSystem2`, which retrieves relevant memories based on input embeddings, and `OutputGenerator`, which generates the final output using the retrieved context. This class acts as a bridge between memory retrieval and output generation, facilitating a seamless flow of data through the system.

## Variables
- `memoryRetrievalSystem`: An instance of `MemoryRetrievalSystem2` used to access and retrieve relevant memories based on input embeddings.
- `outputGenerator`: An instance of `OutputGenerator` responsible for generating the final output based on the input and context embeddings.

## Functions
- **Constructor: `AutoGenerationPipeline(MemoryRetrievalSystem2 memoryRetrievalSystem, OutputGenerator outputGenerator)`**
  - Initializes a new instance of the `AutoGenerationPipeline` class with the specified memory retrieval system and output generator.

- **Method: `Generate(string userInput, float[] inputEmbedding)`**
  - This method takes user input and input embeddings as parameters. It performs the following steps:
    1. Retrieves relevant memories from the `memoryRetrievalSystem` using the provided `inputEmbedding`.
    2. Converts the retrieved memories into context embeddings.
    3. Generates the output by calling the `GenerateOutput` method of the `outputGenerator`, passing both the `inputEmbedding` and the list of context embeddings. The generated output is then returned as a string.