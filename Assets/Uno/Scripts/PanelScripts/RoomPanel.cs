using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel:BasePanel
{
    TextMeshProUGUI roomName;
    GameObject playerItemPrefab;
    Dictionary<int, PlayerItem> playerItems = new Dictionary<int, PlayerItem>();
    public override void InitContent()
    {
        UIManager.Instance.RegisterPanel<RoomPanel>(this);
        EventManager.Instance.AddListener<LoadRoomDataEvent>(OnLoadRoomData);
        EventManager.Instance.AddNetListener<PlayerJoinedMessage>(OnPlayerJoined);
        EventManager.Instance.AddNetListener<PlayerLeftMessage>(OnPlayerLeft);
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

    void OnPlayerJoined(INetEventMessage message)
    {
        // TODO: add player item
        var netMessage = (PlayerJoinedMessage)message;
        AddPlayerItem(netMessage.ClientId, netMessage.PlayerName);
    }

    void OnPlayerLeft(INetEventMessage message)
    {
        // TODO: remove player item
        var netMessage = (PlayerLeftMessage)message;
        playerItems.Remove(netMessage.ClientId);
    }
}