using UnityEngine;
using UnityEngine.UI;

public class EmotionalSynthesisAdmin : MonoBehaviour
{
    public Text currentEmotionDisplay;
    public Text intensityValueText;
    public Text logOutput;
    public Slider pitchSlider;
    public Slider energySlider;

    private EmotionalEngine emotionalEngine;

    private void Start()
    {
        // Check if UI components and sliders are assigned
        if (!ValidateUIComponents())
        {
            return; // Stop execution if components are not assigned
        }

        // Initialize EmotionalEngine by attaching it to the current GameObject
        emotionalEngine = gameObject.AddComponent<EmotionalEngine>();
        if (emotionalEngine == null)
        {
            Debug.LogError("Failed to initialize EmotionalEngine.");
            return;
        }

        // Attach listeners to sliders
        pitchSlider.onValueChanged.AddListener(OnPitchSliderChanged);
        energySlider.onValueChanged.AddListener(OnEnergySliderChanged);

        LogEvent("Emotional Synthesis Admin Initialized.");
    }

    private void OnPitchSliderChanged(float pitch)
    {
        OnEmotionUpdate(pitch, energySlider.value);
    }

    private void OnEnergySliderChanged(float energy)
    {
        OnEmotionUpdate(pitchSlider.value, energy);
    }

    public void OnEmotionUpdate(float pitch, float energy)
    {
        float[] spectralContent = ParseSpectralContent("[0.1, 0.2, 0.3]");

        if (spectralContent == null || spectralContent.Length != 3)
        {
            Debug.LogWarning("Invalid spectral content. Using default values [0.1, 0.2, 0.3].");
            spectralContent = new float[] { 0.1f, 0.2f, 0.3f };
        }

        string emotion = emotionalEngine.UpdateEmotion(pitch, energy, 0.6f, 1.5f, spectralContent); // Adjust volume and speechRate as needed
        float intensity = emotionalEngine.GetEmotionIntensity();

        UpdateUI(emotion, intensity);
    }

    private float[] ParseSpectralContent(string input)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Debug.LogWarning("Spectral content input is empty.");
                return null;
            }

            string[] parts = input.Trim('[', ']').Split(',');
            float[] spectralContent = new float[3];

            for (int i = 0; i < parts.Length; i++)
            {
                spectralContent[i] = float.Parse(parts[i].Trim());
            }

            return spectralContent;
        }
        catch
        {
            Debug.LogError("Error parsing spectral content. Expected format: [0.1, 0.2, 0.3].");
            return null;
        }
    }

    public void UpdateUI(string emotion, float intensity)
    {
        if (currentEmotionDisplay != null && intensityValueText != null)
        {
            currentEmotionDisplay.text = $"Current Emotion: {emotion}";
            intensityValueText.text = $"Intensity: {intensity:F2}";
        }
        else
        {
            Debug.LogError("UI components for emotion display are not assigned or active.");
        }
    }

    public void LogEvent(string message)
    {
        if (logOutput != null)
        {
            logOutput.text += $"{message}\n";
        }
        Debug.Log(message);
    }

    private bool ValidateUIComponents()
    {
        if (currentEmotionDisplay == null || intensityValueText == null || logOutput == null || pitchSlider == null || energySlider == null)
        {
            Debug.LogError("One or more UI components or sliders are not assigned in the Inspector!");
            return false;
        }
        return true;
    }
}
