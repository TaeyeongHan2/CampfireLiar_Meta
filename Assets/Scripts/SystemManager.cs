using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Light mainLight;
    
    public static SystemManager Instance;

    private void Awake()
    {
        Instance = this;

        //NetworkSceneManagerDefault.sceneLoaded += OnSceneLoded;
    }

    // private void OnSceneLoded(Scene scene)
    // {
    //     if (scene.name.Contains("Room"))
    //     {
    //         
    //     }
    // }

    public void ToggleInteraction(bool isOn)
    {
        eventSystem.enabled = isOn;
    }
    
}
