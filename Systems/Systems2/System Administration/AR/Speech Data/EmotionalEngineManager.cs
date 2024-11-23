using UnityEngine;

public class EmotionalEngineManager : MonoBehaviour
{
    public EmotionalFluxEngine fluxEngine = new EmotionalFluxEngine();
    public EmotionalAmbienceEngine ambienceEngine = new EmotionalAmbienceEngine();
    public EmotionalMagnitudeEngine magnitudeEngine = new EmotionalMagnitudeEngine();
    public EmotionalThroughputEngine throughputEngine = new EmotionalThroughputEngine();

    public float pitch;
    public float energy;
    public float volume;
    public float speechRate;
    public float[] spectralContent = new float[3];

    void Update()
    {
        string currentEmotion = EmotionClassifier.ClassifyEmotion(pitch, energy, volume, speechRate, spectralContent);
        fluxEngine.UpdateEmotion(currentEmotion);
        ambienceEngine.SetAmbience(currentEmotion, magnitudeEngine.CalculateMagnitude(pitch, energy, spectralContent));
        throughputEngine.PropagateEmotion(currentEmotion, magnitudeEngine.CalculateMagnitude(pitch, energy, spectralContent));

        Debug.Log($"Emotion: {currentEmotion}");
        Debug.Log($"Flux: {fluxEngine.DetectFlux()}");
        Debug.Log($"Ambience: {ambienceEngine.GetAmbience()}");
        Debug.Log($"Throughput: {throughputEngine.GetStrongestEmotion()}");
    }
}
