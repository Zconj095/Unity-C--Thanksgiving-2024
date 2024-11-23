using UnityEngine;

public class SpatialAndTemporalProcessor : MonoBehaviour
{
    // AudioSource to process
    private AudioSource audioSource;

    void Start()
    {
        // Ensure there is an AudioSource component attached to the GameObject
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Process audio effects for demonstration
        ProcessAudioEffects();
    }

    /// <summary>
    /// Applies spatial positioning to the audio source.
    /// </summary>
    public static void ApplySpatialProcessing(AudioSource audioSource, Vector3 position)
    {
        audioSource.spatialBlend = 1.0f; // Enable 3D audio
        audioSource.transform.position = position;
    }

    /// <summary>
    /// Applies temporal effects like delay or echo to the audio source.
    /// </summary>
    public static void ApplyTemporalProcessing(AudioSource audioSource, float delay, float feedback)
    {
        AudioEchoFilter echo = audioSource.gameObject.AddComponent<AudioEchoFilter>();
        echo.delay = delay;
        echo.decayRatio = feedback;
    }

    void ProcessAudioEffects()
    {
        // Ensure audioSource exists
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned.");
            return;
        }

        // Spatial processing
        SpatialAndTemporalProcessor.ApplySpatialProcessing(audioSource, new Vector3(0, 0, 5));

        // Temporal processing
        SpatialAndTemporalProcessor.ApplyTemporalProcessing(audioSource, 500f, 0.5f);
    }
}
