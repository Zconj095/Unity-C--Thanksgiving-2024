using System;
using System.Collections.Generic;

public class DevicePropertiesHelper
{
    private static readonly Dictionary<string, double> NanosecondUnits = new()
    {
        { "s", 1e9 },
        { "ms", 1e6 },
        { "µs", 1e3 },
        { "us", 1e3 },
        { "ns", 1 }
    };

    private static readonly Dictionary<string, double> GHzUnits = new()
    {
        { "Hz", 1e-9 },
        { "KHz", 1e-6 },
        { "MHz", 1e-3 },
        { "GHz", 1 },
        { "THz", 1e3 }
    };

    public List<(string Name, int[] Qubits, double? Time, double? Error)> GateParamValues(DeviceProperties properties)
    {
        var values = new List<(string, int[], double?, double?)>();

        foreach (var gate in properties.Gates)
        {
            var name = gate.GateName;
            var qubits = gate.Qubits;

            double? gateLength = null;
            var timeParam = CheckForItem(gate.Parameters, "gate_length");
            if (timeParam != null && timeParam.Value.HasValue)
            {
                gateLength = timeParam.Value.Value;
                if (timeParam.Unit != null)
                {
                    gateLength *= NanosecondUnits.GetValueOrDefault(timeParam.Unit, 1);
                }
            }

            double? gateError = null;
            var errorParam = CheckForItem(gate.Parameters, "gate_error");
            if (errorParam != null && errorParam.Value.HasValue)
            {
                gateError = errorParam.Value.Value;
            }

            values.Add((name, qubits, gateLength, gateError));
        }

        return values;
    }

    public List<(string Name, int[] Qubits, double? Error)> GateErrorValues(DeviceProperties properties)
    {
        var values = new List<(string, int[], double?)>();

        foreach (var gate in properties.Gates)
        {
            var name = gate.GateName;
            var qubits = gate.Qubits;

            double? value = null;
            var paramsError = CheckForItem(gate.Parameters, "gate_error");
            if (paramsError != null && paramsError.Value.HasValue)
            {
                value = paramsError.Value.Value;
            }

            values.Add((name, qubits, value));
        }

        return values;
    }

    public List<(string Name, int[] Qubits, double? Length)> GateLengthValues(DeviceProperties properties)
    {
        var values = new List<(string, int[], double?)>();

        foreach (var gate in properties.Gates)
        {
            var name = gate.GateName;
            var qubits = gate.Qubits;

            double? value = null;
            var paramsLength = CheckForItem(gate.Parameters, "gate_length");
            if (paramsLength != null && paramsLength.Value.HasValue)
            {
                value = paramsLength.Value.Value;
                if (paramsLength.Unit != null)
                {
                    value *= NanosecondUnits.GetValueOrDefault(paramsLength.Unit, 1);
                }
            }

            values.Add((name, qubits, value));
        }

        return values;
    }

    public List<double[]> ReadoutErrorValues(DeviceProperties properties)
    {
        var values = new List<double[]>();

        foreach (var qubitProps in properties.Qubits)
        {
            double[] value = null;

            var paramsRoError = CheckForItem(qubitProps.Parameters, "readout_error");
            var paramsM1P0 = CheckForItem(qubitProps.Parameters, "prob_meas1_prep0");
            var paramsM0P1 = CheckForItem(qubitProps.Parameters, "prob_meas0_prep1");

            if (paramsM1P0 != null && paramsM0P1 != null && paramsM1P0.Value.HasValue && paramsM0P1.Value.HasValue)
            {
                value = new double[] { paramsM1P0.Value.Value, paramsM0P1.Value.Value };
            }
            else if (paramsRoError != null && paramsRoError.Value.HasValue)
            {
                value = new double[] { paramsRoError.Value.Value, paramsRoError.Value.Value };
            }

            values.Add(value);
        }

        return values;
    }

    public List<(double T1, double T2, double Freq)> ThermalRelaxationValues(DeviceProperties properties)
    {
        var values = new List<(double, double, double)>();

        foreach (var qubitProps in properties.Qubits)
        {
            double t1 = double.PositiveInfinity;
            double t2 = double.PositiveInfinity;
            double freq = double.PositiveInfinity;

            var t1Params = CheckForItem(qubitProps.Parameters, "T1");
            var t2Params = CheckForItem(qubitProps.Parameters, "T2");
            var freqParams = CheckForItem(qubitProps.Parameters, "frequency");

            if (t1Params != null && t1Params.Value.HasValue)
            {
                t1 = t1Params.Value.Value;
                if (t1Params.Unit != null)
                {
                    t1 *= NanosecondUnits.GetValueOrDefault(t1Params.Unit, 1);
                }
            }

            if (t2Params != null && t2Params.Value.HasValue)
            {
                t2 = t2Params.Value.Value;
                if (t2Params.Unit != null)
                {
                    t2 *= NanosecondUnits.GetValueOrDefault(t2Params.Unit, 1);
                }
            }

            if (freqParams != null && freqParams.Value.HasValue)
            {
                freq = freqParams.Value.Value;
                if (freqParams.Unit != null)
                {
                    freq *= GHzUnits.GetValueOrDefault(freqParams.Unit, 1);
                }
            }

            values.Add((t1, t2, freq));
        }

        return values;
    }

    private Parameter CheckForItem(List<Parameter> parameters, string name)
    {
        foreach (var param in parameters)
        {
            if (param.Name == name)
            {
                return param;
            }
        }

        return null;
    }
}
