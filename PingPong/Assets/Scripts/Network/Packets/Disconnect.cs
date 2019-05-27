using Network.Packets.Base;
using Network.Packets.Enum;
using Unity.Networking.Transport;

namespace Network.Packets
{
    [Packet(PacketId = PacketsEnum.Disconnect)]
    public class Disconnect : NetworkPacket
    {
        public DisconnectCauseEnum DisconnectCauseEnum;
        public override void ToStream(ref DataStreamWriter writer)
        {
            writer.Write((ushort) DisconnectCauseEnum);
        }

        public override void FromStream(DataStreamReader reader, ref DataStreamReader.Context ctx)
        {
            DisconnectCauseEnum = (DisconnectCauseEnum) reader.ReadUShort(ref ctx);
        }
    }
}
