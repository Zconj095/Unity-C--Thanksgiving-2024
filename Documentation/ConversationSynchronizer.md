# ConversationSynchronizer

## Overview
The `ConversationSynchronizer` class is responsible for managing conversations within a system. It interacts with a `ConversationHistoryManager`, which is assumed to handle the storage and retrieval of conversation data. This class provides functionality to initiate new conversations, synchronize messages between participants, and retrieve a list of active conversations. It fits into the larger codebase by serving as a bridge between user interactions and the underlying conversation management system.

## Variables
- **historyManager**: An instance of the `ConversationHistoryManager` class that handles the creation, retrieval, and management of conversation histories.

## Functions
- **ConversationSynchronizer(ConversationHistoryManager manager)**: Constructor that initializes the `ConversationSynchronizer` with a given `ConversationHistoryManager` instance.
  
- **string StartNewConversation()**: Generates a new unique conversation ID, creates a new conversation in the history manager, and returns the conversation ID.

- **void SynchronizeMessage(string conversationId, string sender, string message)**: Takes a conversation ID, sender's name, and message text, retrieves the corresponding conversation from the history manager, and adds the new message to that conversation.

- **List<string> GetActiveConversations()**: Returns a list of active conversation IDs by calling the `ListConversations` method on the `historyManager`.