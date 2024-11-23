using UnityEngine;
using System.Collections.Generic;
public class ARObjectSync : MonoBehaviour
{
    public void SendObjectUpdate(Vector3 position, Quaternion rotation)
    {
        string message = JsonUtility.ToJson(new
        {
            posX = position.x,
            posY = position.y,
            posZ = position.z,
            rotX = rotation.x,
            rotY = rotation.y,
            rotZ = rotation.z,
            rotW = rotation.w
        });

        // Send this message via the network
    }
}
