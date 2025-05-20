using System.Collections;

namespace HTTP
{
    public class Sample_4_PostEchoJson : Sample_Base
    {
        protected override IEnumerator RequestProcess()
        {
            using var webRequest = API_4_PostEchoJson.CreateWebRequest("호두", 7);
            yield return webRequest.SendWebRequest();

            if (ApiBase.ErrorHandling(webRequest))
            {
                yield break;
            }

            var result = ApiBase.GetResultFromJson<API_4_PostEchoJson.Result>(webRequest);
        }
    }
}