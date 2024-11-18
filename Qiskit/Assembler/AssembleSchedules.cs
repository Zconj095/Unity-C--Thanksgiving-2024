using System;
using System.Collections.Generic;
using System.Linq;

public class Qobj
{
    public class PulseQobj
    {
        public List<PulseQobjExperiment> Experiments { get; set; }
        public int QobjId { get; set; }
        public QobjHeader Header { get; set; }
        public PulseQobjConfig Config { get; set; }

        public PulseQobj(List<PulseQobjExperiment> experiments, int qobjId, QobjHeader header, PulseQobjConfig config)
        {
            Experiments = experiments;
            QobjId = qobjId;
            Header = header;
            Config = config;
        }
    }

    public class QobjHeader
    {
        public int MemorySlots { get; set; }
        public string Name { get; set; }
        public Dictionary<string, object> Metadata { get; set; }

        public QobjHeader(int memorySlots, string name, Dictionary<string, object> metadata)
        {
            MemorySlots = memorySlots;
            Name = name;
            Metadata = metadata;
        }
    }

    public class PulseQobjExperiment
    {
        public QobjExperimentHeader Header { get; set; }
        public List<PulseQobjInstruction> Instructions { get; set; }
        public QobjExperimentConfig Config { get; set; }

        public PulseQobjExperiment(QobjExperimentHeader header, List<PulseQobjInstruction> instructions)
        {
            Header = header;
            Instructions = instructions;
        }
    }

    public class PulseQobjInstruction
    {
        public string Name { get; set; }
        public List<int> Qubits { get; set; }
        public List<int> Memory { get; set; }
        public int Conditional { get; set; }

        public PulseQobjInstruction(string name)
        {
            Name = name;
            Qubits = new List<int>();
            Memory = new List<int>();
        }
    }

    public class QobjExperimentHeader
    {
        public int MemorySlots { get; set; }
        public string Name { get; set; }
        public Dictionary<string, object> Metadata { get; set; }

        public QobjExperimentHeader(int memorySlots, string name, Dictionary<string, object> metadata)
        {
            MemorySlots = memorySlots;
            Name = name;
            Metadata = metadata;
        }
    }

    public class QobjExperimentConfig
    {
        public Dictionary<string, object> Config { get; set; }

        public QobjExperimentConfig(Dictionary<string, object> config)
        {
            Config = config;
        }
    }

    public class PulseQobjConfig
    {
        public Dictionary<string, object> Config { get; set; }

        public PulseQobjConfig(Dictionary<string, object> config)
        {
            Config = config;
        }
    }
}

public class AssembleSchedules
{
    public static Qobj.PulseQobj AssembleQobjSchedules(
        List<object> schedules, // Equivalent to List[Union[ScheduleBlock, ScheduleComponent, Tuple[int, ScheduleComponent]]]
        int qobjId,
        Qobj.QobjHeader qobjHeader,
        RunConfig runConfig
    )
    {
        if (!runConfig.HasProperty("qubit_lo_freq"))
        {
            throw new QiskitError("qubit_lo_freq must be supplied.");
        }
        if (!runConfig.HasProperty("meas_lo_freq"))
        {
            throw new QiskitError("meas_lo_freq must be supplied.");
        }

        var loConverter = new Converters.LoConfigConverter(Qobj.PulseQobjExperimentConfig, runConfig.ToDict());
        var (experiments, experimentConfig) = AssembleExperiments(schedules, loConverter, runConfig);
        var qobjConfig = AssembleConfig(loConverter, experimentConfig, runConfig);

        return new Qobj.PulseQobj(
            experiments: experiments,
            qobjId: qobjId,
            header: qobjHeader,
            config: qobjConfig
        );
    }

    private static (List<Qobj.PulseQobjExperiment>, Dictionary<string, object>) AssembleExperiments(
        List<object> schedules,
        Converters.LoConfigConverter loConverter,
        RunConfig runConfig
    )
    {
        var freqConfigs = runConfig.ScheduleLos.Select(loDict => loConverter(loDict)).ToList();

        if (schedules.Count > 1 && freqConfigs.Count != 0 && freqConfigs.Count != 1 && freqConfigs.Count != schedules.Count)
        {
            throw new QiskitError(
                "Invalid 'schedule_los' setting specified. If specified, it should either have a single entry " +
                "to apply the same LOs for each schedule or have length equal to the number of schedules."
            );
        }

        var instructionConverter = runConfig.HasProperty("instruction_converter")
            ? runConfig.InstructionConverter
            : new Converters.InstructionToQobjConverter(Qobj.PulseQobjInstruction, runConfig.ToDict());

        var formattedSchedules = schedules.Select(sched => Transforms.TargetQobjTransform(sched)).ToList();
        var compressedSchedules = Transforms.CompressPulses(formattedSchedules);

        var userPulseLib = new Dictionary<string, List<Tuple<double, double>>>();
        var experiments = new List<Qobj.PulseQobjExperiment>();

        foreach (var (idx, sched) in compressedSchedules.Select((sched, idx) => (idx, sched)))
        {
            var (qobjInstructions, maxMemorySlot) = AssembleInstructions(sched, instructionConverter, runConfig, userPulseLib);

            var metadata = sched.Metadata ?? new Dictionary<string, object>();
            var qobjExperimentHeader = new Qobj.QobjExperimentHeader
            {
                MemorySlots = maxMemorySlot + 1, // Memory slots are 0 indexed
                Name = sched.Name ?? $"Experiment-{idx}",
                Metadata = metadata
            };

            var experiment = new Qobj.PulseQobjExperiment(
                header: qobjExperimentHeader,
                instructions: qobjInstructions
            );
            if (freqConfigs.Any())
            {
                var freqIdx = freqConfigs.Count == 1 ? 0 : idx;
                experiment.Config = freqConfigs[freqIdx];
            }

            experiments.Add(experiment);
        }

        if (freqConfigs.Any() && experiments.Count == 1)
        {
            var experiment = experiments[0];
            experiments.Clear();
            foreach (var freqConfig in freqConfigs)
            {
                experiments.Add(
                    new Qobj.PulseQobjExperiment(
                        header: experiment.Header,
                        instructions: experiment.Instructions,
                        config: freqConfig
                    )
                );
            }
        }

        var experimentConfig = new Dictionary<string, object>
        {
            { "pulse_library", userPulseLib.Select(x => new Qobj.PulseLibraryItem(x.Key, x.Value)).ToList() },
            { "memory_slots", experiments.Max(e => e.Header.MemorySlots) }
        };

        return (experiments, experimentConfig);
    }

    private static (List<Qobj.PulseQobjInstruction>, int) AssembleInstructions(
        object sched,
        Converters.InstructionToQobjConverter instructionConverter,
        RunConfig runConfig,
        Dictionary<string, List<Tuple<double, double>>> userPulseLib
    )
    {
        sched = Transforms.TargetQobjTransform(sched);

        int maxMemorySlot = 0;
        var qobjInstructions = new List<Qobj.PulseQobjInstruction>();
        var acquireInstructionMap = new Dictionary<(int, int), List<Qobj.PulseQobjInstruction>>();

        foreach (var instruction in sched.Instructions)
        {
            if (instruction is PlayInstruction playInstruction)
            {
                if (playInstruction.Pulse is SymbolicPulse symbolicPulse)
                {
                    bool isBackendSupported = true;
                    try
                    {
                        var pulseShape = ParametricPulseShapes.FromInstance(symbolicPulse).Name;
                        if (!runConfig.ParametricPulses.Contains(pulseShape))
                        {
                            isBackendSupported = false;
                        }
                    }
                    catch (Exception)
                    {
                        isBackendSupported = false;
                    }

                    if (!isBackendSupported)
                    {
                        playInstruction = new PlayInstruction(playInstruction.Pulse.GetWaveform(), playInstruction.Channel);
                    }
                }

                if (playInstruction.Pulse is Waveform waveform)
                {
                    var name = HashingUtils.GetSha256Hash(waveform.Samples);
                    playInstruction = new PlayInstruction(new Waveform(name, waveform.Samples), playInstruction.Channel);
                    userPulseLib[name] = waveform.Samples;
                }
            }

            if (instruction is AcquireInstruction acquireInstruction)
            {
                if (acquireInstruction.MemSlot != null)
                {
                    maxMemorySlot = Math.Max(maxMemorySlot, acquireInstruction.MemSlot.Index);
                }
                acquireInstructionMap.Add((acquireInstruction.Time, acquireInstruction.Duration), new List<Qobj.PulseQobjInstruction> { acquireInstruction });
                continue;
            }

            qobjInstructions.Add(instructionConverter.Convert(instruction));
        }

        if (acquireInstructionMap.Any())
        {
            foreach (var instructionBundle in acquireInstructionMap.Values)
            {
                qobjInstructions.AddRange(instructionConverter.Convert(instructionBundle));
            }
        }

        return (qobjInstructions, maxMemorySlot);
    }

    private static Qobj.PulseQobjConfig AssembleConfig(
        Converters.LoConfigConverter loConverter,
        Dictionary<string, object> experimentConfig,
        RunConfig runConfig
    )
    {
        var qobjConfig = runConfig.ToDict();
        foreach (var item in experimentConfig)
        {
            qobjConfig[item.Key] = item.Value;
        }

        qobjConfig.Remove("meas_map");
        qobjConfig.Remove("qubit_lo_range");
        qobjConfig.Remove("meas_lo_range");

        if (qobjConfig.ContainsKey("meas_return") && qobjConfig["meas_return"] is MeasReturnType measReturn)
        {
            qobjConfig["meas_return"] = measReturn.Value;
        }

        if (qobjConfig.ContainsKey("meas_level") && qobjConfig["meas_level"] is MeasLevel measLevel)
        {
            qobjConfig["meas_level"] = measLevel.Value;
        }

        qobjConfig["qubit_lo_freq"] = ((List<double>)qobjConfig["qubit_lo_freq"]).Select(f => f / 1e9).ToList();
        qobjConfig["meas_lo_freq"] = ((List<double>)qobjConfig["meas_lo_freq"]).Select(f => f / 1e9).ToList();

        var scheduleLos = (List<Dictionary<string, object>>)qobjConfig["schedule_los"];
        if (scheduleLos.Count == 1)
        {
            var loDict = scheduleLos[0];
            qobjConfig["qubit_lo_freq"] = loConverter.GetQubitLos(loDict).Select(f => f / 1e9).ToList();
            qobjConfig["meas_lo_freq"] = loConverter.GetMeasLos(loDict).Select(f => f / 1e9).ToList();
        }

        return new Qobj.PulseQobjConfig(qobjConfig);
    }
}
