
using Single;
using UnityEngine;

public class GameButtonAction  : MonoBehaviour
{
    public void BackButton()
    {
       Single.System.SceneManager.LoadScene(SceneDataType.Game);
    }
}
