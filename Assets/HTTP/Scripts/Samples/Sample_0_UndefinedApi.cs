using System.Collections;

namespace HTTP
{
    // 요청을 보내고 응답을 처리하는 샘플 실행 코드
    public class Sample_0_UndefinedApi : Sample_Base
    {
        protected override IEnumerator RequestProcess()
        {
            // GET 요청 생성
            using var webRequest = API_0_UndefinedApi.CreateWebRequest();
            // 요청을 비동기적으로 전송
            yield return webRequest.SendWebRequest();

            if (ApiBase.ErrorHandling(webRequest))
            {
                yield break;
            }
            // JSON 응답 파싱 및 UI 출력
            var result = ApiBase.GetResultFromJson<API_0_UndefinedApi.Result>(webRequest);
        }
    }
}