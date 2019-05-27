using Network.Packets.Base;
using Network.Packets.Enum;
using Unity.Networking.Transport;

namespace Network.Packets
{

    [Packet(PacketId = PacketsEnum.Score)]
    public class Score : NetworkPacket
    {
        public int TopPlayerScore;
        public int BottomPlayerScore;

        public override void ToStream(ref DataStreamWriter writer)
        {
            writer.Write(TopPlayerScore);
            writer.Write(BottomPlayerScore);
        }

        public override void FromStream(DataStreamReader reader, ref DataStreamReader.Context ctx)
        {
            TopPlayerScore = reader.ReadInt(ref ctx);
            BottomPlayerScore = reader.ReadInt(ref ctx);
        }
    }
}
