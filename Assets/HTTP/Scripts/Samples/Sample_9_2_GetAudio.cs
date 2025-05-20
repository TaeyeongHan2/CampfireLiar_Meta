using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HTTP
{
    public class Sample_9_2_GetAudio : Sample_Base
    {
        public AudioSource audioSource;

        protected override IEnumerator RequestProcess()
        {
            using var webRequest = API_9_GetFileWithToken.CreateAudioClipWebRequest(
                Sample_6_2_UploadAudio.LatestUploadFilename,
                Sample_8_Login.LatestAccessToken);
            yield return webRequest.SendWebRequest();

            if (ApiBase.ErrorHandling(webRequest))
            {
                yield break;
            }
            
            var audioClip = API_9_GetFileWithToken.GetAudioClip(webRequest);
            audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}