using System.IO;
using UnityEngine;
// Unity의 AudioClip을 WAV 파일 포맷으로 변환하는 것
namespace HTTP
{
    public static class Common
    {
        // HTTP 통신에 사용할 URL의 도메인
        public const string Domain = "http://192.168.0.23:8080";
        
        //Audio를 받아 WAV 파일로 변환해주는 메서드
        public static byte[] AudioClipToWav(AudioClip clip)
        {
            var samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);

            byte[] wavData = ConvertAudioClipDataToWav(samples, clip.channels, clip.frequency);
            return wavData;
        }
        // WAV 파일 포맷에 맞춰 바이트 배열을 생성하는 메서드입니다.
        private static byte[] ConvertAudioClipDataToWav(float[] samples, int channels, int sampleRate)
        {
            int byteRate = sampleRate * channels * 2;
            int fileSize = 44 + samples.Length * 2;
                 
            var stream = new MemoryStream();
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(System.Text.Encoding.UTF8.GetBytes("RIFF"));
                writer.Write(fileSize - 8);
                writer.Write(System.Text.Encoding.UTF8.GetBytes("WAVE"));
                writer.Write(System.Text.Encoding.UTF8.GetBytes("fmt "));
                writer.Write(16);
                writer.Write((short)1);
                writer.Write((short)channels);
                writer.Write(sampleRate);
                writer.Write(byteRate);
                writer.Write((short)(channels * 2));
                writer.Write((short)16);

                writer.Write(System.Text.Encoding.UTF8.GetBytes("data"));
                writer.Write(samples.Length * 2);

                foreach (float sample in samples)
                {
                    short intSample = (short)(Mathf.Clamp(sample, -1f, 1f) * short.MaxValue);
                    writer.Write(intSample);
                }
            }

            return stream.ToArray();
        }
    }
}