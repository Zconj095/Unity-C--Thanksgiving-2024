using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class CLIController
{
    private Dictionary<string, Action<string>> commands;

    public CLIController()
    {
        commands = new Dictionary<string, Action<string>>();
    }

    public void RegisterCommand(string command, Action<string> action)
    {
        commands[command] = action;
    }

    public void ExecuteCommand(string input)
    {
        string[] parts = input.Split(' ');
        if (commands.ContainsKey(parts[0]))
        {
            commands[parts[0]](string.Join(" ", parts.Skip(1)));
        }
        else
        {
            Console.WriteLine("Unknown command.");
        }
    }
}
