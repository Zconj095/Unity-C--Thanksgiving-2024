using System;
using System.Collections.Generic;

public class FunctionBuilder
{
    public static void AddFunctionFromInstruction(string instruction, FunctionRegistry registry)
    {
        if (instruction.Contains("adds two numbers"))
        {
            registry.RegisterFunction("add", args =>
            {
                if (args.Length != 2) throw new Exception("Add function expects 2 arguments.");
                return (int)args[0] + (int)args[1];
            });
        }
        else if (instruction.Contains("calculates factorial"))
        {
            registry.RegisterFunction("factorial", args =>
            {
                if (args.Length != 1) throw new Exception("Factorial function expects 1 argument.");
                int n = (int)args[0];
                int result = 1;
                for (int i = 1; i <= n; i++) result *= i;
                return result;
            });
        }
    }
}
