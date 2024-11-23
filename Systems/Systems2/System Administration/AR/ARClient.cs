using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ARClient : MonoBehaviour
{
    private TcpClient client;

    void Start()
    {
        client = new TcpClient("127.0.0.1", 7777);
        Debug.Log("Connected to server...");

        SendMessage("Hello from Client!");
    }

    void SendMessage(string message)
    {
        NetworkStream stream = client.GetStream();
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Debug.Log($"Message sent: {message}");
    }
}
