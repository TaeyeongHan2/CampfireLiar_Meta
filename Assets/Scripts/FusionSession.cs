using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FusionSession : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkRunner runnerPrefab;
    
    [SerializeField] [ScenePath] private string loginScene;
    [SerializeField] [ScenePath] private string lobbyScene;
    [SerializeField] [ScenePath] private string demoNight;

    //NetworkRunner 인스턴스는 하나만 유지
    public NetworkRunner Runner { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SceneManager.LoadScene(loginScene);
    }
    
    // LoginManager의 방 생성/참가 기능 통합
    public void CreateRoom(string roomName)
    {
        StartCoroutine(ConnectSharedSessionRoutine(roomName));
    }

    public void JoinLobby()
    {
        if (Runner == null)
        {
            Runner = Instantiate(runnerPrefab);
            Runner.AddCallbacks(this);
        }
            
        Runner.JoinSessionLobby(SessionLobby.Shared);
    }
    
    // 기존의 TryConnect 함수를 확장해서 방 이름을 파라미터로 받음
    private IEnumerator ConnectSharedSessionRoutine(string sessionCode)
    {
        SystemManager.Instance?.ToggleInteraction(false);
            
        if (Runner)
            Runner.Shutdown();
                
        Runner = Instantiate(runnerPrefab);
        Runner.AddCallbacks(this);

        var task = Runner.StartGame(
            new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = sessionCode,
                SceneManager = Runner.GetComponent<INetworkSceneManager>(),
                Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath(loginScene))
            });
                
        yield return new WaitUntil(() => task.IsCompleted);

        SystemManager.Instance?.ToggleInteraction(true);
            
        var result = task.Result;
        Debug.Log($"StartGame Result: {result.ShutdownReason}");
        if (!result.Ok)
        {
            Debug.LogWarning(result.ShutdownReason);
        }
    }

    public void TryDisconnect()
    {
        if (Runner != null)
            Runner.Shutdown();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Runner = null;
        if (shutdownReason == ShutdownReason.Ok)
        {
            SceneManager.LoadScene(lobbyScene);
        }
        else
        {
            Debug.LogWarning(shutdownReason);
        }
    }

    // INetworkRunnerCallbacks 구현 (필요한 콜백만 구현)
    #region INetworkRunnerCallbacks

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
        {
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
        }

    #endregion
}
