using System;
using System.Collections.Generic;
using System.Linq;

public class PubResult
{
    public DataBin Data { get; set; }
    public Dictionary<string, object> Metadata { get; set; }

    public PubResult(DataBin data, Dictionary<string, object> metadata)
    {
        Data = data;
        Metadata = metadata;
    }
}
