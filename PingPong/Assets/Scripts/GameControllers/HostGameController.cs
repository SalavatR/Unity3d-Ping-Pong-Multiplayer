using Input;
using Network.ClientServer;
using Network.Packets;
using Network.Packets.Enum;
using UnityEngine;

namespace GameControllers
{
    public class HostGameController : GameController, IClientGameController
    {
        public ClientBehaviour client => ClientBehaviour.Instance;
        const float synBallPoitionInterval = 0.1f;

        private float lastSyncBallTime;
        private void Start()
        {
            RestartGame();
            SubscribeEvents();
        }

        public void StartGame()
        {
            GameStarted = true;
            UIManager.Instance.SetActiveWaitPanel((false));
            RestartGame();
            ResetBall();
        }

        private void SubscribeEvents()
        {
            SwipeManager.Instance.OnSwiping += InstanceOnOnSwiping;
            TopGate.OnGoal += OnGoal;
            BottomGate.OnGoal += OnGoal;
        }

        void UnSubscribeEvents()
        {
            SwipeManager.Instance.OnSwiping -= InstanceOnOnSwiping;
            TopGate.OnGoal -= OnGoal;
            BottomGate.OnGoal -= OnGoal;
        }

        private void OnGoal(PlayerPlace place)
        {
            SetScore(place);

            CheckGameOver();
        }

        void SetScore(PlayerPlace place)
        {
            if (place == PlayerPlace.Top)
                topScore++;
            if (place == PlayerPlace.Bottom)
                bottomScore++;

            UIManager.Instance.SetScore(topScore, bottomScore);

            client.SendData(new Score()
            {
                TopPlayerScore = topScore,
                BottomPlayerScore = bottomScore
            });
        }

        void CheckGameOver()
        {
            if (topScore >= WinScore || bottomScore >= WinScore)
            {
                client.SendData(new GameOver());
            }
            else
            {
                ResetBall();
            }
        }

        private void InstanceOnOnSwiping(float value)
        {
            if (!GameStarted) return;

            bottomPlayer.MovePlayerWithRatio(value);
        }


        public void MoveEnemy(float xPos)
        {
            topPlayer.MoveToPosition(xPos);
        }

        public void SetupGame(GameSettings settings)
        {
        }

        public void OnRoomReady()
        {
            client.SendData(new GameSettings()
            {
                fieldSizeX = FieldSize.x,
                fieldSizeY = FieldSize.y,
                rocketSizeX = RocketSize.x,
                rocketSizeY = RocketSize.y
            });

            client.SendData(new ClientReady());
        }

        private void FixedUpdate()
        {
            SendPlayerPosToServer();
            SendBallPosToServer();
        }

        public void MoveBall(BallPosition position)
        {
        }

        void SendPlayerPosToServer()
        {
            if (GameStarted)
            {
                client.SendData(new RocketPosition()
                {
                    xPosition = bottomPlayer.transform.position.x
                });
            }
        }

        private void SendBallPosToServer()
        {
            if (GameStarted && lastSyncBallTime + synBallPoitionInterval <= Time.fixedTime)
            {
                var position = ballController.transform.position;
                var velocity = ballController.rigidBody.velocity;
                ClientBehaviour.Instance.SendData(new BallPosition()
                {
                    xPos = position.x,
                    zPos = position.z,
                    xVel = velocity.x,
                    zVel = velocity.z
                });
                lastSyncBallTime = Time.fixedTime;
            }
        }

        protected override void ResetBall()
        {
            base.ResetBall();
            client.SendData(new BallCharacteristics()
            {
                Color = BallColor
            });
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }
    }
}
