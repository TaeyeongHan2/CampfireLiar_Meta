using Fusion;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class NetworkPlayerSpawner : SimulationBehaviour//, IPlayerJoined
{
    //로비에 생성되는 플레이어 UI 프리팹
    public GameObject lobbyPlayerUIPrefab;
    //게임 씬에 생성되는 플레이어 3D 오브젝트 프리팹
    public GameObject playerPrefab3D;

    //로비에서 호출
    // public void SpawnLobbyPlayerUI(PlayerRef player, NetworkRunner runner)
    // {
    //     //자신의 플레이어만 UI를 스폰하도록 (StateAuthority 체크)
    //     if (runner.LocalPlayer == player)
    //     {
    //         //로비 플레이어 UI 프리팹 인스턴스화 
    //         var uiGo = runner.Spawn(lobbyPlayerUIPrefab);
    //         
    //         //LobbyPlayer 컴포넌트 가져오기 
    //         var lobbyPlayer = uiGo.GetComponent<LobbyPlayer>();
    //         if (lobbyPlayer != null)
    //         {
    //             //닉네임 동기화: StaticData.LocalNickname을 사용
    //             lobbyPlayer.SetNickname(StaticData.LocalNickname);
    //         }
    //     }
    //     
    //     
    //     
    //     if(Object.HasStateAuthority == false) return;
    //     
    //     //단발성이 rpc고
    //     //값 자체가 동기화되는게 값이 변결될 때 계속 알려주는거 networked라는 attribute로 동기화
    //     //내부적으로 동기화할 때 ui 도 갱신
    //     
    // }
}
