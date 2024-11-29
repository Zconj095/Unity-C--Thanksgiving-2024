# ConversationHistory

## Overview
The `ConversationHistory` class is designed to manage and store the history of messages in a conversation. It is part of a larger codebase that likely deals with chat or messaging functionalities, possibly within a game or application built using Unity. This class allows for the creation of a conversation identified by a unique `ConversationId`, and it maintains a list of messages exchanged during that conversation.

## Variables
- **ConversationId**: A string that uniquely identifies a conversation. It is set when the `ConversationHistory` object is instantiated and cannot be modified afterward.
- **Messages**: A list that holds instances of `ConversationMessage`. This list stores all the messages sent during the conversation.

## Functions
- **ConversationHistory(string conversationId)**: Constructor that initializes a new instance of the `ConversationHistory` class. It takes a string parameter `conversationId` to set the `ConversationId` and initializes the `Messages` list as empty.
  
- **void AddMessage(string sender, string message)**: This method allows adding a new message to the conversation. It takes two string parameters, `sender` (the name or identifier of the message sender) and `message` (the content of the message). It creates a new `ConversationMessage` object with these parameters and adds it to the `Messages` list.