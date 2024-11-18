using System;

public class CustomComplex
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public CustomComplex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // Addition of complex numbers
    public static CustomComplex operator +(CustomComplex a, CustomComplex b)
    {
        return new CustomComplex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    // Subtraction of complex numbers
    public static CustomComplex operator -(CustomComplex a, CustomComplex b)
    {
        return new CustomComplex(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }

    // Multiplication of complex numbers
    public static CustomComplex operator *(CustomComplex a, CustomComplex b)
    {
        return new CustomComplex(
            a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + a.Imaginary * b.Real
        );
    }

    // Conjugate of a complex number
    public CustomComplex Conjugate()
    {
        return new CustomComplex(Real, -Imaginary);
    }

    // Magnitude of a complex number
    public double Magnitude => Math.Sqrt(Real * Real + Imaginary * Imaginary);
}
