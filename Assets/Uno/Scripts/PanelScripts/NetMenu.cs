using System.Collections;

using System.Collections.Generic;

using TMPro;

using UnityEngine;

using UnityEngine.UI;

public class NetMenu : BasePanel

{
    Button HostBtn;

    Button JoinBtn;

    TMP_InputField IP_Address;

    TMP_InputField IP_Port;

    private void Start()
    {
        HostBtn = transform.Find("HostBtn").GetComponent<Button>();

        JoinBtn = transform.Find("JoinBtn").GetComponent<Button>();

        IP_Address = transform.Find("IPAddress").GetComponent<TMP_InputField>();

        IP_Port = transform.Find("IPPort").GetComponent<TMP_InputField>();

        HostBtn.onClick.AddListener(Host);

        JoinBtn.onClick.AddListener(Join);
    }

    void Host()
    {
        string addr = IP_Address.text;

        string port = IP_Port.text;

        if (!string.IsNullOrEmpty(addr) && !string.IsNullOrEmpty(port))

        {
            // NetManager.Instance.DecideHostOrNot(true);

            // // 发送主机启动网络事件

            // StartHostNetEvent message = new StartHostNetEvent(addr, port);

            // NetManager.Instance.SendNetMessage(message);
        }
    }

    void Join()
    {
        string addr = IP_Address.text;

        string port = IP_Port.text;

        if (!string.IsNullOrEmpty(addr) && !string.IsNullOrEmpty(port))

        {
            // NetManager.Instance.ClientConnect(addr, port);

            // // 发送客户端连接网络事件

            // StartClientConnectNetEvent message = new StartClientConnectNetEvent(addr, port);

            // NetManager.Instance.SendNetMessage(message);
        }
    }
}
