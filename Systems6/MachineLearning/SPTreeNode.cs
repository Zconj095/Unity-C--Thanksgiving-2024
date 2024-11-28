using System;
using System.Text;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    public class SPTreeNode
    {
        private readonly SPTree owner;
        private int cumulativeSize;
        private SPCell region;
        private double[] centerOfMass;
        private double[] buffer;

        public double[] Point { get; private set; }
        public SPTreeNode[] Children { get; private set; }
        public SPTreeNode Parent { get; private set; }
        public int Index { get; private set; }

        public bool IsEmpty => cumulativeSize == 0;
        public bool IsLeaf => Children == null;

        public SPTreeNode(SPTree owner, SPTreeNode parent, int index, double[] corner, double[] width)
        {
            int dimension = owner.Dimension;
            this.owner = owner;
            this.Parent = parent;
            this.Index = index;
            this.region = new SPCell(corner, width);
            this.centerOfMass = new double[dimension];
            this.buffer = new double[dimension];
        }

        public bool Add(double[] point)
        {
            if (!region.Contains(point))
                return false;

            cumulativeSize++;
            double mult1 = (double)(cumulativeSize - 1) / cumulativeSize;
            double mult2 = 1.0 / cumulativeSize;

            for (int d = 0; d < centerOfMass.Length; d++)
            {
                centerOfMass[d] *= mult1;
                centerOfMass[d] += mult2 * point[d];
            }

            if (IsLeaf && Point == null)
            {
                Point = point;
                return true;
            }

            if (IsLeaf)
                Subdivide();

            foreach (var child in Children)
                if (child.Add(point))
                    return true;

            return false;
        }

        private void Subdivide()
        {
            int dimension = owner.Dimension;
            int childCount = (int)Math.Pow(2, dimension);
            Children = new SPTreeNode[childCount];

            for (int i = 0; i < childCount; i++)
            {
                var newCorner = new double[dimension];
                var newWidth = new double[dimension];
                int divisor = 1;

                for (int d = 0; d < dimension; d++)
                {
                    newWidth[d] = 0.5 * region.Width[d];
                    newCorner[d] = ((i / divisor) % 2 == 1)
                        ? region.Corner[d] - 0.5 * region.Width[d]
                        : region.Corner[d] + 0.5 * region.Width[d];
                    divisor *= 2;
                }

                Children[i] = new SPTreeNode(owner, this, i, newCorner, newWidth);
            }

            if (Point != null)
            {
                foreach (var child in Children)
                    if (child.Add(Point))
                        break;

                Point = null;
            }
        }

        public void ComputeNonEdgeForces(double[] point, double theta, double[] negForces, ref double sumQ)
        {
            if (cumulativeSize == 0 || (IsLeaf && IsEqual(Point, point)))
                return;

            double distanceSquared = 0.0;
            for (int d = 0; d < owner.Dimension; d++)
            {
                buffer[d] = point[d] - centerOfMass[d];
                distanceSquared += buffer[d] * buffer[d];
            }

            double maxWidth = region.Width.Max();

            if (IsLeaf || maxWidth / Math.Sqrt(distanceSquared) < theta)
            {
                double invDist = 1.0 / (1.0 + distanceSquared);
                double mult = cumulativeSize * invDist;
                sumQ += mult;
                mult *= invDist;

                for (int d = 0; d < owner.Dimension; d++)
                    negForces[d] += mult * buffer[d];
            }
            else
            {
                foreach (var child in Children)
                    child.ComputeNonEdgeForces(point, theta, negForces, ref sumQ);
            }
        }

        private bool IsEqual(double[] a, double[] b, double epsilon = 1e-10)
        {
            for (int i = 0; i < a.Length; i++)
                if (Math.Abs(a[i] - b[i]) > epsilon)
                    return false;

            return true;
        }

        public override string ToString()
        {
            if (IsEmpty)
                return "Empty node";

            var sb = new StringBuilder();
            if (IsLeaf)
            {
                sb.AppendLine($"Leaf node; Point: {VectorToString(Point)}");
            }
            else
            {
                sb.AppendLine($"Center of Mass: {VectorToString(centerOfMass)}, Children:");
                foreach (var child in Children)
                    sb.AppendLine(child.ToString());
            }

            return sb.ToString();
        }

        private string VectorToString(double[] vector)
        {
            if (vector == null)
                return "null";

            return $"[{string.Join(", ", vector)}]";
        }
    }
}
