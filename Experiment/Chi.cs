using UnityEngine;

public class Chi : MonoBehaviour
{
    public float energy;                   // Current Chi energy
    public float maxEnergy;                // Maximum Chi energy
    public float regenerationRate;         // Rate at which Chi regenerates over time
    public float attackMultiplier;          // Multiplier for attack power when using Chi

    public ParticleSystem chiEffect;       // Particle effect for Chi usage
    public AudioClip chiSound;             // Sound to play when Chi is used
    public AudioSource audioSource;        // Reference to the AudioSource component

    private void Start()
    {
        // Initialize Chi properties
        energy = maxEnergy;  // Start with full Chi energy
    }

    private void Update()
    {
        RegenerateChi();  // Call the regeneration method each frame
    }

    // Method to regenerate Chi over time
    private void RegenerateChi()
    {
        if (energy < maxEnergy)
        {
            energy += regenerationRate * Time.deltaTime;  // Regenerate energy based on the rate
            energy = Mathf.Clamp(energy, 0, maxEnergy);   // Ensure energy doesn't exceed maxEnergy
        }
    }

    // Method to use Chi for an attack
    public void UseChi(float chiAmount)
    {
        if (energy >= chiAmount)
        {
            energy -= chiAmount;  // Deduct the amount of Chi used
            PerformAttack(attackMultiplier);
            PlayChiEffects();     // Play visual and audio effects
        }
        else
        {
            Debug.Log("Not enough Chi energy!");
        }
    }

    // Sample attack method that can be enhanced with Chi
    private void PerformAttack(float multiplier)
    {
        // Your attack logic goes here
        float baseDamage = 10f; // Example base damage
        float totalDamage = baseDamage * multiplier; // Calculate damage with Chi multiplier

        Debug.Log($"Attack performed with {totalDamage} damage using Chi!");
    }

    // Method to play Chi visual and audio effects
    private void PlayChiEffects()
    {
        // Play particle effect
        if (chiEffect != null)
        {
            chiEffect.Play();
        }

        // Play Chi sound
        if (audioSource != null && chiSound != null)
        {
            audioSource.PlayOneShot(chiSound);
        }
    }
}
