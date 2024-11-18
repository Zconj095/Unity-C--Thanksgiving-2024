using UnityEngine;

public class ComplexNumber
{
    public double Real { get; private set; }
    public double Imaginary { get; private set; }

    /// <summary>
    /// Constructor to initialize a complex number.
    /// </summary>
    /// <param name="real">Real part of the complex number.</param>
    /// <param name="imaginary">Imaginary part of the complex number.</param>
    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    /// <summary>
    /// Creates a new ComplexNumber instance.
    /// </summary>
    public static ComplexNumber Create(double real, double imaginary)
    {
        return new ComplexNumber(real, imaginary);
    }

    /// <summary>
    /// Adds two complex numbers.
    /// </summary>
    public static ComplexNumber Add(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    /// <summary>
    /// Multiplies two complex numbers.
    /// </summary>
    public static ComplexNumber Multiply(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(
            a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + a.Imaginary * b.Real
        );
    }

    /// <summary>
    /// Divides a complex number by a scalar.
    /// </summary>
    public static ComplexNumber Divide(ComplexNumber a, double scalar)
    {
        return new ComplexNumber(a.Real / scalar, a.Imaginary / scalar);
    }

    /// <summary>
    /// Gets the magnitude of the complex number.
    /// </summary>
    public double GetMagnitude()
    {
        return Mathf.Sqrt((float)(Real * Real + Imaginary * Imaginary));
    }

    /// <summary>
    /// Overrides the ToString method to display the complex number.
    /// </summary>
    public override string ToString()
    {
        return $"{Real} + {Imaginary}i";
    }
}
