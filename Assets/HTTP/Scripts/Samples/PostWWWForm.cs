using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace HTTP
{
    public class PostWWWForm : Sample_Base
    {
        public Sprite sprite;
        public GameUIData gameUIData;
        
        protected override IEnumerator RequestProcess()
        {
            while (GameData.PutPrefab.Count <= 2)
                GameData.PutPrefab.Add("");
            using var webRequest = APIPostForm.CreateWebRequestWithWWWForm(GameData.PutPrefab[0], GameData.PutPrefab[1], GameData.PutPrefab[2], GameData.Catagory);
            // using var webRequest = APIPostForm.CreateWebRequestWithWWWForm("물", "불", "흙", "직업");
            yield return webRequest.SendWebRequest();
            
            if (ApiBase.ErrorHandling(webRequest))
            {
                Debug.Log("실패");
                Single.System.SceneManager.LoadScene(SceneDataType.PostError);
                yield break;
            }
            
            var result = ApiBase.GetResultFromJson<APIPostForm.Result>(webRequest);
            Debug.Log("받은 제목: " + result.answer);
            Debug.Log("받은 설명: " + result.description);
            Debug.Log("이미지 URL: " + result.imageUrl);
            ResultData.answer = result.answer;
            ResultData.description = result.description;
            
            Description.text = result.description;
            
            Debug.Log(result.ToString());
            StartCoroutine(DownloadAndSetImage(result.imageUrl));
        }
        private IEnumerator DownloadAndSetImage(string imageUrl)
        {
            UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return imageRequest.SendWebRequest();

            if (imageRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("이미지 다운로드 실패: " + imageRequest.error);
                yield break;
            }

            Texture2D texture = DownloadHandlerTexture.GetContent(imageRequest);
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            sprite = Sprite.Create(texture, rect, pivot);
            Image.sprite = sprite;
            gameUIData.TextUpdate();
            Single.System.SceneManager.LoadScene(SceneDataType.Game);
        }
    }
}