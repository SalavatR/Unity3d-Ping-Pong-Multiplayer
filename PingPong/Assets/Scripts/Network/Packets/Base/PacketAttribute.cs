using System;
using Network.Packets.Enum;

namespace Network.Packets.Base
{
    
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PacketAttribute : Attribute
    {
        public PacketsEnum PacketId { get; set; }
    }
}
