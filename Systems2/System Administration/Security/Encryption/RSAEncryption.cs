using System;
using System.Security.Cryptography;
using System.Text;

public class RSAEncryption
{
    private static RSAParameters _publicKey;
    private static RSAParameters _privateKey;

    public static void GenerateKeys()
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            _publicKey = rsa.ExportParameters(false);
            _privateKey = rsa.ExportParameters(true);
        }
    }

    public static string Encrypt(string plainText)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportParameters(_publicKey);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = rsa.Encrypt(plainBytes, false);

            return Convert.ToBase64String(encryptedBytes);
        }
    }

    public static string Decrypt(string encryptedText)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportParameters(_privateKey);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] plainBytes = rsa.Decrypt(encryptedBytes, false);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
