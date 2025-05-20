using System.Collections;
using UnityEngine.UI;

namespace HTTP
{
    public class Sample_9_1_GetTexture : Sample_Base
    {
        public Image image;

        protected override IEnumerator RequestProcess()
        {
            using var webRequest = API_9_GetFileWithToken.CreateTextureWebRequest(
                Sample_6_1_UploadTexture.LatestUploadFilename,
                Sample_8_Login.LatestAccessToken);
            yield return webRequest.SendWebRequest();

            if (ApiBase.ErrorHandling(webRequest))
            {
                yield break;
            }
            
            image.sprite = API_9_GetFileWithToken.GetSprite(webRequest);
        }
    }
}