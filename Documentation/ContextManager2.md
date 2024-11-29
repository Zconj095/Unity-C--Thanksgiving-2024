# ContextManager2

## Overview
The `ContextManager2` script is designed to manage contextual data within a Unity application. It provides a simple way to store and retrieve key-value pairs, allowing different parts of the application to share and access contextual information. This script fits into the broader codebase by acting as a centralized manager for context, making it easier to maintain and access shared state across different components.

## Variables
- **context**: A `Dictionary<string, object>` that stores the contextual data. The keys are strings representing the context identifiers, and the values are objects that hold the associated data.

## Functions
- **ContextManager2()**: Constructor that initializes the `context` dictionary when an instance of `ContextManager2` is created.

- **SetContext(string key, object value)**: This method allows you to set a value in the context dictionary. It takes a string `key` that identifies the context and an `object` `value` that you want to associate with that key.

- **GetContext(string key)**: This method retrieves the value associated with the specified key from the context dictionary. If the key exists, it returns the corresponding value; otherwise, it returns `null`.