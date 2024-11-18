using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingSystem : MonoBehaviour
{
    // Variables for sound detection
    public float hearingRange = 10f;    // The range within which the AI can hear sounds
    public float soundDetectionThreshold = 0.5f; // The threshold for detecting a sound (volume)

    // This could be a list of sound sources (optional)
    private List<AudioSource> soundSources = new List<AudioSource>();

    // Called every frame to check for sounds within hearing range
    void Update()
    {
        ListenForSounds();
    }

    // This method simulates the hearing process
    private void ListenForSounds()
    {
        foreach (AudioSource soundSource in soundSources)
        {
            if (soundSource != null && Vector3.Distance(transform.position, soundSource.transform.position) <= hearingRange)
            {
                // Calculate the volume of the sound based on the distance
                float soundVolume = CalculateSoundVolume(soundSource);
                if (soundVolume >= soundDetectionThreshold)
                {
                    HearSound(soundSource, soundVolume);
                }
            }
        }
    }

    // Calculate the volume based on the distance from the sound source
    private float CalculateSoundVolume(AudioSource soundSource)
    {
        // The volume will decrease with distance
        float distance = Vector3.Distance(transform.position, soundSource.transform.position);
        return Mathf.Clamp01(1f - (distance / hearingRange)); // Returns a value between 0 and 1
    }

    // Handle what happens when a sound is heard
    private void HearSound(AudioSource soundSource, float volume)
    {
        Debug.Log($"Heard sound from {soundSource.name} at volume {volume}");

        // Here you can define behavior based on the sound, such as moving towards it or reacting
        ReactToSound(soundSource, volume);
    }

    // Reaction to the sound (e.g., AI movement or response)
    private void ReactToSound(AudioSource soundSource, float volume)
    {
        // Example reaction: Move towards the sound source
        Vector3 directionToSound = (soundSource.transform.position - transform.position).normalized;
        float speed = 3f; // Speed at which the AI moves towards the sound

        // Move AI towards the sound source (simple movement example)
        transform.position = Vector3.MoveTowards(transform.position, soundSource.transform.position, speed * Time.deltaTime);
        
        // Optionally, you can add sound-specific behaviors based on volume or other factors
    }

    // Method to add a new sound source (if you're spawning sound sources dynamically)
    public void AddSoundSource(AudioSource newSource)
    {
        if (!soundSources.Contains(newSource))
        {
            soundSources.Add(newSource);
        }
    }

    // Method to remove a sound source (if needed)
    public void RemoveSoundSource(AudioSource oldSource)
    {
        if (soundSources.Contains(oldSource))
        {
            soundSources.Remove(oldSource);
        }
    }
}
