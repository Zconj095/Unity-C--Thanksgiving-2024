using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;


public class ARServer : MonoBehaviour
{
    private TcpListener server;
    private TcpClient client;

    void Start()
    {
        server = new TcpListener(IPAddress.Any, 7777);
        server.Start();
        Debug.Log("Server started...");

        server.BeginAcceptTcpClient(new AsyncCallback(AcceptClient), null);
    }

    void AcceptClient(IAsyncResult result)
    {
        client = server.EndAcceptTcpClient(result);
        Debug.Log("Client connected!");

        // Start listening for messages
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReceiveMessage), buffer);
    }

    void ReceiveMessage(IAsyncResult result)
    {
        byte[] buffer = (byte[])result.AsyncState;
        string message = Encoding.ASCII.GetString(buffer).Trim('\0');
        Debug.Log($"Message received: {message}");

        // Echo the message back to clients
        NetworkStream stream = client.GetStream();
        byte[] response = Encoding.ASCII.GetBytes($"Server received: {message}");
        stream.Write(response, 0, response.Length);
    }
}
