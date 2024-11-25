using UnityEngine;
using UnityEngine.UI;

public class UserInputHandler : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;

    public InputField processNameInput;
    public InputField spectralContentInput;
    public Button processButton;

    public Text feedbackText;

    void Start()
    {
        // Attach button click listeners
        loginButton.onClick.AddListener(OnLoginSubmit);
        processButton.onClick.AddListener(OnProcessSubmit);
    }

    void OnLoginSubmit()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Add your validation or processing logic here
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Username or password cannot be empty!";
        }
        else
        {
            feedbackText.text = $"Login Submitted: Username={username}, Password={password}";
            Debug.Log($"Login Submitted: Username={username}, Password={password}");
        }
    }

    void OnProcessSubmit()
    {
        string processName = processNameInput.text;
        string spectralContent = spectralContentInput.text;

        // Add your validation or processing logic here
        if (string.IsNullOrEmpty(processName) || string.IsNullOrEmpty(spectralContent))
        {
            feedbackText.text = "Process name or spectral content cannot be empty!";
        }
        else
        {
            feedbackText.text = $"Process Submitted: ProcessName={processName}, SpectralContent={spectralContent}";
            Debug.Log($"Process Submitted: ProcessName={processName}, SpectralContent={spectralContent}");
        }
    }
}
