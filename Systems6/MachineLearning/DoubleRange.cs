namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Represents a range of double values.
    /// </summary>
    public struct DoubleRange
    {
        public double Min { get; }
        public double Max { get; }

        public DoubleRange(double min, double max)
        {
            if (min > max)
                throw new System.ArgumentException("Min cannot be greater than Max.");

            Min = min;
            Max = max;
        }

        public bool Contains(double value) => value >= Min && value <= Max;

        public override string ToString() => $"[{Min}, {Max}]";
    }
}
