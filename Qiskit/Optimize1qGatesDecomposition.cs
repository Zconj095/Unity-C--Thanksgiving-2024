using System;
using System.Collections.Generic;
using System.Linq;

public class Optimize1qGatesDecomposition : TransformationPass
{
    private HashSet<string> _basisGates;
    private Target _target;
    private Dictionary<string, object> _globalDecomposers;
    private Dictionary<int, List<Decomposer>> _localDecomposersCache;
    private OneQubitGateErrorMap _errorMap;

    public Optimize1qGatesDecomposition(List<string> basis = null, Target target = null)
    {
        _basisGates = basis != null ? new HashSet<string>(basis) : null;
        _target = target;
        _globalDecomposers = null;
        _localDecomposersCache = new Dictionary<int, List<Decomposer>>();

        if (basis != null)
        {
            _globalDecomposers = GetPossibleDecomposers(new HashSet<string>(basis));
        }
        else if (target == null)
        {
            _globalDecomposers = GetPossibleDecomposers(null);
            _basisGates = null;
        }

        _errorMap = BuildErrorMap();
    }

    private OneQubitGateErrorMap BuildErrorMap()
    {
        if (_target != null && _target.NumQubits != null)
        {
            OneQubitGateErrorMap errorMap = new OneQubitGateErrorMap(_target.NumQubits);
            for (int qubit = 0; qubit < _target.NumQubits; qubit++)
            {
                var gateError = new Dictionary<string, float>();
                foreach (var gate in _target)
                {
                    var gateProps = _target.GetGateProperties(gate);
                    if (gateProps != null && gateProps.Error != null)
                    {
                        gateError[gate] = gateProps.Error.Value;
                    }
                }
                errorMap.AddQubit(gateError);
            }
            return errorMap;
        }
        return null;
    }

    private List<Decomposer> GetDecomposer(int? qubit = null)
    {
        if (_target != null && _target.NumQubits != null)
        {
            if (qubit != null)
            {
                if (!_localDecomposersCache.ContainsKey(qubit.Value))
                {
                    var available1qBasis = _target.GetAvailableGatesForQargs(new int[] { qubit.Value });
                    _localDecomposersCache[qubit.Value] = GetPossibleDecomposers(available1qBasis);
                }
                return _localDecomposersCache[qubit.Value];
            }
            else
            {
                return _globalDecomposers;
            }
        }
        return _globalDecomposers;
    }

    public object ResynthesizeRun(Matrix matrix, int? qubit = null)
    {
        var decomposers = GetDecomposer(qubit);
        var bestSynthCircuit = OneQubitDecomposer.UnitaryToGateSequence(matrix, decomposers, qubit, _errorMap);
        return bestSynthCircuit;
    }

    private DAGCircuit GateSequenceToDag(object bestSynthCircuit)
    {
        var qubits = new QuantumRegister(1);
        var outDag = new DAGCircuit();
        outDag.AddQubits(qubits);
        outDag.GlobalPhase = bestSynthCircuit.GlobalPhase;

        foreach (var gate in bestSynthCircuit)
        {
            var op = CircuitInstruction.FromStandard(gate.Name, qubits, gate.Angles);
            outDag.ApplyOperationBack(op.Operation, qubits);
        }
        return outDag;
    }

    private bool SubstitutionChecks(DAGCircuit dag, List<DAGOpNode> oldRun, DAGCircuit newCirc, HashSet<string> basis, int qubit, float? oldError = null, float? newError = null)
    {
        if (newCirc == null) return false;

        bool hasCalibrations = dag.CalibrationsProp != null && dag.CalibrationsProp.Count > 0;
        bool hasUncalibratedGates = !hasCalibrations || oldRun.Any(g => !dag.HasCalibrationFor(g));

        bool uncalibratedAndNotBasis = basis != null && oldRun.Any(g => !basis.Contains(g.Name) && (!hasCalibrations || !dag.HasCalibrationFor(g)));
        
        if (!uncalibratedAndNotBasis)
        {
            if (newError == null)
                newError = CalculateError(newCirc, qubit);
            if (oldError == null)
                oldError = CalculateError(oldRun, qubit);
        }
        else
        {
            newError = 0.0f;
            oldError = 0.0f;
        }

        return uncalibratedAndNotBasis || (hasUncalibratedGates && newError < oldError);
    }

    private float CalculateError(object circuit, int qubit)
    {
        return OneQubitDecomposer.ComputeErrorList(circuit, qubit, _errorMap);
    }

    public DAGCircuit Run(DAGCircuit dag)
    {
        OneQubitDecomposer.Optimize1qGatesDecomposition(dag, _target, _globalDecomposers, _basisGates);
        return dag;
    }

    public static List<string> GetPossibleDecomposers(HashSet<string> basisSet)
    {
        List<string> decomposers = new List<string>();
        var eulerBasisGates = OneQubitDecompose.OneQubitEulerBasisGates;

        if (basisSet == null)
        {
            decomposers.AddRange(eulerBasisGates.Values);
        }
        else
        {
            foreach (var gate in eulerBasisGates)
            {
                if (basisSet.IsSupersetOf(gate.Value))
                {
                    decomposers.Add(gate.Key);
                }
            }

            if (decomposers.Contains("U3") && decomposers.Contains("U321"))
                decomposers.Remove("U3");

            if (decomposers.Contains("ZSX") && decomposers.Contains("ZSXX"))
                decomposers.Remove("ZSX");
        }
        return decomposers;
    }
}
