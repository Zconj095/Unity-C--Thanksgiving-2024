using System;
using System.Collections.Generic;
using System.Linq;

public class PulseSequence
{
    public List<PulseQobjInstruction> SequenceSteps { get; set; }

    public PulseSequence(List<PulseQobjInstruction> sequenceSteps)
    {
        SequenceSteps = sequenceSteps ?? new List<PulseQobjInstruction>();
    }

    // Additional methods for PulseSequence can be added as needed
}
