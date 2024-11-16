using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Register : IEnumerable<Bit>
{
    private static int instancesCounter = 0;
    public string Name { get; private set; }
    public int Size { get; private set; }
    protected List<Bit> Bits;
    private Dictionary<Bit, int> BitIndices;
    private string Prefix;
    private Func<Register, int, Bit> CreateBitFunc;

    public Register(int? size = null, string name = null, List<Bit> bits = null, string prefix = "reg", Func<Register, int, Bit> createBitFunc = null)
    {
        if (size == null && bits == null || size != null && bits != null)
        {
            throw new CircuitError($"Exactly one of the size or bits arguments must be provided. Provided size={size}, bits={bits}.");
        }

        if (bits != null)
        {
            size = bits.Count;
        }

        if (size <= 0)
        {
            throw new CircuitError($"Register size must be positive. Provided size={size}.");
        }

        Size = size.Value;
        Prefix = prefix;
        CreateBitFunc = createBitFunc ?? DefaultCreateBit;

        Name = name ?? $"{Prefix}{instancesCounter++}";

        if (bits != null)
        {
            if (bits.Distinct().Count() != bits.Count)
            {
                throw new CircuitError($"Register bits must not be duplicated. Provided bits={string.Join(", ", bits)}.");
            }
            Bits = new List<Bit>(bits);
        }
        else
        {
            Bits = new List<Bit>();
            for (int i = 0; i < Size; i++)
            {
                Bits.Add(CreateBitFunc(this, i));
            }
        }
    }

    private Bit DefaultCreateBit(Register register, int index)
    {
        return new Bit(register, index);
    }

    public Bit this[int index]
    {
        get
        {
            if (index < 0 || index >= Size)
            {
                throw new CircuitError("Index out of range.");
            }
            return Bits[index];
        }
    }

    public IEnumerable<Bit> this[params int[] indices]
    {
        get
        {
            foreach (int index in indices)
            {
                if (index < 0 || index >= Size)
                {
                    throw new CircuitError("Index out of range.");
                }
                yield return Bits[index];
            }
        }
    }

    public IEnumerator<Bit> GetEnumerator()
    {
        return Bits.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Contains(Bit bit)
    {
        InitializeBitIndices();
        return BitIndices.ContainsKey(bit);
    }

    public int IndexOf(Bit bit)
    {
        InitializeBitIndices();
        if (!BitIndices.ContainsKey(bit))
        {
            throw new CircuitError($"Bit {bit} not found in Register {Name}.");
        }
        return BitIndices[bit];
    }

    private void InitializeBitIndices()
    {
        if (BitIndices == null)
        {
            BitIndices = Bits.Select((bit, index) => new { bit, index })
                             .ToDictionary(b => b.bit, b => b.index);
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is not Register other || Name != other.Name || Size != other.Size)
        {
            return false;
        }

        return Bits.SequenceEqual(other.Bits);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Size);
    }

    public override string ToString()
    {
        return $"{GetType().Name}({Size}, '{Name}')";
    }
}
