using UnityEngine;

public class RealTimeSpeechProcessor : MonoBehaviour
{
    public AudioSource audioSource;

    private AudioClip recordedClip;
    private int sampleRate = 44100;

    void Start()
    {
        // Start recording audio from the default microphone
        recordedClip = Microphone.Start(null, true, 10, sampleRate);
    }

    void Update()
    {
        if (Microphone.IsRecording(null))
        {
            // Extract audio samples
            float[] audioSamples = new float[recordedClip.samples];
            recordedClip.GetData(audioSamples, 0);

            // Process attributes
            float tone = SpeechAttributeProcessor.CalculateTone(audioSamples);
            float density = SpeechAttributeProcessor.CalculateDensity(audioSamples);
            float clarity = SpeechAttributeProcessor.CalculateClarity(audioSamples, sampleRate);

            // Perform spectral analysis
            float[] spectralContent = FeatureExtractor.ExtractSpectralContent(audioSamples);
            float crispness = SpeechAttributeProcessor.CalculateCrispness(spectralContent);
            float bass = SpeechAttributeProcessor.CalculateBass(spectralContent);
            float treble = SpeechAttributeProcessor.CalculateTreble(spectralContent);

            float ambience = SpeechAttributeProcessor.CalculateAmbience(audioSamples, sampleRate);

            // Log the results
            Debug.Log($"Tone: {tone}, Density: {density}, Clarity: {clarity}, Crispness: {crispness}");
            Debug.Log($"Bass: {bass}, Treble: {treble}, Ambience: {ambience}");
        }
    }
}
