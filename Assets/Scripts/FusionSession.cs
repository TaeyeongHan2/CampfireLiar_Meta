using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.PlayerLoop;

public class FusionSession : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkRunner runnerPrefab;
    
    [SerializeField] [ScenePath] private string loginScene;
    [SerializeField] [ScenePath] private string lobbyScene;
    [SerializeField] [ScenePath] private string demoNight;
    
    //NetworkRunner 인스턴스는 하나만 유지
    public NetworkRunner Runner { get; private set; }
    
    //로비씬의 플레이어 프리팹 UI 스폰용 필드
    public GameObject lobbyPlayerUIPrefab;
    //로비 UI 프리팹을 넣을 부모 (Canvas의 Panel)을 할당하는 필드
    public Transform lobbyUIParent;
    
    //닉네임 입력 필드
    public TMP_InputField nicknameInputField;
    //방 이름 입력 필드
    public TMP_InputField roomNameInputField;
    
    //플레이어별로 UI 관리용 Dictionary (PlayerRef → GameObject)
    private Dictionary<PlayerRef, GameObject> lobbyPlayerUIs = new  Dictionary<PlayerRef, GameObject>();

    public LobbySlotManager SlotManager;
    private Dictionary<PlayerRef, int> playerSlotIndexes = new Dictionary<PlayerRef, int>();
    
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
                Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath(lobbyScene))
            });
                
        yield return new WaitUntil(() => task.IsCompleted);

        SystemManager.Instance?.ToggleInteraction(true);
        UIManager.Instance.TogglePlayButton(true);
            
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
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        SpawnLobbyPlayerUI(player,  runner);
        if (SceneManager.GetActiveScene().name == lobbyScene)
        {
            GameObject ui = Instantiate(lobbyPlayerUIPrefab, lobbyUIParent);
            lobbyPlayerUIs[player] = ui;
        }
    }

    public void SpawnLobbyPlayerUI(PlayerRef player, NetworkRunner runner)
    {
        // // 이미 생성된 경우 중복 생성 방지
        // if (lobbyPlayerUIs.ContainsKey(player))
        //     return;
        //
        // //빈 슬롯 인덱스 찾기(가장 작은 인덱스부터)
        // int slotIndex = 0;
        // while(playerSlotIndexes.ContainsValue(slotIndex) && slotIndex
        //       < SlotManager.slots.Count)
        //     slotIndex++;
        //
        // if (slotIndex >= SlotManager.slots.Count)
        // {
        //     Debug.LogWarning("8명의 플레이어가 모두 접속하여 슬롯이 모두 찼습니다.");
        //     return;
        // }
        //
        // //지정해놓은 슬롯의 위치에 프리팹 생성
        // Transform slotTransform = SlotManager.GetSlot(slotIndex);
        // var uiGo = Instantiate(lobbyPlayerUIPrefab, slotTransform);
        // //슬롯 중앙에 오도록
        // uiGo.transform.localPosition = Vector3.zero;
        //
        // lobbyPlayerUIs[player] = uiGo;
        // playerSlotIndexes[player] = slotIndex;
        //
        // var lobbyPlayer = uiGo.GetComponent<LobbyPlayer>();
        // if (lobbyPlayer != null)
        // {
        //     string nickname = (runner.LocalPlayer == player) ?
        //         StaticData.LocalNickname : $"Player_{player.PlayerId}";
        //         lobbyPlayer.SetNickname(nickname);
        // }

        var uiGo = Instantiate(lobbyPlayerUIPrefab, lobbyUIParent);
        
        var lobbyPlayer = uiGo.GetComponent<LobbyPlayer>();
        if (lobbyPlayer != null)
        {
            //닉네임은 StaticData 또는 네트워크에서 받아온 값 사용
            string nickname = (runner.LocalPlayer == player) ? 
                StaticData.LocalNickname : $"Player_{player.PlayerId}";
            lobbyPlayer.SetNickname(nickname);
        }
    }
    
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (lobbyPlayerUIs.TryGetValue(player, out var ui))
        {
            Destroy(ui);
            lobbyPlayerUIs.Remove(player);
            //플레이어 퇴장 시 슬롯 비우기
            //playerSlotIndexes.Remove(player);
        }
    }
    
    public void OnSceneLoadDone(NetworkRunner runner)
    {
        if (SceneManager.GetActiveScene().name == lobbyScene)
        {
            foreach (var player in runner.ActivePlayers)
            {
                if (!lobbyPlayerUIs.ContainsKey(player))
                {
                    GameObject ui = Instantiate(lobbyPlayerUIPrefab, lobbyUIParent);
                    lobbyPlayerUIs[player] = ui;
                }
            }
        }
    }

    public void OnLoginButtonClicked()
    {
        Debug.Log("OnLoginButtonClicked 호출됨");
        //입력값 읽기
        string nickname = nicknameInputField.text;
        string roomName = roomNameInputField.text;
        
        //닉네임 저장 
        if (!string.IsNullOrEmpty(nickname))
        {
            StaticData.LocalNickname = nickname;
            Debug.Log($"닉네임 저장됨: {StaticData.LocalNickname}");
        }
        else
        {
            //닉네임이 비어있으면 기본값 설정
            StaticData.LocalNickname = $"Player_{UnityEngine.Random.Range(1, 1000)}";
        }
        
        //방 생성 및 네트워크 연결
        CreateRoom(roomName);
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

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
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
