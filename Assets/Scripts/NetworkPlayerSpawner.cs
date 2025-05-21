using Fusion;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class NetworkPlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    //로비에 생성되는 플레이어 UI 프리팹
    public GameObject lobbyPlayerUIPrefab;
    //게임 씬에 생성되는 플레이어 3D 오브젝트 프리팹
    public GameObject playerPrefab3D;

    //로비에서 호출
    public void SpawnLobbyPlayerUI(PlayerRef player, NetworkRunner runner)
    {
        if(Object.HasStateAuthority == false) return;
        
        //단발성이 rpc고
        //값 자체가 동기화되는게 값이 변결될 때 계속 알려주는거 networked라는 attribute로 동기화
        //내부적으로 동기화할 때 ui 도 갱신
        
    }
    
    
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(playerPrefab3D, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}





// public class NetworkPlayerSpawner : MonoBehaviour , INetworkRunnerCallbacks
// {
//     [SerializeField] private GameObject playerPrefab;
//     
//     private Dictionary<PlayerPrefs , NetworkObject> spawnedPlayers = new ();
//
//     public void OnPlayerJoined(NetworkRunner runner, PlayerPrefs player)
//     {
//         if (runner.IsServer)
//         {
//             //랜덤 위치 생성
//             Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
//             Quaternion spawnRotation = Quaternion.identity;
//             
//             //플레이어 프리팹 스폰
//             NetworkObject playerObject = runner.Spawn(playerPrefab, spawnPosition, spawnRotation, player);
//             
//             //추적용 저장
//             spawnedPlayers[player] = playerObject;
//         }
//     }
//     
//     
// }
