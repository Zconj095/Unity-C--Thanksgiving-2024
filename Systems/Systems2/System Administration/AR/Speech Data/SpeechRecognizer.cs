using UnityEngine;

public class SpeechRecognizer : MonoBehaviour
{
    public string RecognizeSpeech(AudioClip clip)
    {
        float[] audioData = new float[clip.samples];
        clip.GetData(audioData, 0);

        // Analyze the waveform (mock analysis for demo)
        string recognizedWord = MatchAudioToWord(audioData);
        return recognizedWord;
    }

    private string MatchAudioToWord(float[] audioData)
    {
        // Mock: Match patterns based on simple rules
        float avgAmplitude = 0f;
        foreach (var sample in audioData)
        {
            avgAmplitude += Mathf.Abs(sample);
        }
        avgAmplitude /= audioData.Length;

        if (avgAmplitude > 0.05f) return "hello";
        return "unknown";
    }
}
