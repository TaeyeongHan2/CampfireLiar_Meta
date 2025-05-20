using System.Collections;
using UnityEngine;

namespace HTTP
{
    public class Sample_7_2_GetAudio : Sample_Base
    {
        public AudioSource audioSource;

        protected override IEnumerator RequestProcess()
        {
            using var webRequest = API_7_GetFile.CreateAudioClipWebRequest(Sample_6_2_UploadAudio.LatestUploadFilename);
            yield return webRequest.SendWebRequest();

            if (ApiBase.ErrorHandling(webRequest))
            {
                yield break;
            }
            
            var audioClip = API_7_GetFile.GetAudioClip(webRequest);
            audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}