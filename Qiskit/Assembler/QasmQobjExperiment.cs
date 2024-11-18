using System;
using System.Collections.Generic;
public class QasmQobjExperiment
{
    public ExperimentHeader Header { get; set; }
    public List<Operation> Instructions { get; set; }
    public ExperimentConfig Config { get; set; }

    // Other properties depending on the actual QasmQobjExperiment structure
}
