using Network.Packets.Base;
using Network.Packets.Enum;
using Unity.Networking.Transport;

namespace Network.Packets
{
    [Packet(PacketId = PacketsEnum.ClientReady)]
    public class ClientReady : NetworkPacket
    {
        public override void ToStream(ref DataStreamWriter writer)
        {
        }

        public override void FromStream(DataStreamReader reader, ref DataStreamReader.Context ctx)
        {
        }
    }
}
