using GameControllers;
using Network.Packets;
using Network.Packets.Base;
using Network.Packets.Enum;
using UnityEngine;

namespace Network.ClientServer
{
    public class ClientHandlers : MonoBehaviour
    {
        public static ClientHandlers Instance;

        private ClientBehaviour client => ClientBehaviour.Instance;
        private IClientGameController gameController;

        private void Awake()
        {
            Instance = this;
            gameController = GetComponent<IClientGameController>();
        }

        private void Start()
        {
            client.OnConnected += OnConnected;
            client.OnDisconnected += OnDisconnected;
            client.RegisterHandler(PacketsEnum.Disconnect, OnDisconnect);
            client.RegisterHandler(PacketsEnum.StartGame, OnStartGame);
            client.RegisterHandler(PacketsEnum.BallMove, OnBallMoveRecieve);
            client.RegisterHandler(PacketsEnum.RocketMove, OnRocketMove);
            client.RegisterHandler(PacketsEnum.Score, OnScore);
            client.RegisterHandler(PacketsEnum.GameSettings, OnGameSettings);
            client.RegisterHandler(PacketsEnum.RoomReady, OnRoomReady);
            client.RegisterHandler(PacketsEnum.GameOver, OnGameOver);
            client.RegisterHandler(PacketsEnum.BallCharacteristics, OnBallColor);
        }

        private void OnBallColor(IPacket packet, int connectionid)
        {
            gameController.BallColor = ((BallCharacteristics) packet).ToColor();
        }

        private void OnDisconnected()
        {
            gameController.GameOver();
        }

        private void OnGameOver(IPacket packet, int connectionid)
        {
            gameController.GameOver();
        }

        private void OnRoomReady(IPacket packet, int connectionid)
        {
            gameController.OnRoomReady();
        }

        private void OnGameSettings(IPacket packet, int connectionid)
        {
            gameController.SetupGame(packet as GameSettings);
            client.SendData(new ClientReady());
        }

        private void OnScore(IPacket packet, int connectionid)
        {
            var score = packet as Score;
            UIManager.Instance.SetScore(score.TopPlayerScore, score.BottomPlayerScore);
        }

        private void OnRocketMove(IPacket packet, int connectionid)
        {
            var enemyPos = packet as RocketPosition;
            if (enemyPos != null) gameController.MoveEnemy(enemyPos.xPosition);
        }

        private void OnConnected()
        {
            client.SendData(new OnConnect());
        }

        private void OnBallMoveRecieve(IPacket packet, int connectionid)
        {
            gameController.MoveBall(packet as BallPosition);
        }

        private void OnStartGame(IPacket packet, int connectionid)
        {
            gameController.StartGame();
        }

        private void OnDisconnect(IPacket packet, int connectionid)
        {
            gameController.GameOver();
        }
    }
}
