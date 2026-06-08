
using FishNet;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Managing.Observing;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;


public class NetManager : MonoSingleton<NetManager>

{
    /*
    
    #region Original Code
    
    TcpClient tcpClient;

    NetworkStream stream;

    private TcpListener tcpListener;

    private Dictionary<int, TcpClient> clientsDict = new Dictionary<int, TcpClient>();
    bool HostOrNot = false;

    bool isRunning = true;

    int MachineCode = 0;

    public void DecideHostOrNot(bool value)
    {
        if (value)

        {
            HostStart();
        }

        else

        {
            ClientStart();
        }
    }

    /// <summary>
    /// SERVER CODE
    /// </summary>
    void HostStart()
    {
        HostOrNot = true;

        int port = 0;

        while (true)
        {
            port = UnityEngine.Random.Range(1024, 49151);

            if (IsPortInUse(port)) continue;

            else break;
        }

        tcpListener = new TcpListener(IPAddress.Any, port);

        tcpListener.Start();

        SendLog("正在监听端口...");

        EventManager.Instance.TriggerEvent<StartHostSucceededAction>();

        StartHeartBeat();

        Thread listenThread = new Thread(ListenForClients);

        listenThread.Start();
    }

    public static bool IsPortInUse(int port)
    {
        try

        {
            // 使用 netstat 检查端口占用            
            ProcessStartInfo startInfo = new ProcessStartInfo

            {
                FileName = "netstat",
                Arguments = $"-ano | findstr :{port}",  // 在所有连接中查找端口
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true

            };

            using (Process process = Process.Start(startInfo))
            using (System.IO.StreamReader reader = process.StandardOutput)

            {
                string output = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(output))
                {
                    return true;
                }
            }

            return false;
        }

        catch (Exception ex)

        {
            Console.WriteLine($"检查端口时出错: {ex.Message}");
            return false;
        }
    }

    private void ListenForClients()
    {
        while (isRunning)

        {
            try
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();

                clientsDict.Add(clientsDict.Count, tcpClient);

                SendLog("客户端已连接.");

                // 为新客户端启动处理线程
                Thread clientThread = new Thread(() => HandleClientComm(tcpClient));

                clientThread.Start();
            }

            catch (Exception ex)

            {
                SendLog($"监听客户端时出错: {ex.Message}");
            }
        }
    }

    private void HandleClientComm(TcpClient tcpClient)
    {
        NetworkStream stream = tcpClient.GetStream();

        byte[] buffer = new byte[4096];

        int bytesRead;

        // 获取客户端的key值
        int clientKey = clientsDict.FirstOrDefault(x => x.Value == tcpClient).Key;
        try

        {
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)

            {
                // 复制接收到的数据到新数组
                byte[] receivedData = new byte[bytesRead];

                Array.Copy(buffer, 0, receivedData, 0, bytesRead);

                // 更新客户端消息时间戳
                UpdateClientMessageTime(clientKey);

                // 启动延迟超时检查协程
                StartCoroutine(DelayedTimeoutCheck());

                // 处理接收到的消息
                HandleMessages(receivedData);
            }
        }

        catch (Exception ex)

        {
            //SendLog($"处理客户端通信时出错: {ex.Message}");
        }

        finally

        {
            // 从客户端字典中移除断开的客户端
            clientsDict.Remove(clientsDict.FirstOrDefault(x => x.Value == tcpClient).Key);

            tcpClient.Close();
        }
    }

    private void BroadcastMessage(byte[] message)
    {
        for (int i = 0; i < clientsDict.Count; i++)
        {
            if (i == MachineCode)

            {
                continue;
            }

            try

            {
                TcpClient client = clientsDict[i];

                NetworkStream stream = client.GetStream();

                stream.Write(message, 0, message.Length);

                stream.Flush();
            }

            catch (Exception ex)

            {
                //SendLog($"广播消息时出错: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// CLIENT CODE
    /// </summary>
    void ClientStart()
    {
        HostOrNot = false;
    }

    public static string GetLocalIpAddress()
    {
        string localIp = string.Empty;

        // 遍历所有网络接口获取本地IP地址
        foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())

        {
            // 获取IPv4地址，排除回环地址127.0.0.1
            foreach (var ipAddress in networkInterface.GetIPProperties().UnicastAddresses)

            {
                if (ipAddress.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ipAddress.Address))

                {
                    localIp = ipAddress.Address.ToString();

                    return localIp; // 返回找到的IPv4地址
                }
            }
        }

        return localIp; // 如果没找到则返回空字符串
    }

    void StartHeartBeat()
    {

    }

    // 客户端超时管理：记录每个客户端最后一次消息的时间，用于检测超时的客户端

    // 用于存储客户端最后消息时间的字典
    private Dictionary<int, DateTime> clientLastMessageTime = new Dictionary<int, DateTime>();
    /// <summary>
    /// 更新客户端最后消息时间，用于超时检测
    /// </summary>
    public void UpdateClientMessageTime(int clientKey)
    {
        DateTime now = DateTime.Now;

        foreach (var key in clientLastMessageTime.Keys)

        {
            if (key != clientKey)

            {
                clientLastMessageTime[key] = now;
            }
        }
    }

    /// <summary>
    /// 获取超时的客户端列表
    /// </summary>

    /// <returns>返回超时的客户端key列表</returns>
    public List<int> GetTimeoutClients(float timeoutSeconds = 4f)
    {
        List<int> timeoutClientKeys = new List<int>();
        DateTime now = DateTime.Now;

        foreach (var kv in clientsDict)

        {
            int key = kv.Key;

            if (clientLastMessageTime.ContainsKey(key))

            {
                TimeSpan span = now - clientLastMessageTime[key];

                if (span.TotalSeconds > timeoutSeconds)
                    timeoutClientKeys.Add(key);
            }

            else

            {
                // 没有记录消息时间的客户端，加入超时列表
                timeoutClientKeys.Add(key);
            }
        }

        return timeoutClientKeys;
    }

    /// <summary>
    /// 发送日志信息到主线程
    /// </summary>
    void SendLog(string str)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => UnityEngine.Debug.Log(str));
    }

    void MultiThreadAction(Action action)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(action);
    }

    /// <summary>
    /// 延迟超时检查协程
    /// </summary>
    private System.Collections.IEnumerator DelayedTimeoutCheck()
    {
        yield return new WaitForSeconds(4f);

        // 获取超时的客户端列表
        List<int> timeoutClients = GetTimeoutClients(4f);
        if (timeoutClients.Count > 0)

        {
            SendLog($"发现 {timeoutClients.Count} 个超时的客户端: {string.Join(", ", timeoutClients)}");
            // 这里可以添加处理超时客户端的逻辑，比如断开连接或发送警告

            // 清理超时的客户端连接
        }
    }





    private void HandleMessages(byte[] data)
    {
        if (data == null)
        {
            SendLog("收到无效的数据包：数据为null或长度不足");
            return;
        }

        var value = new DataPacket(data);
        if (value.GetLength() > 0)
        {
            WrapperType type = value.ReadEnum<WrapperType>();
            if (HostOrNot)
            {
                UpdateClientMessageTime(value.ReadInt());
            }
            switch (type)
            {
                case WrapperType.Heartbeat:
                    break;
                case WrapperType.ConnectReq:
                    if (HostOrNot)
                    {
                        DataPacket pack = new DataPacket();
                        pack.WriteEnum<WrapperType>(WrapperType.ConnectReq);
                        pack.WriteInt(MachineCode);
                        BroadcastMessage(pack.GetData());
                    }
                    break;
                case WrapperType.DisconnectReq:
                    break;

            }
        }


    }

    #endregion

    */

    #region New Code

    private NetworkManager _networkManager;
    private void Awake()
    {
        _networkManager = GetComponent<NetworkManager>();
        AddListoners();
    }


    
    private void AddListoners()
    {
        // TODO: 添加事件监听
    }

    private void OnEnable()
    {
        // 订阅 FishNet 底层的连接变动事件
        if (_networkManager != null)
        {
            // 服务器端监听：有任何远程客户端连接/断开
            _networkManager.ServerManager.OnRemoteConnectionState += OnServerRemoteConnectionStateChanged;
            
            // 客户端端监听：当其他客户端连接/断开（注意：需要在 NetworkManager 的 ServerManager 组件中勾选 "Share Ids"）
            _networkManager.ClientManager.OnRemoteConnectionState += OnClientRemoteConnectionStateChanged;
        }
    }

    private void OnDisable()
    {
        if (_networkManager != null)
        {
            _networkManager.ServerManager.OnRemoteConnectionState -= OnServerRemoteConnectionStateChanged;
            _networkManager.ClientManager.OnRemoteConnectionState -= OnClientRemoteConnectionStateChanged;
        }
    }

    private void OnServerRemoteConnectionStateChanged(NetworkConnection conn, RemoteConnectionStateArgs args)
    {
        if (args.ConnectionState == RemoteConnectionState.Started)
        {
            Debug.Log($"[服务器] 玩家 {conn.ClientId} 加入了服务器");
            
            PlayerJoinedMessage msg = new PlayerJoinedMessage(conn.ClientId, $"Player_{conn.ClientId}");
            EventManager.Instance.SendNetMessage(msg);
        }
        else if (args.ConnectionState == RemoteConnectionState.Stopped)
        {
            Debug.Log($"[服务器] 玩家 {conn.ClientId} 离开了服务器");
            
            PlayerLeftMessage msg = new PlayerLeftMessage(conn.ClientId);
            EventManager.Instance.SendNetMessage(msg);
        }
    }
    private void OnClientRemoteConnectionStateChanged(RemoteConnectionStateArgs args)
    {
        if (args.ConnectionState == RemoteConnectionState.Started)
        {
            Debug.Log($"[客户端] 广播：别的玩家 {args.ConnectionId} 加入了游戏");

            PlayerJoinedMessage msg = new PlayerJoinedMessage(args.ConnectionId, $"Player_{args.ConnectionId}");
            EventManager.Instance.SendNetMessage(msg);
        }
        else if (args.ConnectionState == RemoteConnectionState.Stopped)
        {
            Debug.Log($"[客户端] 广播：别的玩家 {args.ConnectionId} 离开了游戏");

            PlayerLeftMessage msg = new PlayerLeftMessage(args.ConnectionId);
            EventManager.Instance.SendNetMessage(msg);
        }
    }
    

    #endregion


}
