using UnityEngine;
using System;

public class LatticeEncryption
{
    public static int[] Encrypt(int[] data, int[,] publicKey, int modulus)
    {
        int size = publicKey.GetLength(0);
        int[] encryptedData = new int[size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < data.Length; j++)
            {
                encryptedData[i] = (encryptedData[i] + publicKey[i, j] * data[j]) % modulus;
            }
        }
        return encryptedData;
    }

    public static int[] Decrypt(int[] encryptedData, int[,] privateKey, int modulus)
    {
        int size = privateKey.GetLength(0);
        int[] decryptedData = new int[size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < encryptedData.Length; j++)
            {
                decryptedData[i] = (decryptedData[i] + privateKey[i, j] * encryptedData[j]) % modulus;
            }
        }
        return decryptedData;
    }
}
