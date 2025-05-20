using UnityEngine;
using UnityEngine.Networking;

namespace HTTP
{
    // HTTP 요청 결과를 공통적으로 처리하는 유틸리티 클래스
    // 여러 개의 API 요청 처리에서 반복되는 에러 처리와 JSON 파싱 로직을 재사용 가능하게 만들어 둔 것
    public class ApiBase
    {
        // Unity의 요청 결과가 성공이 아니면 (통신 오류, 404, 서버 에러 등) 에러를 로그로 출력
        public static bool ErrorHandling(UnityWebRequest request)
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"{request.uri} 실패\n{request.error}\n{request.downloadHandler.text}");
                return true;
            }
            return false;
        }

        // 다양한 API 응답 타입을 처리할 수 있게 설계됨
        // 단 응답 클래스는 반드시 ResultBase를 상속해야 함
        public static T GetResultFromJson<T>(UnityWebRequest webRequest) where T : ResultBase
        {
            var resultText = webRequest.downloadHandler.text;
            var result = ResultBase.FromJson<T>(resultText);
            return result;
        }
    }
}