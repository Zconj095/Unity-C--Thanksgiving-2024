using System.Collections.Generic;
using UnityEngine;

public class CognizantClusteredQuadTreeNode
{
    public Rect Bounds; // Spatial boundary
    public List<Vector2> Points; // Data points within the node
    public CognizantClusteredQuadTreeNode[] Children; // Subdivisions

    private int capacity; // Maximum points before subdivision

    public CognizantClusteredQuadTreeNode(Rect bounds, int capacity)
    {
        Bounds = bounds;
        Points = new List<Vector2>();
        Children = null;
        this.capacity = capacity;
    }

    public bool Insert(Vector2 point)
    {
        if (!Bounds.Contains(point)) return false;

        if (Points.Count < capacity)
        {
            Points.Add(point);
            return true;
        }

        if (Children == null)
        {
            Subdivide();
        }

        foreach (var child in Children)
        {
            if (child.Insert(point)) return true;
        }

        return false;
    }

    private void Subdivide()
    {
        float halfWidth = Bounds.width / 2f;
        float halfHeight = Bounds.height / 2f;
        float x = Bounds.xMin;
        float y = Bounds.yMin;

        Children = new CognizantClusteredQuadTreeNode[4];
        Children[0] = new CognizantClusteredQuadTreeNode(new Rect(x, y, halfWidth, halfHeight), capacity);
        Children[1] = new CognizantClusteredQuadTreeNode(new Rect(x + halfWidth, y, halfWidth, halfHeight), capacity);
        Children[2] = new CognizantClusteredQuadTreeNode(new Rect(x, y + halfHeight, halfWidth, halfHeight), capacity);
        Children[3] = new CognizantClusteredQuadTreeNode(new Rect(x + halfWidth, y + halfHeight, halfWidth, halfHeight), capacity);
    }

    public List<Vector2> Query(Rect range, List<Vector2> found = null)
    {
        if (found == null) found = new List<Vector2>();

        if (!Bounds.Overlaps(range)) return found;

        foreach (var point in Points)
        {
            if (range.Contains(point))
            {
                found.Add(point);
            }
        }

        if (Children != null)
        {
            foreach (var child in Children)
            {
                child.Query(range, found);
            }
        }

        return found;
    }
}
