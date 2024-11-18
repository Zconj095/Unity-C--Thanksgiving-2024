using System;
using System.Collections.Generic;

public class Z3Solver
{
    private List<string> constraints = new List<string>();
    private Stack<List<string>> history = new Stack<List<string>>();

    public Z3BoolRef Bool(string name)
    {
        return new Z3BoolRef(name);
    }

    public void Add(string condition)
    {
        constraints.Add(condition);
    }

    public bool Check()
    {
        // Simulate checking conditions; for simplicity, if there's no contradiction, return true
        // In a real solver, this would involve checking satisfiability of constraints.
        foreach (var constraint in constraints)
        {
            if (constraint.Contains("FALSE")) // Simplified contradiction check
            {
                return false;
            }
        }
        return true; // Always satisfiable in this simplified version
    }

    public void Push()
    {
        // Save the current state of the constraints
        history.Push(new List<string>(constraints));
    }

    public void Pop()
    {
        // Restore the previous state of the constraints
        if (history.Count > 0)
        {
            constraints = history.Pop();
        }
    }
}
