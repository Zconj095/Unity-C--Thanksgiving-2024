using UnityEngine;
using System.Collections.Generic;

public class SpeechNanoTagger
{
    public static Dictionary<float, string> NanoTagSpeech(AudioClip clip, int nanoSegmentLength)
    {
        float[][] nanoSegments = SpeechSegmenter.SegmentAudio(clip, nanoSegmentLength);
        Dictionary<float, string> nanoTags = new Dictionary<float, string>();

        for (int i = 0; i < nanoSegments.Length; i++)
        {
            float pitch = FeatureExtractor.ExtractPitch(nanoSegments[i], clip.frequency);
            float energy = FeatureExtractor.ExtractEnergy(nanoSegments[i]);
            float speechRate = FeatureExtractor.CalculateSpeechRate(nanoSegments[i], clip.frequency);
            float[] spectralContent = FeatureExtractor.ExtractSpectralContent(nanoSegments[i]);

            // Use timestamp (in seconds) as the key
            float timestamp = i * (nanoSegmentLength / 1000f);
            string emotion = EmotionClassifier.ClassifyEmotion(pitch, energy, 0, speechRate, spectralContent);
            nanoTags[timestamp] = emotion;
        }

        return nanoTags;
    }
}
