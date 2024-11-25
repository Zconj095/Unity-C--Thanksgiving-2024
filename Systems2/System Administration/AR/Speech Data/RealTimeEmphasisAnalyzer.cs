using UnityEngine;

public class RealTimeEmphasisAnalyzer : MonoBehaviour
{
    public AudioSource audioSource;

    private AudioClip recordedClip;
    private int sampleRate = 44100; // Default sample rate for microphone input

    void Start()
    {
        // Start recording audio from the default microphone
        recordedClip = Microphone.Start(null, true, 10, sampleRate); // Record continuously for 10 seconds
    }

    void Update()
    {
        if (Microphone.IsRecording(null))
        {
            // Extract audio samples from the recorded clip
            float[] audioSamples = new float[recordedClip.samples];
            recordedClip.GetData(audioSamples, 0);

            // Extract features from the audio
            float pitch = FeatureExtractor.ExtractPitch(audioSamples, sampleRate);
            float energy = FeatureExtractor.ExtractEnergy(audioSamples);
            float volume = FeatureExtractor.ExtractVolume(audioSamples);
            float speechRate = FeatureExtractor.CalculateSpeechRate(audioSamples, sampleRate);
            float[] spectralContent = FeatureExtractor.ExtractSpectralContent(audioSamples);

            // Classify the emphasis using the EmphasisClassifier
            string emphasisType = EmphasisClassifier.ClassifyEmphasis(pitch, energy, volume, speechRate, spectralContent);

            // Log the results
            Debug.Log($"Pitch: {pitch}, Energy: {energy}, Volume: {volume}, Speech Rate: {speechRate}");
            Debug.Log($"Spectral Content (Low/Mid/High): [{spectralContent[0]}, {spectralContent[1]}, {spectralContent[2]}]");
            Debug.Log($"Detected Emphasis: {emphasisType}");
        }
    }
}
