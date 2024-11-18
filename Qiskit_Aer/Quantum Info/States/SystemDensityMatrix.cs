using System;

public class SystemDensityMatrix
{
    public object Data { get; set; }
    public int[] Dims { get; set; }

    public virtual SystemDensityMatrix Conjugate()
    {
        // Default implementation for conjugating a density matrix.
        if (Data is Complex[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Complex.Conjugate(data[i]);
            }
            return new SystemDensityMatrix { Data = data, Dims = this.Dims };
        }
        throw new InvalidOperationException("Unsupported data type for conjugation");
    }

    // Initialize method for general setup, you can adjust if needed
    public void Initialize(object data, int[] dims = null)
    {
        this.Data = data;
        this.Dims = dims;
    }
}
