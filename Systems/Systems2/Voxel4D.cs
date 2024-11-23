using UnityEngine;

public class Voxel4D {
    public int x, y, z, w;
    public Color color;
    public float density;
    public Voxel4DMaterialType materialType;
    public float time; // If w represents time

    public Voxel4D(int x, int y, int z, int w, Color color, float density, Voxel4DMaterialType materialType, float time) {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
        this.color = color;
        this.density = density;
        this.materialType = materialType;
        this.time = time;
    }
}

public enum Voxel4DMaterialType {
    Air,
    Stone,
    Water,
    // Add more as needed
}