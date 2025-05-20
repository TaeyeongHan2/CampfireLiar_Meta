using HTTP;
using UnityEngine;
using UnityEngine.UI;

public class CatagoryButtonAction : MonoBehaviour
{
    [SerializeField]
    private PostWWWForm form;
    
    public void SetCategoryFromButton(GameObject buttonObject)
    {
        // 버튼 안에 있는 Text 컴포넌트 찾기
        Text text = buttonObject.GetComponentInChildren<Text>();
        if (text != null)
            GameData.Catagory = text.text;
        form.SendRequest();
    }
}
