using Network.Packets.Base;
using Network.Packets.Enum;
using Unity.Networking.Transport;

namespace Network.Packets
{

    [Packet(PacketId = PacketsEnum.OnConnect)]
    public class OnConnect : NetworkPacket
    {
        public bool isHost;

        private int isHostInt
        {
            get { return isHost ? 1 : 0; }
            set { isHost = value == 1; }
        }

        public override void ToStream(ref DataStreamWriter writer)
        {
            writer.Write(isHostInt);
        }

        public override void FromStream(DataStreamReader reader, ref DataStreamReader.Context ctx)
        {
            isHostInt = reader.ReadInt(ref ctx);
        }
    }
}
