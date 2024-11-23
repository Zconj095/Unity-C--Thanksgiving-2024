public class VoxelGrid4D {
    private Voxel4D[] voxels;
    private int width, height, depth, timeSteps;

    public VoxelGrid4D(int width, int height, int depth, int timeSteps) {
        this.width = width;
        this.height = height;
        this.depth = depth;
        this.timeSteps = timeSteps;
        voxels = new Voxel4D[width * height * depth * timeSteps];
    }

    public Voxel4D GetVoxel(int x, int y, int z, int w) {
        int index = x + y * width + z * width * height + w * width * height * depth;
        return voxels[index];
    }

    public void SetVoxel(int x, int y, int z, int w, Voxel4D voxel) {
        int index = x + y * width + z * width * height + w * width * height * depth;
        voxels[index] = voxel;
    }
}