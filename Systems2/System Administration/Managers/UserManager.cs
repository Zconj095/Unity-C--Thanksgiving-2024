using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UserManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Text feedbackText;

    // Simulated user database
    private Dictionary<string, string> userDatabase = new Dictionary<string, string>();

    public void AddUser()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Error: Username and password cannot be empty!";
            return;
        }

        if (userDatabase.ContainsKey(username))
        {
            feedbackText.text = $"Error: User {username} already exists!";
            return;
        }

        userDatabase[username] = password;
        feedbackText.text = $"User {username} added successfully!";
    }

    public void ValidateUser()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (userDatabase.TryGetValue(username, out string storedPassword))
        {
            if (storedPassword == password)
            {
                feedbackText.text = $"User {username} authenticated successfully!";
            }
            else
            {
                feedbackText.text = "Error: Incorrect password.";
            }
        }
        else
        {
            feedbackText.text = $"Error: User {username} does not exist.";
        }
    }

    public void RemoveUser()
    {
        string username = usernameInput.text;

        if (userDatabase.Remove(username))
        {
            feedbackText.text = $"User {username} removed successfully!";
        }
        else
        {
            feedbackText.text = $"Error: User {username} does not exist.";
        }
    }
}
