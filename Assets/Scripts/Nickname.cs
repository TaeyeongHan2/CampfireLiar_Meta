using NUnit.Framework.Interfaces;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Nickname : MonoBehaviour
{
    public static string Value;
    
    public TMP_InputField nickNameField;

    private void OnEnable()
    {
        nickNameField.text = Value;
        nickNameField.onValueChanged.AddListener(OnChanged);
    }

    private void OnDisable()
    {
        nickNameField.onValueChanged.RemoveListener(OnChanged);
    }

    private void OnChanged(string nickName)
    {
        Nickname.Value = nickName;
    }
}
