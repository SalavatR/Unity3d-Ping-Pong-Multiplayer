using Network.ClientServer.Base;
using Network.Packets.Base;
using Unity.Collections;
using Unity.Jobs;
using Unity.Networking.Transport;
using UnityEngine;

// todo: Add connection map
// todo: Move to Unity ECS
namespace Network.ClientServer
{
    public class ServerBehaviour : PacketsProcessor
    {
        public static ServerBehaviour Instance;

        private NativeList<NetworkConnection> m_connections;

        public override bool isServer => true;

        private JobHandle m_updateHandle;

        private int lastConnection;


        protected override void Awake()
        {
            Instance = this;
            base.Awake();
        }

        void Start()
        {
            m_Driver = new UdpNetworkDriver(new INetworkParameter[0]);
            var addr = NetworkEndPoint.AnyIpv4;
            addr.Port = NetworkInfo.Port;
            if (m_Driver.Bind(addr) != 0)
                Debug.Log("Failed to bind to port");
            else
                m_Driver.Listen();

            m_connections = new NativeList<NetworkConnection>(16, Allocator.Persistent);
        }

        void OnDestroy()
        {
            m_updateHandle.Complete();
            m_Driver.Dispose();
            m_connections.Dispose();
        }

        void FixedUpdate()
        {
            m_Driver.ScheduleUpdate().Complete();

            while (true)
            {
                var con = m_Driver.Accept();
                if (!con.IsCreated)
                    break;
                m_connections.Add(con);
            }

            for (int i = 0; i < m_connections.Length; ++i)
            {
                DataStreamReader strm;
                NetworkEvent.Type cmd;


                while ((cmd = m_Driver.PopEventForConnection(m_connections[i], out strm)) != NetworkEvent.Type.Empty)
                {
                    if (cmd == NetworkEvent.Type.Data)
                    {
                        ProcessData(strm, m_connections[i].InternalId);
                        lastConnection = i;
                    }
                    else if (cmd == NetworkEvent.Type.Disconnect)
                    {
                        m_connections.RemoveAtSwapBack(i);
                        lastConnection = -1;
                        if (i >= m_connections.Length)
                            break;
                    }
                }
            }
        }

        public void Disconnect(int connectionID)
        {
            for (int i = 0; i < m_connections.Length; i++)
            {
                if (connectionID == m_connections[i].InternalId)
                {
                    m_connections.RemoveAtSwapBack(connectionID);
                    lastConnection = -1;
                    if (i >= m_connections.Length)
                        break;
                }
            }
        }

        void SendData(IPacket packet, NetworkConnection connection)
        {
            var writer = new DataStreamWriter(BufferSize, Allocator.Temp);
            writer.Write((ushort) packet.GetPacketType());
            packet.ToStream(ref writer);
            m_Driver.Send(NetworkPipeline.Null, connection, writer);
        }

        public override void SendData(IPacket packet)
        {
            for (int i = 0; i < m_connections.Length; ++i)
            {
                SendData(packet, m_connections[i]);
            }
        }

        public void SendData(IPacket packet, int connectionID)
        {
            for (int i = 0; i < m_connections.Length; i++)
            {
                if (connectionID == m_connections[i].InternalId)
                {
                    SendData(packet, m_connections[i]);
                }
            }
        }

        public void SendDataExceptSender(IPacket packet, int connectionID)
        {
            for (int i = 0; i < m_connections.Length; ++i)
            {
                if (m_connections[i].InternalId == connectionID)
                    continue;


                SendData(packet, m_connections[i]);
            }
        }

        public void Reply(IPacket packet)
        {
            if (lastConnection >= 0 && lastConnection < m_connections.Length)
                SendData(packet, m_connections[lastConnection]);
        }
    }
}
