using Fusion;
using UnityEngine;
using System.Collections.Generic;

public class NetworkPlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
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
