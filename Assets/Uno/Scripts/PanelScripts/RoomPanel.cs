using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel:BasePanel
{
    TextMeshProUGUI roomName;
    GameObject playerItemPrefab;
    Dictionary<int, PlayerItem> playerItems = new Dictionary<int, PlayerItem>();
    Button startGameBtn;
    Button leaveRoomBtn;

    public override void InitContent()
    {
        EventManager.Instance.AddListener<LoadRoomDataEvent>(OnLoadRoomData);
        startGameBtn = transform.Find("StartBtn").GetComponent<Button>();
        leaveRoomBtn = transform.Find("ExitBtn").GetComponent<Button>();
        startGameBtn.onClick.AddListener(OnStartGame);
        leaveRoomBtn.onClick.AddListener(OnLeaveRoom);
    }
    protected override void Awake()
    {
        roomName = transform.Find("RoomName").GetComponent<TextMeshProUGUI>();
        HideInit();
    }

    void OnLoadRoomData(IEventMessage message)
    {
        // TODO: load room data
        var msg = (LoadRoomDataEvent)message;
        roomName.text = msg.roomName;
        foreach (var player in msg.playerList)
        {
            AddPlayerItem(player.Key, player.Value);
        }
    }

    void AddPlayerItem(int playerId, string playerName)
    {
        // TODO: add player item
        var playerItem = Instantiate(playerItemPrefab, transform.Find("PlayerList"));
        PlayerItem item = playerItem.GetComponent<PlayerItem>();
        item.SetPlayerName(playerName);
        playerItems.Add(playerId, item);
    }

    void OnStartGame()
    {
        // TODO: start game
        GameManager.Instance.StartGame();
    }
    
    void OnLeaveRoom()
    {
        // TODO: leave room
        GameManager.Instance.ExitRoom();
    }
}