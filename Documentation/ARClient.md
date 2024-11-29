# ARClient

## Overview
The `ARClient` script is designed to establish a TCP connection with a server and send a simple message upon starting. It is a Unity MonoBehaviour that integrates networking functionality to enable communication between a client application and a server, specifically targeting a local server running on IP address 127.0.0.1 and port 7777. This script is essential for any application that requires real-time data exchange with a server, such as augmented reality applications where client-server interaction is necessary.

## Variables

- `client`: An instance of `TcpClient` that represents the connection to the server. This variable is responsible for managing the network connection and facilitating data transmission.

## Functions

- `void Start()`: This is a Unity lifecycle method that is called when the script instance is being loaded. In this method, a TCP connection is initiated to the server at IP address 127.0.0.1 on port 7777, and a debug message is logged to indicate that the connection has been established. It also calls the `SendMessage` method to send an initial message to the server.

- `void SendMessage(string message)`: This function takes a string message as an argument and sends it to the connected server. It retrieves the network stream from the `TcpClient`, converts the message into a byte array using ASCII encoding, and writes the byte data to the stream. After sending the message, it logs a debug message indicating the content of the sent message.