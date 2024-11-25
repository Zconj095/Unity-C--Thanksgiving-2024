using UnityEngine;
using UnityEngine.UI;

public class EmotionInputManager : MonoBehaviour
{
    public EmotionLayoutController emotionController;

    public Slider pitchSlider;
    public Slider energySlider;
    public InputField spectralContentInput;

    void Start()
    {
        pitchSlider.onValueChanged.AddListener(UpdateEmotionData);
        energySlider.onValueChanged.AddListener(UpdateEmotionData);
    }

    public void UpdateEmotionData(float value)
    {
        float pitch = pitchSlider.value;
        float energy = energySlider.value;
        float[] spectralContent = ParseSpectralContent(spectralContentInput.text);

        // Default or calculated speechRate
        float speechRate = 1.0f; // Placeholder value

        emotionController.ProcessEmotionData(pitch, energy, 0, speechRate, spectralContent);
    }

    private float[] ParseSpectralContent(string input)
    {
        string[] parts = input.Split(',');
        float[] content = new float[3];
        for (int i = 0; i < Mathf.Min(parts.Length, 3); i++)
        {
            float.TryParse(parts[i], out content[i]);
        }
        return content;
    }
}
