using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    TextMeshProUGUI playerName;
    Image playerIcon;
    Button kickBtn;
    int playerId;
    
    void Awake()
    {
        playerName = transform.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        playerIcon = transform.Find("PlayerIcon").GetComponent<Image>();
        kickBtn = transform.Find("KickBtn").GetComponent<Button>();
        kickBtn.onClick.AddListener(OnKickBtnClick);
    }
    public void SetPlayerId(int id)
    {
        playerId = id;
    }
    public void SetPlayerName(string name)
    {
        playerName.text = name;
    }
    
    public void SetPlayerIcon(Sprite icon)
    {
        playerIcon.sprite = icon;
    }

    void OnKickBtnClick()
    {
        // TODO: 把人踢出去
    }
    
}
