using System;
using System.Collections.Generic;

public class ControlledGate
{
    public string Name { get; private set; }
    public int NumQubits { get; private set; }
    public List<double> Parameters { get; private set; }
    public int NumCtrlQubits { get; private set; }
    public string CtrlState { get; private set; }
    public ControlledGate BaseGate { get; private set; }

    public ControlledGate(string name, int numQubits, List<double> parameters, ControlledGate baseGate, int numCtrlQubits, string ctrlState = null)
    {
        Name = name;
        NumQubits = numQubits;
        Parameters = parameters ?? new List<double>();
        BaseGate = baseGate;
        NumCtrlQubits = numCtrlQubits;
        CtrlState = ctrlState;
    }
}
