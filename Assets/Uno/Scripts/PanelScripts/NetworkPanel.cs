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
    [SerializeField] private NetworkManager _networkManager;

    public override void InitContent()
    {
        UIManager.Instance.RegisterPanel<NetworkPanel>(this);
        starthostbtn.onClick.AddListener(OnStartHost);
        // startserverbtn.onClick.AddListener(OnStartServer);
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

    void OnBack(){
        UIManager.Instance.ShowPanel<MainMenuPanel>();
        UIManager.Instance.HidePanel<NetworkPanel>();
    }
}
