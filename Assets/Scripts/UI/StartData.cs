using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StartData : MonoBehaviour
{
    private StringBuilder _stringBuilder = new();
    [SerializeField]
    private Text keywordText;

    private void OnEnable()
    {
        TextUpdate();
    }

    private void Update()
    {
        TextUpdate();
    }
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

        keywordText.text = _stringBuilder.ToString();
    }
}
