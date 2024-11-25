public class VoxelGrid {
    private Voxel[] voxels;
    private int width, height, depth;

    public VoxelGrid(int width, int height, int depth) {
        this.width = width;
        this.height = height;
        this.depth = depth;
        voxels = new Voxel[width * height * depth];
    }

    public Voxel GetVoxel(int x, int y, int z) {
        int index = x + y * width + z * width * height;
        return voxels[index];
    }

    public void SetVoxel(int x, int y, int z, Voxel voxel) {
        int index = x + y * width + z * width * height;
        voxels[index] = voxel;
    }
}