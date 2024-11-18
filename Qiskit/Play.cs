using System;
using System.Collections.Generic;

public class Play
{
    public string Name { get; set; }
    public Pulse Pulse { get; set; }
    public int Channel { get; set; }

    public Play(string name, Pulse pulse, int channel)
    {
        Name = name;
        Pulse = pulse;
        Channel = channel;
    }

    // Renamed method to "Execute" to avoid conflict with the class name
    public void Execute(Pulse newPulse, int newChannel)
    {
        // Logic to play the pulse on the specified channel
        Pulse = newPulse;
        Channel = newChannel;
    }
}
