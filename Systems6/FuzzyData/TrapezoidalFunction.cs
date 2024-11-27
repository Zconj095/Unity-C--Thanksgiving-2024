using UnityEngine;

namespace UnityFuzzy
{
    /// <summary>
    /// Membership function in the shape of a trapezoid. Can represent a full trapezoid or a half trapezoid.
    /// </summary>
    public class TrapezoidalFunction : PiecewiseLinearFunction
    {
        public enum EdgeType
        {
            Left,
            Right
        }

        private TrapezoidalFunction(int size) : base(new Point[size])
        {
        }

        public TrapezoidalFunction(float m1, float m2, float m3, float m4, float max = 1.0f, float min = 0.0f) : this(4)
        {
            points[0] = new Point(m1, min);
            points[1] = new Point(m2, max);
            points[2] = new Point(m3, max);
            points[3] = new Point(m4, min);
        }

        public TrapezoidalFunction(float m1, float m2, float m3, float max = 1.0f, float min = 0.0f) : this(3)
        {
            points[0] = new Point(m1, min);
            points[1] = new Point(m2, max);
            points[2] = new Point(m3, min);
        }

        public TrapezoidalFunction(float m1, float m2, float max, float min, EdgeType edge) : this(2)
        {
            if (edge == EdgeType.Left)
            {
                points[0] = new Point(m1, min);
                points[1] = new Point(m2, max);
            }
            else
            {
                points[0] = new Point(m1, max);
                points[1] = new Point(m2, min);
            }
        }

        public TrapezoidalFunction(float m1, float m2, EdgeType edge) : this(m1, m2, 1.0f, 0.0f, edge)
        {
        }
    }
}
