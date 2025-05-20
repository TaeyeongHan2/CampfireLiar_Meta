using UnityEngine;

namespace Single
{
    public class System : MonoBehaviour
    {
        public static System Instance { get; private set; }
        
        public static SceneManager SceneManager => Instance?.sceneManager;
        [SerializeField] private SceneManager sceneManager;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // 이미 있다면 파괴
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
        }
    }
}