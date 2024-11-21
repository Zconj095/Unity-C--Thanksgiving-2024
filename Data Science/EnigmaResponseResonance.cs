using UnityEngine;

public class EnigmaResponseResonance : MonoBehaviour
{
    // Resonance parameters
    [SerializeField] private float resonanceFrequency = 1.0f; // Base frequency for resonance effect
    private float currentResonanceStrength = 0f; // Current strength of the resonance effect
    private bool isResonating = false; // Is resonance active?

    // Visual and audio feedback
    [SerializeField] private Material resonanceMaterial; // Material to modify during resonance
    [SerializeField] private AudioSource resonanceAudioSource; // Audio source for resonance sound
    private Renderer objectRenderer; // Renderer for accessing material properties

    // Puzzle interaction parameters
    [SerializeField] private GameObject puzzleObject; // Object associated with triggering resonance
    [SerializeField, Range(0f, 1f)] private float puzzleInteractionThreshold = 0.75f; // Interaction threshold for resonance

    // Resonance duration control
    [SerializeField] private float resonanceDuration = 5f; // Duration for resonance effect
    private float resonanceTimer = 0f; // Timer to track resonance duration

    void Start()
    {
        // Initialize object renderer and ensure material/audio source are properly set up
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogWarning("Renderer not found. Resonance material effects may not work.");
        }

        if (resonanceAudioSource != null)
        {
            resonanceAudioSource.Stop();
        }
        else
        {
            Debug.LogWarning("Resonance audio source is not assigned.");
        }
    }

    void Update()
    {
        // Calculate the interaction strength (replace this logic with actual game mechanics)
        float interactionStrength = GetPuzzleInteractionStrength();

        // Activate resonance if interaction strength exceeds the threshold
        if (interactionStrength > puzzleInteractionThreshold)
        {
            StartResonance();
        }

        // Update resonance effects while active
        if (isResonating)
        {
            UpdateResonanceEffects();

            // Stop resonance after the specified duration
            resonanceTimer += Time.deltaTime;
            if (resonanceTimer >= resonanceDuration)
            {
                StopResonance();
            }
        }
        else
        {
            // Reset resonance effects when inactive
            ResetResonanceEffects();
        }
    }

    /// <summary>
    /// Simulate puzzle interaction strength.
    /// Replace this with real interaction logic in your game.
    /// </summary>
    private float GetPuzzleInteractionStrength()
    {
        // Example: Simulate interaction strength using a ping-pong effect over time
        return Mathf.PingPong(Time.time * 0.5f, 1.0f);
    }

    /// <summary>
    /// Starts the resonance effect.
    /// </summary>
    private void StartResonance()
    {
        if (!isResonating)
        {
            isResonating = true;
            resonanceTimer = 0f;

            // Play audio feedback if available
            resonanceAudioSource?.Play();
        }
    }

    /// <summary>
    /// Updates the resonance effects based on the current strength.
    /// </summary>
    private void UpdateResonanceEffects()
    {
        currentResonanceStrength = Mathf.PingPong(Time.time * resonanceFrequency, 1.0f);
        ApplyResonanceEffects(currentResonanceStrength);
    }

    /// <summary>
    /// Stops the resonance effect and resets the timer.
    /// </summary>
    private void StopResonance()
    {
        isResonating = false;
        resonanceTimer = 0f;

        // Stop audio feedback if playing
        resonanceAudioSource?.Stop();
    }

    /// <summary>
    /// Applies visual and other effects based on the resonance strength.
    /// </summary>
    private void ApplyResonanceEffects(float strength)
    {
        if (resonanceMaterial != null)
        {
            // Adjust material properties based on resonance strength
            resonanceMaterial.SetFloat("_Glossiness", strength);
            resonanceMaterial.SetColor("_Color", Color.Lerp(Color.white, Color.blue, strength));
        }

        // Additional effects can be added here, such as lighting, particle effects, or physics interactions
    }

    /// <summary>
    /// Resets visual and other effects when resonance stops.
    /// </summary>
    private void ResetResonanceEffects()
    {
        if (resonanceMaterial != null)
        {
            resonanceMaterial.SetFloat("_Glossiness", 0f);
            resonanceMaterial.SetColor("_Color", Color.white);
        }
    }
}
