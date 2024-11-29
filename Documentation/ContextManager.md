# ContextManager

## Overview
The `ContextManager` class is responsible for managing a context buffer that holds `MemoryItem` objects. It ensures that the total number of tokens (derived from the content of these items) does not exceed a specified limit (`maxTokens`). This functionality is crucial in scenarios where memory or context management is essential, such as in AI applications or chatbots, where retaining relevant information while discarding older data is necessary for maintaining coherent interactions.

## Variables
- `maxTokens`: An integer that defines the maximum number of tokens that can be held in the context buffer. It acts as a cap to ensure that the buffer does not exceed a manageable size.
- `contextBuffer`: A list of `MemoryItem` objects that stores the current context. This buffer holds the most recent memories while adhering to the `maxTokens` limit.

## Functions
- **Constructor: `ContextManager(int maxTokens)`**
  - Initializes a new instance of the `ContextManager` class with a specified maximum number of tokens. It also initializes the `contextBuffer` as an empty list.

- **Method: `void AddToContext(MemoryItem memoryItem)`**
  - Adds a new `MemoryItem` to the context buffer. Before adding, it calculates the number of tokens in the new item and removes the oldest items from the buffer if adding the new item would exceed the `maxTokens` limit.

- **Method: `string GetContextAsString()`**
  - Returns a string representation of the current context by concatenating the content of all `MemoryItem` objects in the `contextBuffer`, separated by spaces.

- **Method: `int GetContextTokenCount()`**
  - Calculates and returns the total number of tokens in the `contextBuffer` by summing the token counts of each `MemoryItem`.

- **Method: `int[] Tokenize(string input)`**
  - Tokenizes the input string by splitting it into words and converting each word into its hash code. This method returns an array of integers representing the tokenized form of the input string.