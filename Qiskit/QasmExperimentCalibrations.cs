using System;
using System.Collections.Generic;

public class QasmExperimentCalibrations
{
    public List<GateCalibration> Calibrations { get; set; }

    public QasmExperimentCalibrations(List<GateCalibration> calibrations)
    {
        Calibrations = calibrations;
    }
}
