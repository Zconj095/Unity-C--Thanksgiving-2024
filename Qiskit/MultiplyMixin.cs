using System;

public class MultiplyMixin
{
    // Multiply operator (for * operator)
    public MultiplyMixin Multiply(MultiplyMixin other)
    {
        return _Multiply(other);
    }

    // Right multiply operator (for reverse multiplication)
    public MultiplyMixin RMultiply(MultiplyMixin other)
    {
        return _Multiply(other);
    }

    // Division operator (for / operator)
    public MultiplyMixin Divide(double other)
    {
        return _Multiply(1 / other);
    }

    // Negation operator (for unary - operator)
    public MultiplyMixin Negate()
    {
        return _Multiply(-1);
    }

    // Actual multiplication logic, to be implemented in derived classes
    protected virtual MultiplyMixin _Multiply(MultiplyMixin other)
    {
        // Implement the multiplication logic here, e.g. multiplying two operators, or a scalar with an operator
        throw new NotImplementedException("The _Multiply method must be implemented in the derived class.");
    }

    // Operator overload for * (multiplication)
    public static MultiplyMixin operator *(MultiplyMixin left, MultiplyMixin right)
    {
        return left.Multiply(right);
    }

    // Operator overload for / (division)
    public static MultiplyMixin operator /(MultiplyMixin left, double right)
    {
        return left.Divide(right);
    }

    // Operator overload for unary negation
    public static MultiplyMixin operator -(MultiplyMixin operand)
    {
        return operand.Negate();
    }
}
