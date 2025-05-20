using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class GameUIData : MonoBehaviour
{
    private StringBuilder _stringBuilder = new();
    [SerializeField]
    private Text keywordText;
    
    public void TextUpdate()
    {
        _stringBuilder.Clear();
        
        foreach (var keyword in GameData.PutPrefab)
        {
            _stringBuilder.Append(keyword);
            _stringBuilder.Append(", ");
        }
        if (_stringBuilder.Length > 2)
            _stringBuilder.Length -= 2;
        _stringBuilder.Append("Category : ");
        _stringBuilder.Append(GameData.Catagory);

        keywordText.text = _stringBuilder.ToString();
    }
}
