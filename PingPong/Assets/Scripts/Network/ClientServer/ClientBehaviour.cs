using System;
using Network.ClientServer.Base;
using Network.Packets.Base;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

namespace Network.ClientServer
{
    public class ClientBehaviour : PacketsProcessor
    {
        public static ClientBehaviour Instance;

        private bool connected = false;

        private NetworkConnection m_clientToServerConnection;


        public override bool isServer => false;


        public float roketPosition;

        public event Action OnConnected;
        public event Action OnDisconnected;

        protected override void Awake()
        {
            Instance = this;
            base.Awake();
        }

        void Start()
        {
            m_Driver = new UdpNetworkDriver(new INetworkParameter[0]);
        }

        void OnDestroy()
        {
            m_Driver.Dispose();
            NetworkInfo.SetPoint(default(NetworkEndPoint));
        }


        void FixedUpdate()
        {
            m_Driver.ScheduleUpdate().Complete();

            if (NetworkInfo.ServerEndPoint.IsValid && !m_clientToServerConnection.IsCreated)
                m_clientToServerConnection = m_Driver.Connect(NetworkInfo.ServerEndPoint);


            DataStreamReader strm;
            NetworkEvent.Type cmd;
            while ((cmd = m_clientToServerConnection.PopEvent(m_Driver, out strm)) != NetworkEvent.Type.Empty)
            {
                switch (cmd)
                {
                    case NetworkEvent.Type.Connect:
                    {
                        connected = true;
                        OnConnected?.Invoke();
                        break;
                    }
                    case NetworkEvent.Type.Data:
                    {
                        ProcessData(strm, m_clientToServerConnection.InternalId);
                        break;
                    }
                    case NetworkEvent.Type.Disconnect:
                        connected = false;
                        m_clientToServerConnection = default(NetworkConnection);
                        OnDisconnected?.Invoke();
                        break;
                }
            }
        }

        public override void SendData(IPacket packet)
        {
            if (!connected)
            {
                Debug.LogError("NotConnectedToServer");
                return;
            }

            var writer = new DataStreamWriter(BufferSize, Allocator.Temp);
            writer.Write((ushort) packet.GetPacketType());
            packet.ToStream(ref writer);
            m_clientToServerConnection.Send(m_Driver, writer);
        }
    }
}
