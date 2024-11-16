using System;
using System.Collections.Generic;
using System.Linq;

public class Options
{
    public double DefaultPrecision { get; set; } = 0.0;
    public Dictionary<string, object> BackendOptions { get; set; } = new Dictionary<string, object>();
    public Dictionary<string, object> RunOptions { get; set; } = new Dictionary<string, object>();
}