using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRControllerEncryption : MonoBehaviour
{
    void Update()
    {
        // Retrieve all controllers
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, devices);

        foreach (var device in devices)
        {
            if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position) &&
                device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation))
            {
                // Encrypt controller data
                string rawData = $"{position}-{rotation}";
                string encryptedData = AESEncryption.Encrypt(rawData);
                Debug.Log($"Encrypted Controller Data: {encryptedData}");

                // Decrypt controller data
                string decryptedData = AESEncryption.Decrypt(encryptedData);
                Debug.Log($"Decrypted Controller Data: {decryptedData}");
            }
        }
    }
}
