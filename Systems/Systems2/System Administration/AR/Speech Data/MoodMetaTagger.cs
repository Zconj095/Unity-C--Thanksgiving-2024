using System.Collections.Generic;
using UnityEngine; // Required for AudioClip
public class MoodMetaTagger
{
    public static Dictionary<int, string> MetaTagMoods(AudioClip clip, int segmentLength)
    {
        float[][] segments = SpeechSegmenter.SegmentAudio(clip, segmentLength);
        Dictionary<int, string> moodTags = new Dictionary<int, string>();

        for (int i = 0; i < segments.Length; i++)
        {
            float pitch = ExtendedFeatureExtractor.ExtractPitch(segments[i], clip.frequency);
            float energy = ExtendedFeatureExtractor.ExtractEnergy(segments[i]);
            float volume = ExtendedFeatureExtractor.ExtractVolume(segments[i]);
            float speechRate = ExtendedFeatureExtractor.CalculateSpeechRate(segments[i], clip.frequency);
            float[] spectralContent = ExtendedFeatureExtractor.ExtractSpectralContent(segments[i]);

            string mood = MoodClassifier.ClassifyMood(pitch, energy, volume, speechRate, spectralContent);
            moodTags[i] = mood;
        }

        return moodTags;
    }
}
