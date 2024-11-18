using System;
using System.Collections.Generic;

public class Schedule
{
    public string Name { get; set; }
    public List<Play> Plays { get; set; }

    public Schedule(string name)
    {
        Name = name;
        Plays = new List<Play>();
    }

    public void AddPlay(Play play)
    {
        Plays.Add(play);
    }

    public override string ToString()
    {
        return $"Schedule: {Name}, Plays count: {Plays.Count}";
    }
}
