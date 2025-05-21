//이 스크립트는 혹시 몰르니까 참고용으로 남겨둠
//여기 있는 내용을 FussionSession으로 넘길 예정

// using System;
// using System.Collections.Generic;
// using Fusion;
// using Fusion.Sockets;
// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
//
// public class LoginManager : MonoBehaviour, INetworkRunnerCallbacks
// {
//     private const string LobbySceneName = "LobbyScene";
//     
//     public GameObject runnerPrefab;
//     public TMP_InputField roomNameInputField;
//
//     private static NetworkRunner Runner;
//
//     public void CreateRoom()
//     {
//         CreateRoom(roomNameInputField.text);
//     }
//
//     public void JoinLobby()
//     {
//         if (Runner == null)
//         {
//             var runnerGo = Instantiate(runnerPrefab);
//             Runner = runnerGo.GetComponent<NetworkRunner>();
//         }
//
//         Runner.JoinSessionLobby(SessionLobby.Shared);
//     }
//
//     private void CreateRoom(string roomName)
//     {
//         if (Runner == null)
//         {
//             var runnerGo = Instantiate(runnerPrefab);
//             Runner = runnerGo.GetComponent<NetworkRunner>();
//         }
//
//         Runner.StartGame(new StartGameArgs
//         {
//             SessionName = roomName,
//             GameMode = GameMode.Shared,
//             Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath(LobbySceneName))
//         });
//     }
//
//     public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnInput(NetworkRunner runner, NetworkInput input)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnConnectedToServer(NetworkRunner runner)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnSceneLoadDone(NetworkRunner runner)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void OnSceneLoadStart(NetworkRunner runner)
//     {
//         throw new NotImplementedException();
//     }
// }