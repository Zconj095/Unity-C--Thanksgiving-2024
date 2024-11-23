using System.IO;

public class FileEncryption
{
    public static void SaveEncryptedFile(string path, string content)
    {
        string encryptedContent = AESEncryption.Encrypt(content);
        File.WriteAllText(path, encryptedContent);
    }

    public static string LoadEncryptedFile(string path)
    {
        string encryptedContent = File.ReadAllText(path);
        return AESEncryption.Decrypt(encryptedContent);
    }
}
