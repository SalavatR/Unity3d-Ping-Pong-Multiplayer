using Network.Packets.Base;
using Network.Packets.Enum;
using Unity.Networking.Transport;

namespace Network.Packets
{
    [Packet(PacketId = PacketsEnum.RocketMove)]
    public class RocketPosition : NetworkPacket
    {
        public float xPosition;

        public override void ToStream(ref DataStreamWriter writer)
        {
            writer.Write(xPosition);
        }

        public override void FromStream(DataStreamReader reader, ref DataStreamReader.Context ctx)
        {
            xPosition = reader.ReadFloat(ref ctx);
        }
    }
}
