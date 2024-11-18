using System;
using System.Collections.Generic;
using System.Linq;

public class ExperimentHeader
{
    public List<Tuple<string, int>> QubitLabels { get; set; }
    public int NQubits { get; set; }
    public List<Tuple<string, int>> QregSizes { get; set; }
    public List<Tuple<string, int>> ClbitLabels { get; set; }
    public int MemorySlots { get; set; }
    public List<Tuple<string, int>> CregSizes { get; set; }
    public string Name { get; set; }
    public double GlobalPhase { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
}

