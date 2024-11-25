using System;
using System.Collections.Generic;

public class FunctionRegistry
{
    private Dictionary<string, Func<object[], object>> functions;

    public FunctionRegistry()
    {
        functions = new Dictionary<string, Func<object[], object>>();
    }

    public void RegisterFunction(string name, Func<object[], object> function)
    {
        functions[name] = function;
    }

    public object ExecuteFunction(string name, params object[] args)
    {
        if (functions.ContainsKey(name))
        {
            return functions[name](args);
        }

        throw new Exception($"Function {name} not found.");
    }

    public bool HasFunction(string name)
    {
        return functions.ContainsKey(name);
    }
}
