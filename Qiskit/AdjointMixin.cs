using System;

public class AdjointMixin
{
    // Method to get the adjoint by calling conjugate and transpose
    public AdjointMixin Adjoint()
    {
        return Conjugate().Transpose();
    }

    // Method to be overridden for conjugate logic in derived classes
    public virtual AdjointMixin Conjugate()
    {
        throw new NotImplementedException("Conjugate method must be implemented in the derived class.");
    }

    // Method to be overridden for transpose logic in derived classes
    public virtual AdjointMixin Transpose()
    {
        throw new NotImplementedException("Transpose method must be implemented in the derived class.");
    }
}
