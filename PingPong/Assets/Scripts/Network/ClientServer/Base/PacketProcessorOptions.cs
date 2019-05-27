using System.Collections.Generic;
using Network.Extentions;
using Network.Packets.Base;
using Network.Packets.Enum;

namespace Network.ClientServer.Base
{
    public class PacketProcessorOptions
    {

        public Dictionary<PacketsEnum, IPacket> Packets = new Dictionary<PacketsEnum, IPacket>();
        public Dictionary<PacketsEnum, PacketHandler> PacketsHanlders = new Dictionary<PacketsEnum, PacketHandler>();
        public bool AddPacket(PacketsEnum packetId, IPacket packet)
        {
            var r = Packets.ContainsKey(packetId);
            if (!r)
            {
                Packets.Add(packetId, packet);
            }
            return !r;
        }

        public void RegisterHandler(PacketsEnum packetId, PacketHandler action)
        {
            PacketHandler del;

            bool hasVal = PacketsHanlders.TryGetValue(packetId, out del);
            if (hasVal)
            {
                del += action;
            }
            else
            {
                PacketsHanlders.Add(packetId, new PacketHandler(action));
            }
        }

        public bool TryGetHandler(PacketsEnum packetId, out PacketHandler del)
        {
            return PacketsHanlders.TryGetValue(packetId, out del);
        }
    }
}
