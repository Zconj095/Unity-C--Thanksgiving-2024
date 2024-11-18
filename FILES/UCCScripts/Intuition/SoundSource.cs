using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Set up an AudioSource on this object
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("sound_file");  // Load your sound here
        audioSource.loop = true; // Set the sound to loop
        audioSource.Play();  // Start the sound immediately
    }
}
