using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine; // Required for PlayerPrefs

public class DataStorage
{
    public static string HashData(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes); // Convert requires the System namespace
        }
    }

    public static void StoreData(string username, string hashedPassword)
    {
        // Save username and hashedPassword securely
        PlayerPrefs.SetString(username, hashedPassword); // Unity's PlayerPrefs for storage
        PlayerPrefs.Save(); // Ensures data is written to disk
    }

    public static bool ValidateData(string username, string input)
    {
        if (!PlayerPrefs.HasKey(username))
        {
            return false; // No stored data for the username
        }

        string storedHash = PlayerPrefs.GetString(username);
        string inputHash = HashData(input);
        return storedHash == inputHash;
    }
}
