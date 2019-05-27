using System.Collections.Generic;
using Field;
using Input;
using Network.Packets.Enum;

namespace GameControllers
{
    public class SingleGameController : GameController
    {
        List<RocketController> players = new List<RocketController>();

        protected override void Awake()
        {
            base.Awake();

            players.Add(topPlayer);
            players.Add(bottomPlayer);
        }

        private void Start()
        {
            RestartGame();
            SubscribeEvents();
            StartGame();
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
        }

        void SetScore(PlayerPlace place)
        {
            if (place == PlayerPlace.Top)
                topScore++;
            if (place == PlayerPlace.Bottom)
                bottomScore++;

            UIManager.Instance.SetScore(topScore, bottomScore);

            CheckGameOver();
        }

        void CheckGameOver()
        {
            if (topScore >= WinScore || bottomScore >= WinScore)
            {
                GameOver();
            }
            else
            {
                ResetBall();
            }
        }


        void StartGame()
        {
            GameStarted = true;
            ballController.SetVelocity(RandomVelocity);
            ResetBall();
        }

        private void InstanceOnOnSwiping(float value)
        {
            if (!GameStarted) return;

            foreach (var pl in players)
            {
                pl.MovePlayerWithRatio(value);
            }
        }


        private void OnDestroy()
        {
            UnSubscribeEvents();
        }
    }
}
