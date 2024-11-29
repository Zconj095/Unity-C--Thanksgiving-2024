# ConversationVectorizer

## Overview
The `ConversationVectorizer` class is responsible for transforming messages from conversations into numerical vectors using an embedding generator. This functionality is essential for various applications such as natural language processing, machine learning, and AI-driven conversation analysis. By converting textual messages into a format that machines can understand, it enables further processing and analysis of conversation data within the larger codebase.

## Variables

- **embeddingGenerator**: An instance of the `EmbeddingGenerator` class that is used to generate embeddings for the messages. This generator is crucial for converting the message content into a numerical format.

## Functions

- **ConversationVectorizer(EmbeddingGenerator generator)**: 
  - Constructor that initializes the `ConversationVectorizer` with a specific `EmbeddingGenerator`. This sets up the necessary tool to generate embeddings for messages.

- **float[] VectorizeMessage(ConversationMessage message)**: 
  - This method takes a `ConversationMessage` object as input and returns a float array representing the embedding of the message. It utilizes the `embeddingGenerator` to generate the embedding based on the hash code of the message content.

- **List<float[]> VectorizeConversation(ConversationHistory conversation)**: 
  - This method accepts a `ConversationHistory` object that contains multiple messages. It iterates through each message in the conversation, vectorizes them using the `VectorizeMessage` method, and collects the resulting embeddings into a list, which it then returns. This allows for the entire conversation to be represented as a series of numerical vectors.