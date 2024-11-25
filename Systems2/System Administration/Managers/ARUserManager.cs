using UnityEngine;
using System.Collections.Generic;

public class ARUserManager : MonoBehaviour
{
    private Dictionary<string, string> userDatabase = new Dictionary<string, string>();

    public void AddUser(string username, string password)
    {
        if (userDatabase.ContainsKey(username))
        {
            Debug.LogError($"User {username} already exists!");
            return;
        }

        userDatabase[username] = password;
        Debug.Log($"User {username} added successfully.");
    }

    public bool ValidateUser(string username, string password)
    {
        if (userDatabase.TryGetValue(username, out string storedPassword))
        {
            if (storedPassword == password)
            {
                Debug.Log($"User {username} authenticated successfully.");
                return true;
            }
            else
            {
                Debug.LogError("Invalid password.");
            }
        }
        else
        {
            Debug.LogError($"User {username} does not exist.");
        }

        return false;
    }

    public void RemoveUser(string username)
    {
        if (userDatabase.Remove(username))
        {
            Debug.Log($"User {username} removed successfully.");
        }
        else
        {
            Debug.LogError($"User {username} does not exist.");
        }
    }
}
