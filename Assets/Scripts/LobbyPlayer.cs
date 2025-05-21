using Fusion;
using UnityEngine;
using TMPro;

public class LobbyPlayer : NetworkBehaviour, IPlayerJoined
{
    [Networked, OnChangedRender(nameof(NicknameChanged))]
    
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
}
