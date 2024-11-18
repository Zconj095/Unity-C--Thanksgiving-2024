using System;

public class ScheduleBlock
{
    public string Name { get; set; }

    public ScheduleBlock(string name)
    {
        Name = name;
    }

    // Example method to convert a schedule block to a string
    public override string ToString()
    {
        return $"Schedule Block: {Name}";
    }
}
