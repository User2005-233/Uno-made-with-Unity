using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public GameObject playerPrefab;
    readonly Dictionary<int, PlayerEntity> playerDict = new();

    public void AddPlayer(int code)
    {
        GameObject player = Instantiate(playerPrefab);
        PlayerEntity playerEntity = player.GetComponent<PlayerEntity>();
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
        playerDict.Remove(code);
    }
    
}