using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SpeechSynthesisEncryptor
{
    private static readonly string EncryptionKey = "EncryptionKey123"; // Key must be 16, 24, or 32 characters

    /// <summary>
    /// Encrypts the synthesized audio data.
    /// </summary>
    public static byte[] EncryptAudio(float[] audioSamples)
    {
        string audioData = ConvertSamplesToString(audioSamples);
        return EncryptString(audioData);
    }

    /// <summary>
    /// Decrypts the encrypted audio data.
    /// </summary>
    public static float[] DecryptAudio(byte[] encryptedData)
    {
        string decryptedString = DecryptString(encryptedData);
        return ConvertStringToSamples(decryptedString);
    }

    private static string ConvertSamplesToString(float[] samples)
    {
        return string.Join(",", samples);
    }

    private static float[] ConvertStringToSamples(string data)
    {
        string[] stringArray = data.Split(',');
        float[] samples = Array.ConvertAll(stringArray, float.Parse);
        return samples;
    }

    private static byte[] EncryptString(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = new byte[16]; // Default IV
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
    }

    private static string DecryptString(byte[] encryptedBytes)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = new byte[16]; // Default IV
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
