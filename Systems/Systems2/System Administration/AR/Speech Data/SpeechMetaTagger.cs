using System.Collections.Generic;
using UnityEngine;

public class SpeechMetaTagger
{
    public static Dictionary<int, string> MetaTagSpeech(AudioClip clip, int segmentLength)
    {
        float[][] segments = SpeechSegmenter.SegmentAudio(clip, segmentLength);
        Dictionary<int, string> metaTags = new Dictionary<int, string>();

        for (int i = 0; i < segments.Length; i++)
        {
            float pitch = FeatureExtractor.ExtractPitch(segments[i], clip.frequency);
            float energy = FeatureExtractor.ExtractEnergy(segments[i]);
            float speechRate = FeatureExtractor.CalculateSpeechRate(segments[i], clip.frequency);
            float[] spectralContent = FeatureExtractor.ExtractSpectralContent(segments[i]);

            string emotion = EmotionClassifier.ClassifyEmotion(pitch, energy, 0, speechRate, spectralContent);
            metaTags[i] = emotion;
        }

        return metaTags;
    }
}
