namespace Destroy.Network
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// 网络客户端
    /// </summary>
    public class NetworkClient
    {
        /// <summary>
        /// 连接服务器回调
        /// </summary>
        protected event Action<Socket> OnConnected;

        private readonly string serverIP;

        private readonly int serverPort;

        private Socket client;

        private Dictionary<uint, GetServerDataCallBack> callbackDict;

        private Queue<byte[]> messagesToBeSend;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="serverPort">服务器端口</param>
        /// <param name="onConnected">连接回调方法</param>
        public NetworkClient(string serverIP, int serverPort, Action<Socket> onConnected)
        {
            this.serverIP = serverIP;
            this.serverPort = serverPort;
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            callbackDict = new Dictionary<uint, GetServerDataCallBack>();
            messagesToBeSend = new Queue<byte[]>();
            this.OnConnected = onConnected;
        }

        /// <summary>
        /// 注册回调方法
        /// </summary>
        /// <param name="cmd1">指令1</param>
        /// <param name="cmd2">指令2</param>
        /// <param name="callback">回调方法</param>
        public void Register(ushort cmd1, ushort cmd2, GetServerDataCallBack callback)
        {
            uint key = NetworkUtils.EnumToKey(cmd1, cmd2);
            if (callbackDict.ContainsKey(key))
            {
                return;
            }
            callbackDict.Add(key, callback);
        }

        /// <summary>
        /// 向服务器发送数据
        /// </summary>
        /// <param name="cmd1">指令1</param>
        /// <param name="cmd2">指令2</param>
        /// <param name="data">数据</param>
        public void Send(ushort cmd1, ushort cmd2, byte[] data)
        {
            byte[] packData = NetworkUtils.PackTCPMessage(cmd1, cmd2, data);
            messagesToBeSend.Enqueue(packData);
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public void Start()
        {
            client.Connect(new IPEndPoint(IPAddress.Parse(serverIP), serverPort));
            OnConnected?.Invoke(client);
        }

        /// <summary>
        /// 更新(读取, 发送)
        /// </summary>
        public void Update()
        {
            //接受消息
            while (client.Available > 0)
            {
                NetworkUtils.UnpackTCPMessage(client, out ushort cmd1, out ushort cmd2, out byte[] data);
                uint key = NetworkUtils.EnumToKey(cmd1, cmd2);
                if (callbackDict.ContainsKey(key))
                {
                    callbackDict[key](data);
                }
            }
            //发送消息
            while (messagesToBeSend.Count > 0)
            {
                byte[] data = messagesToBeSend.Dequeue();
                client.Send(data);
            }
        }
    }
}