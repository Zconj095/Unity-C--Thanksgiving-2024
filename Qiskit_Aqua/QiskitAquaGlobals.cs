using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

public class QiskitAquaGlobals
{
    // Constants
    private static readonly int CPU_COUNT = Environment.ProcessorCount;

    // Instance variables
    private int? _randomSeed = null;
    private int _numProcesses = CPU_COUNT;
    private Random _random = null;
    private bool _massive = false;

    // Constructor
    public QiskitAquaGlobals()
    {
        _randomSeed = null;
        _numProcesses = CPU_COUNT;
        _random = null;
        _massive = false;
    }

    // Property for RandomSeed
    public int? RandomSeed
    {
        get
        {
            ShowDeprecationWarning();
            return _randomSeed;
        }
        set
        {
            ShowDeprecationWarning();
            _randomSeed = value;
            _random = null; // Reset the Random instance
        }
    }

    // Property for NumProcesses
    public int NumProcesses
    {
        get
        {
            ShowDeprecationWarning();
            return _numProcesses;
        }
        set
        {
            ShowDeprecationWarning();

            if (value < 1)
            {
                throw new ArgumentException($"Invalid Number of Processes {value}.");
            }

            if (value > CPU_COUNT)
            {
                throw new ArgumentException($"Number of Processes {value} cannot be greater than CPU count {CPU_COUNT}.");
            }

            _numProcesses = value;
        }
    }

    // Property for Random
    public Random Random
    {
        get
        {
            ShowDeprecationWarning();

            if (_random == null)
            {
                if (_randomSeed.HasValue)
                {
                    _random = new Random(_randomSeed.Value);
                }
                else
                {
                    _random = new Random();
                }
            }

            return _random;
        }
    }

    // Property for Massive
    public bool Massive
    {
        get
        {
            ShowDeprecationWarning();
            return _massive;
        }
        set
        {
            ShowDeprecationWarning();
            _massive = value;
        }
    }

    // Helper method for deprecation warning
    private void ShowDeprecationWarning()
    {
        Debug.WriteLine("Deprecation Warning: This class and its methods are being deprecated. Use updated alternatives.");
    }

    // Utility to get local hardware info (CPU count in this context)
    public static int LocalHardwareInfo()
    {
        return CPU_COUNT;
    }
}

// Global instance to be used as the entry point for globals
public static class AquaGlobals
{
    public static QiskitAquaGlobals Instance { get; } = new QiskitAquaGlobals();
}
