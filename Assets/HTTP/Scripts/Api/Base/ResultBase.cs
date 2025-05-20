using Newtonsoft.Json;

namespace HTTP
{
    //HTTP API 응답의 공통 형식을 표현하고, JSON 직렬화 및 역직렬화 처리를 담당하는 기본 클래스
    public class ResultBase
    {
        // 직렬화 시 사용할 설정
        private static readonly JsonSerializerSettings SerializerSettings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        // 역직렬화 시 사용할 설정
        private static readonly JsonSerializerSettings DeserializerSettings = new()
        {
            MissingMemberHandling = MissingMemberHandling.Error,
        };
        
        // 문자열 JSON을 객체로 변환
        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, DeserializerSettings);
        }

        // 객체를 문자열 JSON 문자열로 예쁘게 출력
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializerSettings);
        }
    }
}