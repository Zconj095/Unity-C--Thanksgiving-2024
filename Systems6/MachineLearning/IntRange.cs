namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Represents a range of integer values.
    /// </summary>
    public struct IntRange
    {
        public int Min { get; }
        public int Max { get; }

        public IntRange(int min, int max)
        {
            if (min > max)
                throw new System.ArgumentException("Min cannot be greater than Max.");

            Min = min;
            Max = max;
        }

        public bool Contains(int value) => value >= Min && value <= Max;

        public override string ToString() => $"[{Min}, {Max}]";
    }
}
