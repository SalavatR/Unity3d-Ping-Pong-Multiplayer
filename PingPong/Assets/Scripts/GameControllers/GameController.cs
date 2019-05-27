using Field;
using Network.Packets.Enum;
using UnityEngine;

namespace GameControllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        protected Gate TopGate;
        protected Gate BottomGate;

        protected RocketController topPlayer;
        protected RocketController bottomPlayer;

        protected BallController ballController;

        public Field.Field field;

        public Vector2 FieldSize = new Vector2(100, 70);

        public Vector2 RocketSize = new Vector2(8, 2);

        private CameraView cameraView;

        protected int bottomScore = 0;
        protected int topScore = 0;

        public bool GameStarted { get; protected set; }

        [SerializeField] protected int WinScore = 10;

        protected int PositiveNegative => Random.Range(0, 2) == 0 ? 1 : -1;
        protected Vector3 RandomVelocity => new Vector3(Random.Range(10f, 30f) * PositiveNegative, 0, Random.Range(10f, 30f) * PositiveNegative);

        protected Color RandomColor => new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        public Color BallColor
        {
            get => ballController.Color;
            set => ballController.Color = value;
        }

        protected virtual void Awake()
        {
            Instance = this;
            foreach (var gate in GameObject.FindObjectsOfType<Gate>())
            {
                if (gate.position == PlayerPlace.Bottom) BottomGate = gate;
                else TopGate = gate;
            }

            foreach (var rc in FindObjectsOfType<RocketController>())
            {
                if (rc.position == PlayerPlace.Bottom) bottomPlayer = rc;
                else topPlayer = rc;
            }

            ballController = FindObjectOfType<BallController>();

            cameraView = FindObjectOfType<CameraView>();

            field = FindObjectOfType<Field.Field>();
        }


        public virtual void RestartGame()
        {
            SetupField();
            SetupRocket();
        }

        public void SetupRocket()
        {
            bottomPlayer.transform.localScale =
                topPlayer.transform.localScale =
                    new Vector3(RocketSize.x, 3, RocketSize.y);

            var playerZPos = FieldSize.y / 2 - RocketSize.y / 2;
            bottomPlayer.transform.position =
                new Vector3(0, -98, -playerZPos);

            topPlayer.transform.position =
                new Vector3(0, -98, playerZPos);
        }

        protected void SetupField()
        {
            field.transform.localScale = new Vector3(FieldSize.x, 1, FieldSize.y);
            cameraView.SetCameraSize(FieldSize);
        }

        protected void SetupBall()
        {
            ballController.transform.position = new Vector3(0, -98, 0);
        }


        public void GameOver()
        {
            GameStarted = false;
            UIManager.Instance.OnGameOver();
        }

        protected virtual void ResetBall()
        {
            SetupBall();
            BallColor = RandomColor;
            ballController.SetVelocity(RandomVelocity);
        }
    }
}
