using Network.ClientServer;
using Network.Packets;
using UnityEngine;

namespace GameControllers
{
    public interface IClientGameController
    {
        ClientBehaviour client { get; }
        Color BallColor{ get; set; }
        void StartGame();

        void GameOver();

        void MoveEnemy(float xPos);

        void SetupGame(GameSettings settings);
        void OnRoomReady();
        void MoveBall(BallPosition position);

    }
}
