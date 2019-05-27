using Network.Packets.Base;
using Network.Packets.Enum;
using Unity.Networking.Transport;

namespace Network.Packets
{
    [Packet(PacketId = PacketsEnum.BallMove)]
    public class BallPosition : NetworkPacket
    {
        public float xPos;
        public float zPos;
        public float xVel;
        public float zVel;

        public override void ToStream(ref DataStreamWriter writer)
        {
            writer.Write(xPos);
            writer.Write(zPos);
            writer.Write(xVel);
            writer.Write(zVel);
        }

        public override void FromStream(DataStreamReader reader, ref DataStreamReader.Context ctx)
        {
            xPos = reader.ReadFloat(ref ctx);
            zPos = reader.ReadFloat(ref ctx);
            xVel = reader.ReadFloat(ref ctx);
            zVel = reader.ReadFloat(ref ctx);
        }

        public override string ToString()
        {
            return $"BallPosition {xPos} {zPos} {xVel} {zVel}";
        }
    }
}
