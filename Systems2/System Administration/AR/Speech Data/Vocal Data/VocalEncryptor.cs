using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class VocalEncryptor : MonoBehaviour
{
    private static readonly string Key = "EncryptionKey123"; // Must be 16, 24, or 32 chars

    public byte[] EncryptAudio(float[] samples)
    {
        string data = ConvertSamplesToString(samples);
        return EncryptString(data);
    }

    public float[] DecryptAudio(byte[] encryptedData)
    {
        string decryptedString = DecryptString(encryptedData);
        return ConvertStringToSamples(decryptedString);
    }

    private string ConvertSamplesToString(float[] samples)
    {
        return string.Join(",", samples);
    }

    private float[] ConvertStringToSamples(string data)
    {
        string[] stringArray = data.Split(',');
        float[] samples = Array.ConvertAll(stringArray, float.Parse);
        return samples;
    }

    private byte[] EncryptString(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = new byte[16]; // Default IV
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
    }

    private string DecryptString(byte[] encryptedBytes)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = new byte[16]; // Default IV
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
