# ConversationMessage

## Overview
The `ConversationMessage` class is designed to represent a message in a conversation, typically between a user and an AI. This class encapsulates the sender's identity, the content of the message, and the time the message was created. It is likely part of a larger codebase that handles conversations, such as a chat application or a dialogue system in a game. By providing a structured way to manage messages, this class facilitates the organization and retrieval of conversation data.

## Variables
- `Sender`: A string that indicates who sent the message. It can either be "User" or "AI".
- `Message`: A string that contains the content of the message.
- `Timestamp`: A `DateTime` object that records the exact time when the message was created, set to the current UTC time.

## Functions
- **ConversationMessage(string sender, string message)**: This is the constructor for the `ConversationMessage` class. It takes two parameters: `sender`, which specifies who is sending the message, and `message`, which is the content of the message. Upon instantiation, it initializes the `Sender`, `Message`, and sets the `Timestamp` to the current UTC time.