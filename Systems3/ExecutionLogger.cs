using System.Collections.Generic;
using System;

public class ExecutionLogger
{
    private List<string> logs;

    public ExecutionLogger()
    {
        logs = new List<string>();
    }

    public void Log(string message)
    {
        logs.Add(message);
        Console.WriteLine(message);
    }

    public void LogError(string error)
    {
        logs.Add($"ERROR: {error}");
        Console.WriteLine($"ERROR: {error}");
    }

    public List<string> GetLogs()
    {
        return logs;
    }
}
