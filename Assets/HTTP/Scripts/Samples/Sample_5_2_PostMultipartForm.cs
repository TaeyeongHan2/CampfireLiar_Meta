using System.Collections;

namespace HTTP
{
    public class Sample_5_2_PostMultipartForm : Sample_Base
    {
        protected override IEnumerator RequestProcess()
        {
            using var webRequest = APIPostForm.CreateWebRequestWithMultipartForm("호두", 7);
            yield return webRequest.SendWebRequest();

            if (ApiBase.ErrorHandling(webRequest))
            {
                yield break;
            }
            
            var result = ApiBase.GetResultFromJson<APIPostForm.Result>(webRequest);
        }
    }
}