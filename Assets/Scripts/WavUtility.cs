using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;

//AudioClip → WAV 변환하는 클래스
// WavUtility.cs (간단 버전)
public static class WavUtility
{
    public static byte[] FromAudioClip(AudioClip clip)
    {
        if (clip == null) throw new ArgumentNullException("clip");

        MemoryStream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);

        int sampleCount = clip.samples * clip.channels;
        float[] samples = new float[sampleCount];
        clip.GetData(samples, 0);

        byte[] wavData = ConvertSamplesTo16BitPCM(samples);
        byte[] header = GetWavHeader(clip, wavData.Length);

        writer.Write(header);
        writer.Write(wavData);

        return stream.ToArray();
    }

    private static byte[] ConvertSamplesTo16BitPCM(float[] samples)
    {
        MemoryStream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);

        foreach (float sample in samples)
        {
            short int16Sample = (short)(Mathf.Clamp(sample, -1f, 1f) * short.MaxValue);
            writer.Write(int16Sample);
        }

        return stream.ToArray();
    }

    private static byte[] GetWavHeader(AudioClip clip, int dataLength)
    {
        int sampleRate = clip.frequency;
        short channels = (short)clip.channels;
        short bitsPerSample = 16;
        int byteRate = sampleRate * channels * bitsPerSample / 8;
        short blockAlign = (short)(channels * bitsPerSample / 8);
        int chunkSize = 36 + dataLength;

        MemoryStream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);

        writer.Write(System.Text.Encoding.ASCII.GetBytes("RIFF")); // Chunk ID
        writer.Write(chunkSize);                                    // Chunk Size
        writer.Write(System.Text.Encoding.ASCII.GetBytes("WAVE")); // Format
        writer.Write(System.Text.Encoding.ASCII.GetBytes("fmt ")); // Subchunk1 ID
        writer.Write(16);                                           // Subchunk1 Size
        writer.Write((short)1);                                     // AudioFormat (PCM)
        writer.Write(channels);                                     // NumChannels
        writer.Write(sampleRate);                                   // SampleRate
        writer.Write(byteRate);                                     // ByteRate
        writer.Write(blockAlign);                                   // BlockAlign
        writer.Write(bitsPerSample);                                // BitsPerSample
        writer.Write(System.Text.Encoding.ASCII.GetBytes("data"));  // Subchunk2 ID
        writer.Write(dataLength);                                   // Subchunk2 Size

        return stream.ToArray();
    }
}



