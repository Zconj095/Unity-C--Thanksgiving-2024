using UnityEngine;

public class SpeechSegmenter
{
    public static float[][] SegmentAudio(AudioClip clip, int segmentLength)
    {
        int sampleRate = clip.frequency;
        int samplesPerSegment = segmentLength * sampleRate / 1000; // segment length in ms
        int totalSegments = clip.samples / samplesPerSegment;

        float[][] segments = new float[totalSegments][];
        float[] fullAudio = new float[clip.samples];
        clip.GetData(fullAudio, 0);

        for (int i = 0; i < totalSegments; i++)
        {
            segments[i] = new float[samplesPerSegment];
            System.Array.Copy(fullAudio, i * samplesPerSegment, segments[i], 0, samplesPerSegment);
        }

        return segments;
    }
}
