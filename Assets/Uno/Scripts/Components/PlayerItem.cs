using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    TextMeshProUGUI playerName;
    Image playerIcon;
    Button Btn;
    int playerId;
    bool IAmClient = true;
    
    void Awake()
    {
        playerName = transform.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        playerIcon = transform.Find("PlayerIcon").GetComponent<Image>();
        Btn = transform.Find("KickBtn").GetComponent<Button>();
        Btn.onClick.AddListener(OnKickBtnClick);
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
    
    public void SetIAmClient(bool isClient)
    {
        IAmClient = isClient;
        if (!IAmClient)
        {
            Btn.gameObject.SetActive(false);
        }
    }

    void OnKickBtnClick()
    {
        // TODO: 把人踢出去
        GameManager.Instance.KickPlayer(playerId);
    }
    
    
}
