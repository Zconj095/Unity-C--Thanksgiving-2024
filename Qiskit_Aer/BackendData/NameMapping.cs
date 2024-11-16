using System.Collections.Generic;

public class NameMapping
{
    private Dictionary<string, ControlledGate> mapping;

    public NameMapping()
    {
        mapping = new Dictionary<string, ControlledGate>
        {
            { "mcsx", new MCSXGate(0) },
            { "mcy", new MCYGate(0) },
            { "mcz", new MCZGate(0) },
            { "mcrx", new MCRXGate(0, 0) },
            { "mcry", new MCRYGate(0, 0) },
            { "mcrz", new MCRZGate(0, 0) },
            { "mcr", new MCRGate(0, 0, 0) },
            { "mcu2", new MCU2Gate(0, 0, 0) },
            { "mcu3", new MCU3Gate(0, 0, 0, 0) },
            { "mcswap", new MCSwapGate(0) }
        };
    }

    public ControlledGate GetGate(string name)
    {
        if (mapping.ContainsKey(name))
        {
            return mapping[name];
        }

        throw new KeyNotFoundException($"Gate with name '{name}' not found.");
    }
}
