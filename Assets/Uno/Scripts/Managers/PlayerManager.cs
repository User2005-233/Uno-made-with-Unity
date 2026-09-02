using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public GameObject playerPrefab;
    readonly Dictionary<int, PlayerEntity> playerDict = new();

    protected override void Awake()
    {
        base.Awake();
        EventManager.Instance.AddListener<PlayerEntityAddEvent>(OnPlayerEntityAdd);
    }

    private void OnPlayerEntityAdd(IEventMessage obj)
    {
        if (obj is PlayerEntityAddEvent eventMsg)
        {
            AddPlayer(eventMsg.PlayerId);
        }
    }

    public void AddPlayer(int code)
    {
        GameObject player = Instantiate(playerPrefab);
        PlayerEntity playerEntity = player.GetComponent<PlayerEntity>();
        playerEntity.SetCode(code);
        player.transform.parent = transform;
        playerDict.Add(code, playerEntity);
        playerEntity.MakeNotSelf();
    }
    
    public void AddSelfPlayer(int code)
    {
        AddPlayer(code);
        GetPlayer(code).MakeMeSelf();
    }
    
    public PlayerEntity GetPlayer(int code)
    {
        return playerDict[code];
    }
    
    public void RemovePlayer(int code)
    {
        Destroy(playerDict[code].gameObject);
        playerDict.Remove(code);
    }
    
    public bool HasPlayer(int code)
    {
        return playerDict.ContainsKey(code);
    }
    
    public bool TryGetPlayer(int code, out PlayerEntity player)
    {
        return playerDict.TryGetValue(code, out player);
    }
    
    public bool TryRemovePlayer(int code, out PlayerEntity player)
    {
        if (playerDict.TryGetValue(code, out player))
        {
            Destroy(player.gameObject);
            playerDict.Remove(code);
            return true;
        }
        player = null;
        return false;
    }
    
    public int GetPlayerCount()
    {
        return playerDict.Count;
    }
    
    public List<PlayerEntity> GetAllPlayers()
    {
        return new List<PlayerEntity>(playerDict.Values);
    }
    
}