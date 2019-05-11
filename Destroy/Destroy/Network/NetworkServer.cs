namespace Destroy.Network
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// 网阔服务器
    /// </summary>
    public class NetworkServer
    {
        /// <summary>
        /// 客户端连接回调
        /// </summary>
        protected event Action<Client> OnConnected;

        /// <summary>
        /// 客户端断线回调
        /// </summary>
        protected event Action<Client, string> OnDisconnected;

        private Socket server;

        private List<Client> clients;

        private Dictionary<uint, GetClientDataCallBack> callbackDict;

        private Queue<ClientMessage> messagesToBeSend;

        private bool accept;

        private IAsyncResult acceptAsync;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="onConnected">客户端连接回调方法</param>
        /// <param name="onDisconnected">客户端断开连接回调方法</param>
        public NetworkServer(int port, Action<Client> onConnected, Action<Client, string> onDisconnected)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(NetworkUtils.LocalIPv4, port));
            clients = new List<Client>();
            callbackDict = new Dictionary<uint, GetClientDataCallBack>();
            messagesToBeSend = new Queue<ClientMessage>();
            accept = true;
            acceptAsync = null;
            this.OnConnected = onConnected;
            this.OnDisconnected = onDisconnected;
        }

        /// <summary>
        /// 注册回调方法
        /// </summary>
        /// <param name="cmd1">指令1</param>
        /// <param name="cmd2">指令2</param>
        /// <param name="callback">回调方法</param>
        public void Register(ushort cmd1, ushort cmd2, GetClientDataCallBack callback)
        {
            uint key = NetworkUtils.EnumToKey(cmd1, cmd2);
            if (callbackDict.ContainsKey(key))
            {
                return;
            }
            callbackDict.Add(key, callback);
        }

        /// <summary>
        /// 向客户端发送数据
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="cmd1">指令1</param>
        /// <param name="cmd2">指令2</param>
        /// <param name="data">数据</param>
        public void Send(Client client, ushort cmd1, ushort cmd2, byte[] data)
        {
            byte[] packData = NetworkUtils.PackTCPMessage(cmd1, cmd2, data);
            messagesToBeSend.Enqueue(new ClientMessage(client, packData));
        }

        /// <summary>
        /// 开始监听客户端
        /// </summary>
        public void Start()
        {
            server.Listen(10);
        }

        /// <summary>
        /// 更新(接受, 读取, 发送)
        /// </summary>
        public void Update()
        {
            //异步接收客户端
            if (accept)
            {
                try
                {
                    acceptAsync = server.BeginAccept(null, null);
                    accept = false;
                }
                catch (Exception)
                {
                }
            }
            if (acceptAsync.IsCompleted)
            {
                try
                {
                    Socket socket = server.EndAccept(acceptAsync);
                    Client client = new Client(true, socket);
                    clients.Add(client);
                    OnConnected?.Invoke(client);
                }
                catch (Exception)
                {
                }
                finally
                {
                    accept = true;
                }
            }
            //异步接收消息
            foreach (Client client in clients)
            {
                try
                {
                    while (client.Socket.Available > 0)
                    {
                        NetworkUtils.UnpackTCPMessage(client.Socket,
                            out ushort cmd1, out ushort cmd2, out byte[] data);
                        uint key = NetworkUtils.EnumToKey(cmd1, cmd2);
                        if (callbackDict.ContainsKey(key))
                        {
                            //执行回调方法
                            callbackDict[key](client, data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    RemoveClient(client, ex.Message);
                }
            }
            List<Client> deleteClients = new List<Client>();
            //异步发送消息
            while (messagesToBeSend.Count > 0)
            {
                ClientMessage message = messagesToBeSend.Dequeue();
                Client client = message.Client;
                if (!client.Connected)
                {
                    deleteClients.Add(client);
                }
                else
                {
                    try
                    {
                        client.Socket.Send(message.Data);
                    }
                    catch (Exception ex)
                    {
                        RemoveClient(client, ex.Message);
                        deleteClients.Add(client);
                    }
                }
            }
            foreach (Client item in deleteClients)
            {
                clients.Remove(item);
            }
        }

        private void RemoveClient(Client client, string msg = "")
        {
            client.Socket.Close();
            client.Connected = false;
            OnDisconnected?.Invoke(client, msg);
        }
    }
}