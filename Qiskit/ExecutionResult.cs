using System;
using System.Collections.Generic;

public class ExecutionResult
{
    public string Status { get; set; }

    public ExecutionResult(string status)
    {
        Status = status;
    }
}
