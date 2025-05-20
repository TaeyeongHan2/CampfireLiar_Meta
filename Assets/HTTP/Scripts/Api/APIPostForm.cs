using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace HTTP
{
    public class APIPostForm : ApiBase
    {
        private static string Uri => $"{Common.Domain}/image";

        public static UnityWebRequest CreateWebRequestWithWWWForm(string keyword1, string keyword2, string keyword3, string category)
        {
            var formData = new WWWForm();
            formData.AddField("KeyWord1", keyword1);
            formData.AddField("KeyWord2", keyword2);
            formData.AddField("KeyWord3", keyword3);
            formData.AddField("Category", category);
                
            var webRequest = UnityWebRequest.Post(Uri, formData);
            return webRequest;
        }
            
        public static UnityWebRequest CreateWebRequestWithMultipartForm(string name, int age)
        {
            var formData = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("name", name),
                new MultipartFormDataSection("age", $"{age + Random.Range(0, 10)}")
            };
                
            var webRequest = UnityWebRequest.Post(Uri, formData);
            return webRequest;
        }

        public class Result : ResultBase
        {
            public string imageUrl;
            public string description;
            public string answer;
        }
    }
}