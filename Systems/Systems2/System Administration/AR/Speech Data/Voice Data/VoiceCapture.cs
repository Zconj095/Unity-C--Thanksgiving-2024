using UnityEngine;

public class VoiceCapture : MonoBehaviour
{
    private AudioClip recordedClip;
    private string microphone;

    void Start()
    {
        microphone = Microphone.devices[0];
        StartRecording();
    }

    void StartRecording()
    {
        recordedClip = Microphone.Start(microphone, true, 10, 44100);
        Debug.Log("Recording started...");
    }

    void StopRecording()
    {
        Microphone.End(microphone);
        Debug.Log("Recording stopped.");
    }

    public AudioClip GetRecordedClip()
    {
        return recordedClip;
    }
}
