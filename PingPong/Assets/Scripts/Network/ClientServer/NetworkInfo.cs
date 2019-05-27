using Unity.Networking.Transport;

namespace Network.ClientServer
{
    public static class NetworkInfo
    {
        public const ushort Port = 9000;
        public static NetworkEndPoint ServerEndPoint { get; private set; } = default(NetworkEndPoint);


        public static void SetPoint(string address)
        {
            ServerEndPoint = NetworkEndPoint.Parse(address, Port);
        }

        public static void SetPoint(NetworkEndPoint endPoint)
        {
            ServerEndPoint = endPoint;
        }

        public static void SetLocalPoint()
        {
            var endpoint = NetworkEndPoint.LoopbackIpv4;
            endpoint.Port = Port;
            SetPoint(endpoint);
        }
    }
}
