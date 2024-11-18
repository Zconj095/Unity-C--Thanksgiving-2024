using System;
using System.Collections.Generic;
public class Parameter
{
    public string Name { get; set; }
    public double Value { get; set; }

    // Constructor to initialize with a name and a value
    public Parameter(string name, double value)
    {
        Name = name;
        Value = value;
    }

    // Method to update the value of the parameter
    public void UpdateValue(double newValue)
    {
        Value = newValue;
    }

    // Method to get a string representation of the parameter
    public override string ToString()
    {
        return $"{Name}: {Value}";
    }
}
