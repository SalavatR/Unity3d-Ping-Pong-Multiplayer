using Network.Packets.Base;
using Network.Packets.Enum;
using Unity.Networking.Transport;

namespace Network.Packets
{

    [Packet(PacketId = PacketsEnum.GameSettings)]
    public class GameSettings : NetworkPacket
    {
        public float fieldSizeX;
        public float fieldSizeY;
        public float rocketSizeX;
        public float rocketSizeY;
        public override void ToStream(ref DataStreamWriter writer)
        {
            writer.Write(fieldSizeX);
            writer.Write(fieldSizeY);
            writer.Write(rocketSizeX);
            writer.Write(rocketSizeY);
        }

        public override void FromStream(DataStreamReader reader, ref DataStreamReader.Context ctx)
        {
            fieldSizeX = reader.ReadFloat(ref ctx);
            fieldSizeY = reader.ReadFloat(ref ctx);
            rocketSizeX = reader.ReadFloat(ref ctx);
            rocketSizeY = reader.ReadFloat(ref ctx);
        }
    }
}
