using System;
using System.Linq;
using System.Reflection;
using Network.ClientServer.Base;
using Network.Packets.Base;

namespace Network.Extentions
{
    public delegate void PacketHandler(IPacket packet, int connectionID);

    public static class PacketMapHelper
    {
        public static void LoadPackets(this PacketProcessorOptions options, PacketsProcessor packProc)
        {
            var packets = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(IPacket).IsAssignableFrom(x) && x.IsClass && x.BaseType == typeof(NetworkPacket));

            foreach (var item in packets)
            {
                var attr = item.GetCustomAttribute<PacketAttribute>();
                if (attr == null)
                    throw new Exception($"{item.ToString()} not have packet attribute");
                var _packet = (IPacket) Activator.CreateInstance(item);
                _packet.PacketsProcessor = packProc;
                options.AddPacket(attr.PacketId, _packet);
            }
        }
    }
}
