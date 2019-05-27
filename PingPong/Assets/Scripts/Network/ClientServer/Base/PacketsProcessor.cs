using Network.Extentions;
using Network.Packets.Base;
using Network.Packets.Enum;
using Unity.Networking.Transport;
using UnityEngine;

namespace Network.ClientServer.Base
{
    public abstract class PacketsProcessor : MonoBehaviour
    {
        protected const int BufferSize = 1472;
        public virtual bool isServer { get; }


        public UdpNetworkDriver m_Driver;

        protected virtual void Awake()
        {
            LoadPackets();
        }

        protected PacketProcessorOptions Options { get; set; } = new PacketProcessorOptions();

        private void LoadPackets()
        {
            Options.LoadPackets(this);
        }


        protected virtual void ProcessData(DataStreamReader reader, int connectionID)
        {
            DataStreamReader.Context readerCtx = default(DataStreamReader.Context);
            var packetID = (PacketsEnum) reader.ReadUShort(ref readerCtx);
            Debug.Log(packetID + " " + connectionID);
            var packet = Options.Packets[packetID];
            packet.FromStream(reader, ref readerCtx);
            InvokeHandler(packetID, packet, connectionID);
        }

        public void RegisterHandler(PacketsEnum packetId, PacketHandler action)
        {
            Options.RegisterHandler(packetId, action);
        }

        protected void InvokeHandler(PacketsEnum packetId, IPacket data, int connectionID)
        {
            PacketHandler handler;
            if (Options.TryGetHandler(packetId, out handler))
            {
                handler.Invoke(data, connectionID);
            }
        }

        public abstract void SendData(IPacket packet);
    }
}
