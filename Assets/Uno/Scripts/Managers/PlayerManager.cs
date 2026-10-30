using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.Splines;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public GameObject playerPrefab;
    public SplineContainer playerPositionSpline;
    protected override bool IsPersistent => false;
    readonly Dictionary<int, PlayerEntity> playerDict = new();

    protected override void Awake()
    {
        base.Awake();
        EventManager.Instance.AddListener<PlayerEntityCreateEvent>(OnPlayerEntityAdd);
        playerPositionSpline = transform.Find("PlayerPositionSpline").GetComponent<SplineContainer>();
    }

    private void OnPlayerEntityAdd(IEventMessage obj)
    {
        if (obj is PlayerEntityCreateEvent eventMsg)
        {
            if(eventMsg.isSelf)
            {
                AddSelfPlayer(eventMsg.PlayerId);
            }
            else
            {
                AddPlayer(eventMsg.PlayerId);
            }
            UpdatePlayerPositions();
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
    
    /// <summary>
    /// Updates the positions of all players based on the player position spline.
    /// </summary>
    public void UpdatePlayerPositions()
    {
        int count = playerDict.Count;
        if (count == 0) return;

        int index = 0;
        foreach (var player in playerDict.Keys)
        {
            float space = index / (float)count;
            Vector3 pos = playerPositionSpline.EvaluatePosition(space);
            playerDict[player].transform.position = pos;
            playerDict[player].transform.localRotation = Quaternion.Euler(0, index * 360f / count, 0);
            index++;
        }
    }
}