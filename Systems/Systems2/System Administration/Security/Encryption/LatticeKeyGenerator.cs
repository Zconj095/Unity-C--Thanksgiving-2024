using System;

public class LatticeKeyGenerator
{
    public static (int[,] publicKey, int[,] privateKey) GenerateKeys(int size, int modulus)
    {
        Random random = new Random();
        int[,] publicKey = new int[size, size];
        int[,] privateKey = new int[size, size];

        // Generate random public and private keys
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                publicKey[i, j] = random.Next(modulus);
                privateKey[i, j] = random.Next(modulus);
            }
        }
        return (publicKey, privateKey);
    }
}
