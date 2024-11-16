using System;
using System.Collections.Generic;
using System.Linq;

public class PulseLibraryEntry
{
    public string Name;
    public List<Tuple<double, double>> Samples; // Replacing complex numbers with tuples (real, imaginary)

    public PulseLibraryEntry(string name, List<Tuple<double, double>> samples)
    {
        Name = name;
        Samples = samples;
    }
}