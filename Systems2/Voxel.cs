using UnityEngine;

public class Voxel {
    public int x, y, z;
    public Color color;
    public float density;
    public MaterialType materialType;

    public Voxel(int x, int y, int z, Color color, float density, MaterialType materialType) {
        this.x = x;
        this.y = y;
        this.z = z;
        this.color = color;
        this.density = density;
        this.materialType = materialType;
    }
}

public enum MaterialType {
    Air,
    Stone,
    Water,
    // Add more as needed
}