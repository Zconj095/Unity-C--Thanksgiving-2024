# LLMManager

## Overview
The `LLMManager` script is responsible for managing the connection between a Unity application and a backend server using TCP. It establishes a connection to the server, sends prompts, and handles responses asynchronously. This script is essential for enabling communication with the backend, which is likely responsible for processing language models or similar tasks. It ensures that the application can send and receive data reliably, even in the face of network issues.

## Variables
- `TcpClient client`: Represents the TCP client used to connect to the backend server.
- `NetworkStream stream`: The stream used for reading from and writing to the backend server.
- `const string ServerIP`: The IP address of the backend server. Default is set to "127.0.0.1".
- `const int ServerPort`: The port number for the server connection, defaulting to 11434.
- `const int RetryAttempts`: The number of attempts to retry sending data in case of failure.
- `bool isReconnecting`: A flag to prevent multiple reconnection attempts at the same time.
- `bool isDestroyed`: A flag that indicates whether the Unity object has been destroyed.

## Functions
- `async void Start()`: This is a Unity lifecycle method that starts the connection process to the backend as soon as the script is initialized.

- `private async Task ConnectToBackendAsync()`: This function attempts to establish a connection to the backend server. It logs the connection attempt and handles any exceptions that may occur during the connection process.

- `public async Task<string> SendPromptAsync(string prompt)`: This method sends a prompt to the backend server and waits for a response. It checks if the manager has been destroyed or if the stream is ready before attempting to send data. If the send operation fails, it retries up to a specified number of attempts, handling various exceptions gracefully.

- `private async Task ReconnectAsync()`: This function attempts to reconnect to the backend server if the connection is lost. It ensures that multiple reconnection attempts do not occur simultaneously and logs the outcome of the reconnection process.

- `void OnDestroy()`: This Unity lifecycle method is called when the Unity object is destroyed. It sets the `isDestroyed` flag, logs the destruction event, and ensures that any open connections are closed properly to avoid resource leaks.