using Input;
using Network.ClientServer;
using Network.Packets;
using UnityEngine;

namespace GameControllers
{
    public class ClientGameController : GameController, IClientGameController
    {
        public ClientBehaviour client => ClientBehaviour.Instance;

        private void Start()
        {
            SubscribeEvents();
        }

        public void StartGame()
        {
            GameStarted = true;
            UIManager.Instance.SetActiveWaitPanel((false));
            RestartGame();
        }

        private void SubscribeEvents()
        {
            SwipeManager.Instance.OnSwiping += InstanceOnOnSwiping;
        }

        void UnSubscribeEvents()
        {
            SwipeManager.Instance.OnSwiping -= InstanceOnOnSwiping;
        }

        private void InstanceOnOnSwiping(float value)
        {
            if (!GameStarted) return;

            topPlayer.MovePlayerWithRatio(value);
        }


        public void MoveEnemy(float xPos)
        {
            bottomPlayer.MoveToPosition(xPos);
        }

        public void SetupGame(GameSettings settings)
        {
            FieldSize = new Vector2(settings.fieldSizeX, settings.fieldSizeY);
            RocketSize = new Vector2(settings.rocketSizeX, settings.rocketSizeY);
        }

        public void OnRoomReady()
        {
        }

        private void FixedUpdate()
        {
            SendPlayerPosToServer();
        }

        void SendPlayerPosToServer()
        {
            if (GameStarted)
            {
                client.SendData(new RocketPosition()
                {
                    xPosition = topPlayer.transform.position.x
                });
            }
        }

        public void MoveBall(BallPosition position)
        {
            ballController.SetPositionAndVelocity(position.xPos, position.zPos, position.xVel, position.zVel);
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }
    }
}
