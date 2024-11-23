using UnityEngine;

public class QuantumVREncryptionDemo : MonoBehaviour
{
    void Start()
    {
        // Step 1: Generate keys
        var (publicKey, privateKey) = LatticeKeyGenerator.GenerateKeys(3, 256);

        // Step 2: Encode VR data
        Vector3 position = new Vector3(1.23f, 4.56f, 7.89f);
        Quaternion rotation = Quaternion.Euler(45f, 90f, 180f);
        int[] encodedData = VRDataEncoder.EncodeInput(position, rotation);

        // Step 3: Encrypt data
        int[] encryptedData = LatticeEncryption.Encrypt(encodedData, publicKey, 256);
        Debug.Log($"Encrypted Data: {string.Join(",", encryptedData)}");

        // Step 4: Decrypt data
        int[] decryptedData = LatticeEncryption.Decrypt(encryptedData, privateKey, 256);
        Vector3 decodedPosition = VRDataEncoder.DecodePosition(decryptedData);
        Debug.Log($"Decoded Position: {decodedPosition}");
    }
}
