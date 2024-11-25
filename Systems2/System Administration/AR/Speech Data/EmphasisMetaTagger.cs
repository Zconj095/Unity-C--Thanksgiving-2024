using System.Collections.Generic;
using UnityEngine;

public class EmphasisMetaTagger
{
    public static Dictionary<int, string> TagEmphasis(AudioClip clip, int segmentLengthMs)
    {
        int sampleRate = clip.frequency;
        int samplesPerSegment = (segmentLengthMs * sampleRate) / 1000;
        int totalSegments = clip.samples / samplesPerSegment;

        Dictionary<int, string> emphasisTags = new Dictionary<int, string>();
        float[] audioSamples = new float[clip.samples];
        clip.GetData(audioSamples, 0);

        for (int i = 0; i < totalSegments; i++)
        {
            int segmentStart = i * samplesPerSegment;
            float[] segmentSamples = new float[samplesPerSegment];
            System.Array.Copy(audioSamples, segmentStart, segmentSamples, 0, samplesPerSegment);

            // Extract features for the segment
            float pitch = FeatureExtractor.ExtractPitch(segmentSamples, sampleRate);
            float energy = FeatureExtractor.ExtractEnergy(segmentSamples);
            float volume = FeatureExtractor.ExtractVolume(segmentSamples);
            float speechRate = FeatureExtractor.CalculateSpeechRate(segmentSamples, sampleRate);
            float[] spectralContent = FeatureExtractor.ExtractSpectralContent(segmentSamples);

            // Classify emphasis
            string emphasis = EmphasisClassifier.ClassifyEmphasis(pitch, energy, volume, speechRate, spectralContent);

            // Tag the segment with the classified emphasis
            emphasisTags[i] = emphasis;
        }

        return emphasisTags;
    }
}
