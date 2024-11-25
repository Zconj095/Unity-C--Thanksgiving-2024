using System.Net.Sockets;
using System.Text;
using UnityEngine;
public class LLMManager : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        ConnectToBackend("127.0.0.1", 5000);
    }

    void ConnectToBackend(string ip, int port)
    {
        client = new TcpClient(ip, port);
        stream = client.GetStream();
    }

    public string SendPrompt(string prompt)
    {
        if (stream == null) return null;

        byte[] data = Encoding.UTF8.GetBytes(prompt);
        stream.Write(data, 0, data.Length);

        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(buffer, 0, bytesRead);
    }

    void OnDestroy()
    {
        stream?.Close();
        client?.Close();
    }
}
