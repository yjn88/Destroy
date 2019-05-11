namespace Destroy.Network
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// UDP
    /// </summary>
    public class Udp
    {
        private UdpClient udp;

        private Dictionary<uint, GetUdpDataCallBack> callbackDict;

        private Queue<UdpMessage> messagesToBeSend;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">端口号</param>
        public Udp(string ip, int port)
        {
            callbackDict = new Dictionary<uint, GetUdpDataCallBack>();
            udp = new UdpClient(new IPEndPoint(IPAddress.Parse(ip), port));
            messagesToBeSend = new Queue<UdpMessage>();
        }

        /// <summary>
        /// 注册回调方法
        /// </summary>
        /// <param name="cmd1">指令1</param>
        /// <param name="cmd2">指令2</param>
        /// <param name="callback">回调方法</param>
        public void Register(ushort cmd1, ushort cmd2, GetUdpDataCallBack callback)
        {
            uint key = NetworkUtils.EnumToKey(cmd1, cmd2);
            if (callbackDict.ContainsKey(key))
            {
                return;
            }
            callbackDict.Add(key, callback);
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="cmd1">指令1</param>
        /// <param name="cmd2">指令2</param>
        /// <param name="data">数据</param>
        public void Send(string ip, int port, ushort cmd1, ushort cmd2, byte[] data)
        {
            byte[] packData = NetworkUtils.PackUDPMessage(cmd1, cmd2, data);
            messagesToBeSend.Enqueue(new UdpMessage(new IPEndPoint(IPAddress.Parse(ip), port), packData));
        }

        /// <summary>
        /// 广播
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="cmd1">指令1</param>
        /// <param name="cmd2">指令2</param>
        /// <param name="data">数据</param>
        public void Broadcast(int port, ushort cmd1, ushort cmd2, byte[] data)
        {
            Send(IPAddress.Broadcast.ToString(), port, cmd1, cmd2, data);
        }

        /// <summary>
        /// 更新(接受 发送)
        /// </summary>
        public void Update()
        {
            //接收消息
            while (udp.Available > 0)
            {
                IPEndPoint iPEndPoint = null;
                byte[] data = udp.Receive(ref iPEndPoint);

                NetworkUtils.UnpackUDPMessage(data, out ushort cmd1, out ushort cmd2, out byte[] msgData);
                uint key = NetworkUtils.EnumToKey(cmd1, cmd2);
                if (callbackDict.ContainsKey(key))
                {
                    callbackDict[key](iPEndPoint, msgData);
                }
            }
            //发送消息
            while (messagesToBeSend.Count > 0)
            {
                UdpMessage message = messagesToBeSend.Dequeue();
                message.Send(udp);
            }
        }
    }
}