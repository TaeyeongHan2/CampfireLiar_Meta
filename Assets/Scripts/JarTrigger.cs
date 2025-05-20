using System.Collections.Generic;
using UnityEngine;

public class JarTrigger : MonoBehaviour
{ 
    //테스트용 디버그
    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log($"무언가 들어옴: {other.name}");
    // }
    
    //키워드를 리스트로 관리
    public List<string> collectedKeywords = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        KeywordBlock keywordBlock = other.GetComponent<KeywordBlock>();
        if (keywordBlock != null)
        {
            collectedKeywords.Add(keywordBlock.keyword);
            Debug.Log($"키워드 수집됨: {keywordBlock.keyword}");

            // 블록 제거
            Destroy(other.gameObject);

            // 3개 모이면 처리
            if (collectedKeywords.Count == 3)
            {
                Debug.Log("3개의 키워드 수집 완료!");
                // 여기에 AI 이미지 생성 요청 등의 로직 연결
            }
        }
    }
}