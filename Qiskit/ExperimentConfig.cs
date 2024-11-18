using System;
using System.Collections.Generic;
using System.Linq;

public class ExperimentConfig
{
    public int NQubits { get; set; }
    public int MemorySlots { get; set; }
    public QasmExperimentCalibrations Calibrations { get; set; }
}
