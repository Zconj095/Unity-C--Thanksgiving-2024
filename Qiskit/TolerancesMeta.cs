using System;

public class TolerancesMeta
{
    // Default values for ATOL, RTOL, and MAX_TOL
    private static double _atolDefault = 1e-8;
    private static double _rtolDefault = 1e-5;
    private const double _maxTol = 1e-4;

    public static double ATOL_DEFAULT
    {
        get { return _atolDefault; }
        set
        {
            CheckValue(value, "atol");
            _atolDefault = value;
        }
    }

    public static double RTOL_DEFAULT
    {
        get { return _rtolDefault; }
        set
        {
            CheckValue(value, "rtol");
            _rtolDefault = value;
        }
    }

    private static void CheckValue(double value, string valueName)
    {
        // Ensure the tolerance value is valid
        if (value < 0)
        {
            throw new ArgumentException($"Invalid {valueName} ({value}) must be non-negative.");
        }

        if (value > _maxTol)
        {
            throw new ArgumentException($"Invalid {valueName} ({value}) must be less than {_maxTol}.");
        }
    }
}

public class TolerancesMixin : TolerancesMeta
{
    // Instance properties to access the tolerance values for a specific object
    public double Atol => ATOL_DEFAULT;
    public double Rtol => RTOL_DEFAULT;
}
