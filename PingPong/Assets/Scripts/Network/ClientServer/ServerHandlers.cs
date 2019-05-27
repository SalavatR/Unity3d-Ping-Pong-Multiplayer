using Network.Packets;
using Network.Packets.Base;
using Network.Packets.Enum;

namespace Network.ClientServer
{
    //todo: Add rooms class for dedicated
    public class ServerHandlers : UnityEngine.MonoBehaviour
    {
        private ServerBehaviour server => ServerBehaviour.Instance;

        private int clientsToStartGame = 2;
        private int clientsReady = 0;
        private int _clientsConnected = 0;

        public int ClientsConnected
        {
            get => _clientsConnected;
            set { _clientsConnected++; }
        }

        private void Start()
        {
            server.RegisterHandler(PacketsEnum.BallMove, OnBallMoveRecieve);
            server.RegisterHandler(PacketsEnum.OnConnect, OnConnect);
            server.RegisterHandler(PacketsEnum.RocketMove, OnRocketMove);
            server.RegisterHandler(PacketsEnum.Score, OnScore);
            server.RegisterHandler(PacketsEnum.GameSettings, OnGameSettings);
            server.RegisterHandler(PacketsEnum.GameOver, OnGameOver);
            server.RegisterHandler(PacketsEnum.ClientReady, OnClientReady);
            server.RegisterHandler(PacketsEnum.BallCharacteristics, OnBallColor);
        }

        private void OnBallColor(IPacket packet, int connectionid)
        {
            server.SendDataExceptSender(packet, connectionid);
        }

        private void OnGameOver(IPacket packet, int connectionid)
        {
            server.SendData(packet);
        }

        private void OnClientReady(IPacket packet, int connectionid)
        {
            clientsReady++;

            if (clientsReady == clientsToStartGame)
            {
                server.SendData(new StartGame());
            }
        }

        private void OnGameSettings(IPacket packet, int connectionid)
        {
            server.SendDataExceptSender(packet, connectionid);
        }

        private void OnScore(IPacket packet, int connectionid)
        {
            server.SendDataExceptSender(packet, connectionid);
        }

        private void OnRocketMove(IPacket packet, int connectionid)
        {
            server.SendDataExceptSender(packet, connectionid);
        }

        private void OnConnect(IPacket packet, int connectionid)
        {
            ClientsConnected++;

            if (ClientsConnected == clientsToStartGame)
            {
                server.SendData(new RoomReady());
            }
            else if (ClientsConnected > clientsToStartGame)
            {
//                server.SendData(new Disconnect() {DisconnectCauseEnum = DisconnectCauseEnum.MaxPlayers});
//                server.Disconnect(connectionid);
            }
        }

        private void OnBallMoveRecieve(IPacket packet, int connectionID)
        {
            ServerBehaviour.Instance.SendDataExceptSender(packet, connectionID);
        }
    }
}
