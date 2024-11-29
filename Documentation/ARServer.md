# ARServer

## Overview
The `ARServer` script is a Unity MonoBehaviour that implements a simple TCP server. It listens for incoming client connections on port 7777 and handles messages sent from connected clients. The main function of this script is to accept client connections, receive messages, and echo back responses to the clients. This functionality is crucial for applications that require real-time communication, such as augmented reality applications where data exchange between the server and clients is necessary.

## Variables

- `TcpListener server`: An instance of `TcpListener` that listens for incoming TCP connections on the specified port.
- `TcpClient client`: An instance of `TcpClient` that represents the connected client.

## Functions

- `void Start()`: This is a Unity lifecycle method that initializes the TCP server when the script starts. It sets up the `TcpListener` to listen for connections on all network interfaces at port 7777 and begins accepting client connections asynchronously.

- `void AcceptClient(IAsyncResult result)`: This method is called when a client connection is accepted. It retrieves the connected client from the `TcpListener` and logs a message indicating a successful connection. It also prepares to listen for messages from the client by starting an asynchronous read operation on the network stream.

- `void ReceiveMessage(IAsyncResult result)`: This method is invoked when a message is received from the client. It processes the incoming message by converting the byte array to a string, logs the received message, and echoes a response back to the client, confirming receipt of the message.