using UnityEngine;

public class SpeechEncryptor : MonoBehaviour
{
    private VocalEncryptor encryptor = new VocalEncryptor();

    public byte[] EncryptSpeech(AudioClip clip)
    {
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);
        return encryptor.EncryptAudio(samples);
    }

    public AudioClip DecryptSpeech(byte[] encryptedData, int sampleRate)
    {
        float[] samples = encryptor.DecryptAudio(encryptedData);
        AudioClip clip = AudioClip.Create("DecryptedSpeech", samples.Length, 1, sampleRate, false);
        clip.SetData(samples, 0);
        return clip;
    }
}
