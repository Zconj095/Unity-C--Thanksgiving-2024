using UnityEngine;

public class VRDataEncoder
{
    public static int[] EncodeInput(Vector3 position, Quaternion rotation)
    {
        // Example: Convert VR position and rotation into integers for encryption
        return new int[]
        {
            Mathf.RoundToInt(position.x * 1000),
            Mathf.RoundToInt(position.y * 1000),
            Mathf.RoundToInt(position.z * 1000),
            Mathf.RoundToInt(rotation.x * 1000),
            Mathf.RoundToInt(rotation.y * 1000),
            Mathf.RoundToInt(rotation.z * 1000),
            Mathf.RoundToInt(rotation.w * 1000)
        };
    }

    public static Vector3 DecodePosition(int[] encodedData)
    {
        // Example: Decode integers back to Vector3
        return new Vector3(encodedData[0] / 1000f, encodedData[1] / 1000f, encodedData[2] / 1000f);
    }
}
