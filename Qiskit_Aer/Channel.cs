using System;

public class Channel
{
    public string ChannelName { get; set; }
    public double Frequency { get; set; }

    public Channel(string channelName, double frequency)
    {
        ChannelName = channelName;
        Frequency = frequency;
    }

    // Example method to send data over the channel
    public void SendData(string data)
    {
        Console.WriteLine($"Sending data: {data} over {ChannelName} at frequency {Frequency}Hz");
    }

    // Example method to receive data
    public string ReceiveData()
    {
        Console.WriteLine($"Receiving data on {ChannelName} at frequency {Frequency}Hz");
        return "Received data"; // Placeholder for actual received data
    }
}
