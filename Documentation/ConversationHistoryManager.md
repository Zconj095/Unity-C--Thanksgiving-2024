# ConversationHistoryManager

## Overview
The `ConversationHistoryManager` class is responsible for managing multiple conversations within a Unity application. It allows for the creation, retrieval, deletion, and listing of conversations, each identified by a unique conversation ID. This class acts as a central hub for conversation management, making it easy for other parts of the codebase to interact with conversation histories.

## Variables
- `conversations`: A private dictionary that maps conversation IDs (strings) to their corresponding `ConversationHistory` objects. This serves as the primary data structure for storing and managing conversations.

## Functions
- **ConversationHistoryManager()**
  - Constructor that initializes the `conversations` dictionary when a new instance of `ConversationHistoryManager` is created.

- **ConversationHistory CreateConversation(string conversationId)**
  - Creates a new conversation with the specified `conversationId`. If a conversation with that ID already exists, an exception is thrown. Returns the newly created `ConversationHistory` object.

- **ConversationHistory GetConversation(string conversationId)**
  - Retrieves the `ConversationHistory` object associated with the specified `conversationId`. If the conversation ID does not exist, an exception is thrown.

- **void DeleteConversation(string conversationId)**
  - Deletes the conversation associated with the specified `conversationId` from the dictionary. If the conversation ID does not exist, it simply does nothing.

- **List<string> ListConversations()**
  - Returns a list of all conversation IDs currently stored in the `conversations` dictionary. This provides an easy way to view all active conversations.