using Network.ClientServer.Base;
using Network.Extentions;
using Network.Packets.Enum;
using Unity.Networking.Transport;

namespace Network.Packets.Base
{
    public abstract class NetworkPacket : IPacket
    {
        private PacketsEnum packetType;

        public NetworkPacket()
        {
            packetType = this.GetPacketEnum();
        }

        public PacketsProcessor PacketsProcessor { get; set; }

        public PacketsEnum GetPacketType()
        {
            return packetType;
        }

        public abstract void ToStream(ref DataStreamWriter writer);

        public abstract void FromStream(DataStreamReader reader, ref DataStreamReader.Context ctx);
        public void Send()
        {
            PacketsProcessor.SendData(this);
        }
    }
}
