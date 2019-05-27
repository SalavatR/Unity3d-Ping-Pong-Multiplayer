using Network.ClientServer.Base;
using Network.Packets.Enum;
using Unity.Networking.Transport;

namespace Network.Packets.Base
{
    public interface IPacket
    {
        PacketsProcessor PacketsProcessor { get; set; }
        PacketsEnum GetPacketType();


        void ToStream(ref DataStreamWriter writer);
        void FromStream(DataStreamReader reader, ref DataStreamReader.Context ctx);

        void Send();
    }
}
