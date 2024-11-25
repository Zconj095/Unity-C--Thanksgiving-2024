using UnityEngine;

public class RealTimeTagger : MonoBehaviour
{
    private AudioClip recordedClip;
    private int segmentLength = 500; // 500ms segments

    void Start()
    {
        recordedClip = Microphone.Start(null, true, 10, 44100); // Continuously record
    }

    void Update()
    {
        if (Microphone.IsRecording(null))
        {
            float[][] segments = SpeechSegmenter.SegmentAudio(recordedClip, segmentLength);
            foreach (var segment in segments)
            {
                float pitch = FeatureExtractor.ExtractPitch(segment, 44100);
                float energy = FeatureExtractor.ExtractEnergy(segment);
                float speechRate = FeatureExtractor.CalculateSpeechRate(segment, 44100);
                string emotion = EmotionClassifier.ClassifyEmotion(pitch, energy, 0, speechRate, null);

                Debug.Log($"Real-Time Emotion: {emotion}");
            }
        }
    }
}
