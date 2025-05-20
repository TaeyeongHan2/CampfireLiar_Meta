using UnityEngine;

public class StartButtonAction : MonoBehaviour
{
    [SerializeField]
    private StartData startData;
    
    public void StartButton()
    {
        if (GameData.PutPrefab.Count <= 0)
        {
            Single.System.SceneManager.Warning(SceneDataType.NoPrefab, "");
            return;
        }
        
        Single.System.SceneManager.LoadScene(SceneDataType.Catagory);
    }

    public void DeleteButton()
    {
        if (GameData.PutPrefab.Count <= 0)
        {
            Single.System.SceneManager.Warning(SceneDataType.NoPrefab, "");
            return;
        }
        GameData.PutPrefab.RemoveAt(GameData.PutPrefab.Count - 1);
        GameData.PutCount--;
        startData.TextUpdate();
    }
}
