using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomItem : MonoBehaviour
{

    //TODO: add room data

    TextMeshProUGUI roomSize;
    TextMeshProUGUI roomName;
    Button joinButton;
    
    void Awake()
    {
        roomSize = transform.Find("RoomSize").GetComponent<TextMeshProUGUI>();
        roomName = transform.Find("RoomName").GetComponent<TextMeshProUGUI>();
        joinButton = transform.Find("JoinButton").GetComponent<Button>();
        joinButton.onClick.AddListener(OnJoinButtonClicked);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRoomText(string roomSizeText, string roomNameText)
    {
        roomSize.text = roomSizeText;
        roomName.text = roomNameText;
    }
    
    public void SetRoomData()
    {
        //TODO: set room data
    }
    
    void OnJoinButtonClicked()
    {
        //TODO: send join room message with room data
        
        NetManager.Instance.TryJoinRoom();
    }
}
