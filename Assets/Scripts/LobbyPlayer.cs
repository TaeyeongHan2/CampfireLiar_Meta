using Fusion;
using UnityEngine;
using TMPro;

public class LobbyPlayer : NetworkBehaviour, IPlayerJoined
{ 
    //네트워크로 동기화되는 속성은 [Networked] 속성이 표시되어 있어야 함.
    [Networked, OnChangedRender(nameof(NicknameChanged))]
    //최대 16자까지 저장 가능
    private NetworkString<_16> Nickname { get; set; }

    public TMP_Text nicknameUI;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            if (string.IsNullOrEmpty(StaticData.LocalNickname))
            {
                StaticData.LocalNickname = Object.Name;
            }
            
            Nickname = StaticData.LocalNickname;
        }
        
        gameObject.name = $"Player {Nickname.Value}";
        NicknameChanged();
    }
    
    private void NicknameChanged()
    {
        nicknameUI.text = Nickname.Value;
    }
    
    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log($"PlayerJoined: {player}");
        
    }

    public void SetNickname(string nickname)
    {
        //닉네임을 직접 세팅하는 메서드
        if (nicknameUI != null)
        {
            nicknameUI.text = nickname;
        }
    }
}
