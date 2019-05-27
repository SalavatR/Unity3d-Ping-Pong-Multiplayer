using Network.Packets.Base;
using Network.Packets.Enum;
using Unity.Networking.Transport;
using UnityEngine;

namespace Network.Packets
{
    [Packet(PacketId = PacketsEnum.BallCharacteristics)]
    public class BallCharacteristics : NetworkPacket
    {
        public Color Color;

        public override void ToStream(ref DataStreamWriter writer)
        {
            writer.Write(Color.r);
            writer.Write(Color.g);
            writer.Write(Color.b);
        }

        public override void FromStream(DataStreamReader reader, ref DataStreamReader.Context ctx)
        {
            Color.r = reader.ReadFloat(ref ctx);
            Color.g = reader.ReadFloat(ref ctx);
            Color.b = reader.ReadFloat(ref ctx);
        }

        public Color ToColor()
        {
            return Color;
        }
    }
}
