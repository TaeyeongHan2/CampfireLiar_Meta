using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField nicknameInput;
    public Text statusText;

    public void OnClickConnect()
    {
        string nickname = nicknameInput.text.Trim();
        if (string.IsNullOrEmpty(nickname))
        {
            statusText.text = "닉네임을 입력하세요!";
            return;
        }
        PlayerPrefs.SetString("PlayerNickname", nickname);

        statusText.text = "접속 중...";

        // LobbyScene으로 이동
        SceneManager.LoadScene("LobbyScene");
    }
}