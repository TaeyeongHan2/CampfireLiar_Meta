using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

//게임 실행 시 서버 접속 및 룸 생성하는 스크립트
public class NetworkStarter : MonoBehaviour
{
    public NetworkRunner runnerPrefab;

    private void Start()
    {
        StartGame();
    }

    async void StartGame()
    {
        var runner = Instantiate(runnerPrefab);
        runner.ProvideInput = true;

        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared, 
            SessionName = "MyRoom",
            Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
            SceneManager = runner.GetComponent<NetworkSceneManagerDefault>()
        });
    }
}

