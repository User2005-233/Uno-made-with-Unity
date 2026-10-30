using System.Collections;

using System.Collections.Generic;

using TMPro;

using UnityEngine;

using UnityEngine.UI;

using FishNet.Managing;

public class NetworkPanel : BasePanel

{
    [SerializeField] Button starthostbtn;
    [SerializeField] Button startserverbtn;
    [SerializeField] Button startclientbtn;
    [SerializeField] TMP_InputField ipaddressinputfield;
    [SerializeField] Button backbtn;
    [SerializeField] private NetManager _networkManager;

    public override void InitContent()
    {
        starthostbtn = transform.Find("GameMode/StartHostBtn").GetComponent<Button>();
        startserverbtn = transform.Find("GameMode/StartServerBtn").GetComponent<Button>();
        startclientbtn = transform.Find("GameMode/StartClientBtn").GetComponent<Button>();
        ipaddressinputfield = transform.Find("GameMode/IPAddressInputField").GetComponent<TMP_InputField>();
        backbtn = transform.Find("GameMode/BackBtn").GetComponent<Button>();
        starthostbtn.onClick.AddListener(OnStartHost);
        startserverbtn.onClick.AddListener(OnStartServer);
        startclientbtn.onClick.AddListener(OnStartClient);
        backbtn.onClick.AddListener(OnBack);
    }
    protected override void Awake()
    {
        HideInit();
    }

    void OnStartHost()
    {
        // EventManager.Instance.TriggerEvent<StartHostAction>();
    }
    void OnStartClient()
    {
        // EventManager.Instance.TriggerEvent<StartClientAction>();
    }
    
    void OnStartServer()
    {
        // EventManager.Instance.TriggerEvent<StartServerAction>();
    }

    void OnBack(){
        UIManager.Instance.ShowPanel<MainMenuPanel>();
        UIManager.Instance.HidePanel<NetworkPanel>();
    }
}
