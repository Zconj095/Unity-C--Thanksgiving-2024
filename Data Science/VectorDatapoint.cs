using System.Linq;

public class VectorDataPoint
{
    public float[] Features { get; private set; }

    public VectorDataPoint(float[] features)
    {
        Features = features ?? throw new System.ArgumentNullException(nameof(features));
    }

    public override string ToString()
    {
        return "[" + string.Join(", ", Features.Select(f => f.ToString("F4"))) + "]";
    }
}
