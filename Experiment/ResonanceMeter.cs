using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResonanceMeter : MonoBehaviour
{
    public Slider resonanceSlider;
    public ChakraSystem chakraSystem; // Reference to the ChakraSystem
    public SpiritualEnergy spiritualEnergy; // Reference to the SpiritualEnergy
    public CharacterAttributePreset characterAttributePreset; // Reference to the Character Attribute Preset
    public PlayerAbilities playerAbilities; // Reference to PlayerAbilities
    public float syncThreshold = 0.1f;
    public float resonanceDecayRate = 0.05f;
    public float resonanceGain = 0.2f;
    public LineRenderer lineRenderer;
    public AudioSource playerAudioSource;
    public AudioClip resonanceBuildupClip;
    public AudioClip resonanceActivationClip;
    public ParticleSystem playerGlowEffect;

    void Start()
    {
        // Existing Start code...
        playerAudioSource.clip = resonanceBuildupClip;
        playerAudioSource.Play();

        // Initialize components if not assigned in the inspector
        if (chakraSystem == null)
        {
            chakraSystem = FindObjectOfType<ChakraSystem>();
            if (chakraSystem == null)
            {
                Debug.LogError("ChakraSystem not found in the scene.");
            }
        }

        if (spiritualEnergy == null)
        {
            spiritualEnergy = FindObjectOfType<SpiritualEnergy>();
            if (spiritualEnergy == null)
            {
                Debug.LogError("SpiritualEnergy not found in the scene.");
            }
        }

        if (characterAttributePreset == null)
        {
            characterAttributePreset = FindObjectOfType<CharacterAttributePreset>();
            if (characterAttributePreset == null)
            {
                Debug.LogError("CharacterAttributePreset not found in the scene.");
            }
        }

        if (playerAbilities == null)
        {
            playerAbilities = GetComponent<PlayerAbilities>();
            if (playerAbilities == null)
            {
                Debug.LogError("PlayerAbilities not found in the scene.");
            }
        }
    }

    void Update()
    {
        // Existing Update code...

        // Adjust player movement speed based on chakra energies
        if (playerAbilities != null && chakraSystem != null)
        {
            playerAbilities.AdjustMovementSpeedBasedOnChakras(chakraSystem);
        }

        // Check for resonance completion
        if (resonanceSlider.value >= resonanceSlider.maxValue)
        {
            // Check if enough spiritual energy is available to trigger resonance
            if (spiritualEnergy != null && spiritualEnergy.IsEnergyAvailable(20f)) // Example cost for resonance
            {
                TriggerSoulResonance();
                resonanceSlider.value = 0; // Reset the meter
            }
            else
            {
                Debug.Log("Not enough spiritual energy to activate Soul Resonance!");
            }
        }

        // Update chakra energy based on resonance
        if (chakraSystem != null)
        {
            chakraSystem.AddEnergyToChakra(0, resonanceGain * Time.deltaTime); // Example: Adding energy to Root Chakra
        }
    }

    void TriggerSoulResonance()
    {
        Debug.Log("Soul Resonance Activated!");

        playerAudioSource.PlayOneShot(resonanceActivationClip);

        if (playerAbilities != null)
        {
            playerAbilities.BoostAbilities();
        }

        // Deduct energy cost for resonance
        if (spiritualEnergy != null)
        {
            spiritualEnergy.UseEnergy(20f); // Deduct the cost from spiritual energy
        }

        // Apply character attributes from the preset
        if (characterAttributePreset != null)
        {
            characterAttributePreset.ApplyAttributes(playerAbilities, chakraSystem);
        }

        // Start visual effects or other independent systems here
        StartCoroutine(SoulResonanceEffects());
    }

    IEnumerator SoulResonanceEffects()
    {
        yield return new WaitForSeconds(5f); // Duration of resonance effects
        
        // Here you might want to align or balance chakras
        if (chakraSystem != null)
        {
            chakraSystem.AlignChakras(); // Optional: Align chakras after resonance effects
        }

        Debug.Log("Soul Resonance Effects Completed");
    }
}
