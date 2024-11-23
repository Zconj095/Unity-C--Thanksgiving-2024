using UnityEngine;

public class RealTimeFeatureAnalysis : MonoBehaviour
{
    public AudioSource audioSource;

    private AudioClip recordedClip;

    void Start()
    {
        // Start recording audio
        recordedClip = Microphone.Start(null, true, 10, 44100); // 10 seconds recording
    }

    void Update()
    {
        if (Microphone.IsRecording(null))
        {
            float[] audioSamples = new float[recordedClip.samples];
            recordedClip.GetData(audioSamples, 0);

            int sampleRate = recordedClip.frequency;

            // Extract features
            float pitch = FeatureExtractor.ExtractPitch(audioSamples, sampleRate);
            float energy = FeatureExtractor.ExtractEnergy(audioSamples);
            float volume = FeatureExtractor.ExtractVolume(audioSamples);
            float speechRate = FeatureExtractor.CalculateSpeechRate(audioSamples, sampleRate);
            float[] spectralContent = FeatureExtractor.ExtractSpectralContent(audioSamples);

            // Classify feelings
            string feeling = FeelingClassifier.ClassifyFeeling(pitch, energy, volume, speechRate, spectralContent);

            // Output results
            Debug.Log($"Pitch: {pitch}, Energy: {energy}, Volume: {volume}, SpeechRate: {speechRate}");
            Debug.Log($"Detected Feeling: {feeling}");
        }
    }
}
