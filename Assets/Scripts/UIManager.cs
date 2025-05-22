using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject goToLobbyButton;
    
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;

        //NetworkSceneManagerDefault.sceneLoaded += OnSceneLoaded;
        
        
    }

    // private void OnSceneLoaded(Scene scene)
    // {
    //     if (scene.name.Contains("LoginScene"))
    //     {
    //         TogglePlayButton(true);
    //     }
    //
    //     if (scene.name.Contains("LobbyScene"))
    //     {
    //         TogglePlayButton(false);
    //     }
    // }

    public void TogglePlayButton(bool isOn)
    {
        goToLobbyButton.SetActive(isOn);
    }
}
