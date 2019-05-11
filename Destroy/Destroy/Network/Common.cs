namespace Destroy.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// 获得客户端消息回调
    /// </summary>
    /// <param name="client">客户端</param>
    /// <param name="data">数据</param>
    public delegate void GetClientDataCallBack(Client client, byte[] data);

    /// <summary>
    /// 获得服务器消息回调
    /// </summary>
    /// <param name="data">数据</param>
    public delegate void GetServerDataCallBack(byte[] data);

    /// <summary>
    /// 获得UDP消息回调
    /// </summary>
    /// <param name="sender">发送者的IP地址与端口号</param>
    /// <param name="data">数据</param>
    public delegate void GetUdpDataCallBack(IPEndPoint sender, byte[] data);

    /// <summary>
    /// 客户端
    /// </summary>
    public class Client
    {
        /// <summary>
        /// 是否连接
        /// </summary>
        public bool Connected;

        /// <summary>
        /// 套接字
        /// </summary>
        public Socket Socket;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connected">是否连接</param>
        /// <param name="socket">套接字</param>
        public Client(bool connected, Socket socket)
        {
            Connected = connected;
            Socket = socket;
        }
    }

    internal class ClientMessage
    {
        public Client Client;

        public byte[] Data;

        public ClientMessage(Client client, byte[] data)
        {
            Client = client;
            Data = data;
        }
    }

    internal class UdpMessage
    {
        private readonly IPEndPoint endPoint;

        private readonly byte[] data;

        public UdpMessage(IPEndPoint endPoint, byte[] data)
        {
            this.endPoint = endPoint;
            this.data = data;
        }

        public void Send(UdpClient udp) => udp.Send(data, data.Length, endPoint);
    }

    /// <summary>
    /// 网络工具包 <see langword="static"/>
    /// </summary>
    public static class NetworkUtils
    {
        /// <summary>
        /// 本地IPv4
        /// </summary>
        public static IPAddress LocalIPv4
        {
            get
            {
                string hostName = Dns.GetHostName();
                IPHostEntry iPEntry = Dns.GetHostEntry(hostName);
                for (int i = 0; i < iPEntry.AddressList.Length; i++)
                {
                    if (iPEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        IPAddress address = iPEntry.AddressList[i];
                        return address;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 枚举转整数
        /// </summary>
        /// <param name="cmd1">指令1(0-65535)</param>
        /// <param name="cmd2">指令2(0-65535)</param>
        /// <returns>唯一键</returns>
        public static uint EnumToKey(ushort cmd1, ushort cmd2)
        {
            uint temp = cmd1;
            uint key = (temp << 16) + cmd2;
            return key;
        }

        /// <summary>
        /// 打TCP包
        /// </summary>
        public static byte[] PackTCPMessage(ushort cmd1, ushort cmd2, byte[] data)
        {
            List<byte> datas = new List<byte>();

            byte[] data1 = BitConverter.GetBytes(cmd1);
            byte[] data2 = BitConverter.GetBytes(cmd2);
            byte[] bodyLen = BitConverter.GetBytes((ushort)(data1.Length + data2.Length + data.Length));

            //packet head
            datas.AddRange(bodyLen); // 2bytes (the length of the packet body)
            //packet body
            datas.AddRange(data1);   // 2bytes
            datas.AddRange(data2);   // 2bytes
            datas.AddRange(data);    // nbytes

            return datas.ToArray();
        }

        /// <summary>
        /// 解TCP包
        /// </summary>
        public static void UnpackTCPMessage(Socket socket, out ushort cmd1, out ushort cmd2, out byte[] data)
        {
            ushort bodyLen;
            byte[] head = new byte[2];
            socket.Receive(head);

            bodyLen = BitConverter.ToUInt16(head, 0);     // 2bytes (the length of the packet body)
            byte[] body = new byte[bodyLen];
            socket.Receive(body);

            using (MemoryStream memory = new MemoryStream(body))
            {
                using (BinaryReader reader = new BinaryReader(memory))
                {
                    cmd1 = reader.ReadUInt16();           // 2bytes
                    cmd2 = reader.ReadUInt16();           // 2bytes
                    data = reader.ReadBytes(bodyLen - 4); // nbytes
                }
            }
        }

        /// <summary>
        /// 打UDP包
        /// </summary>
        public static byte[] PackUDPMessage(ushort cmd1, ushort cmd2, byte[] data)
        {
            List<byte> datas = new List<byte>();

            byte[] data1 = BitConverter.GetBytes(cmd1);
            byte[] data2 = BitConverter.GetBytes(cmd2);

            //packet
            datas.AddRange(data1);      // 2bytes
            datas.AddRange(data2);      // 2bytes
            datas.AddRange(data);       // nbytes

            return datas.ToArray();
        }

        /// <summary>
        /// 解UDP包
        /// </summary>
        public static void UnpackUDPMessage(byte[] data, out ushort cmd1, out ushort cmd2, out byte[] msgData)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    cmd1 = reader.ReadUInt16();                         // 2bytes
                    cmd2 = reader.ReadUInt16();                         // 2bytes
                    msgData = reader.ReadBytes(data.Length - 4);        // nbytes
                }
            }
        }
    }
}