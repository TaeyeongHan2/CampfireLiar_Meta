using System.Collections;
using UnityEngine.UI;

namespace HTTP
{
    public class Sample_7_1_GetTexture : Sample_Base
    {
        public Image image;

        protected override IEnumerator RequestProcess()
        {
            using var webRequest = API_7_GetFile.CreateTextureWebRequest(Sample_6_1_UploadTexture.LatestUploadFilename);
            yield return webRequest.SendWebRequest();

            if (ApiBase.ErrorHandling(webRequest))
            {
                yield break;
            }
            
            image.sprite = API_7_GetFile.GetSprite(webRequest);
        }
    }
}