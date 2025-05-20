using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Single
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private List<SceneData> _scenes;
        [SerializeField]
        private Text WarningText;
        [SerializeField]
        private Text DuplicateText;
        private GameObject prev;
        public bool InGameCheck = false;

        [SerializeField] public Text InputText;
        [SerializeField] public Text result;

        public void LoadSceneResult(SceneDataType sceneDataType, bool Result, string inputText, float similar)
        {
            foreach (SceneData scene in _scenes)
            {
                if (scene.SceneDataType == sceneDataType)
                {
                    //정답
                    if (Result)
                    {
                        string inputMessage = $"입력 : {inputText}";
                        InputText.text = inputMessage;
                        string resultMessage = $"정답입니다. 정답은 {ResultData.answer} 이었습니다.";
                        result.text = resultMessage;
                    }
                    //오답
                    else
                    {
                        string inputMessage = $"입력 : {inputText}";
                        InputText.text = inputMessage;
                        string resultMessage = $"오답입니다. 정답 유사율 {similar:F2}%입니다.";
                        result.text = resultMessage; 
                    }
                    scene.SceneObject.SetActive(true);
                }
                else
                    scene.SceneObject.SetActive(false);
            }
        }
        public void LoadScene(SceneDataType sceneDataType)
        {
            foreach (SceneData sceneData in _scenes)
            {
                if (sceneData.SceneDataType == sceneDataType)
                {
                    sceneData.SceneObject.SetActive(true);
                }
                else
                    sceneData.SceneObject.SetActive(false);
            }
        }

        public void Warning(SceneDataType sceneDataType, string Duplicate)
        {
            foreach (SceneData scene in _scenes)
            {
                if (scene.SceneObject.activeSelf)
                {
                    prev = scene.SceneObject;
                    scene.SceneObject.SetActive(false); // 2. 전부 끔
                }
            }
            
            foreach (SceneData sceneData in _scenes)
            {
                if (sceneData.SceneDataType == sceneDataType)
                {
                    sceneData.SceneObject.SetActive(true);
                    if (sceneDataType == SceneDataType.Warning)
                        WarningText.text = $"항아리 안의 키워드 3개 이상 넣을 수 없습니다.\n현재 키워드: {string.Join(", ", GameData.PutPrefab)}";
                    else if (sceneDataType == SceneDataType.Duplicate)
                    {
                        DuplicateText.text = $"중복된 단어를 넣을 수 없습니다.\n중복 키워드: {Duplicate}";
                    }
                    // 흔들기 애니메이션 시작
                    var rectTransform = sceneData.SceneObject.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        rectTransform.anchoredPosition = Vector2.zero; // 원위치로 초기화
                        rectTransform.DOShakeAnchorPos(
                            duration: 3f,
                            strength: new Vector2(30f, 0f), // 좌우 흔들기
                            vibrato: 20,
                            randomness: 90,
                            snapping: false,
                            fadeOut: true
                        );
                    }

                    // 3초 후 비활성화
                    StartCoroutine(HideAfterSeconds(sceneData.SceneObject, 3f));
                }
            }
        }

        private IEnumerator HideAfterSeconds(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            obj.SetActive(false);
            prev.SetActive(true);
        }

        public void UnLoadScene()
        {
            foreach (SceneData sceneData in _scenes)
            {
                sceneData.SceneObject.SetActive(false);
            }
        }
    }
}