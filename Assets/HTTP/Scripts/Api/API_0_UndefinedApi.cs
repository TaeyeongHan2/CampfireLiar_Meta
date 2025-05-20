using UnityEngine.Networking;

namespace HTTP
{
    // 실제 HTTP 요청을 만드는 기능
    // 특정 API에 GET 요청을 보내고, 그 응답을 받을 때 사용할 요청 생성 로직과 결과 데이터 구조를 정의한 클래스
    public class API_0_UndefinedApi : ApiBase
    {
        private static string Uri => $"{Common.Domain}/undefined-api";

        public static UnityWebRequest CreateWebRequest()
        {
            //GET 요청 생성
            var webRequest = UnityWebRequest.Get(Uri);
            webRequest.SetRequestHeader("Content-Type", "text/plain");
            return webRequest;
        }
        
        // 서버 응답 JSON을 객체로 파싱할 때 사용
        public class Result : ResultBase
        {
            public string data;
        }
    }
}