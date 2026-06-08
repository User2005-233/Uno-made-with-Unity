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
        starthostbtn.onClick.AddListener(StartHost);
        starthostbtn.onClick.AddListener(() => Debug.Log("Host button clicked"));
        startserverbtn.onClick.AddListener(StartServer);
        startserverbtn.onClick.AddListener(() => Debug.Log("Server button clicked"));
        startclientbtn.onClick.AddListener(StartClient);
        startclientbtn.onClick.AddListener(() => Debug.Log("Client button clicked"));
        backbtn.onClick.AddListener(OnBack);
    }
    protected override void Awake()
    {
        HideInit();
    }
    public void StartHost()
    {
        StartServer();
        StartClient();
    }
    private void StartServer()
    {
        SetIPAddress("127.0.0.1");
        _networkManager.ServerManager.StartConnection();
    }

    private void StartClient()
    {
        SetIPAddress(ipaddressinputfield.text);
        _networkManager.ClientManager.StartConnection();
    }
    public void SetIPAddress(string text)
    {
        _networkManager.TransportManager.Transport.SetClientAddress(text);
    }
    void OnBack(){
        UIManager.Instance.ShowPanel<MainMenuPanel>();
        UIManager.Instance.HidePanel<NetworkPanel>();
    }
}
