using System;
using System.Linq;
using Network.Packets.Base;
using Network.Packets.Enum;

namespace Network.Extentions
{
    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            if (type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() is TAttribute att)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }

        public static PacketsEnum GetPacketEnum(this IPacket packet)
        {
            return packet.GetType().GetAttributeValue((PacketAttribute x) => x.PacketId);
        }
    }
}
