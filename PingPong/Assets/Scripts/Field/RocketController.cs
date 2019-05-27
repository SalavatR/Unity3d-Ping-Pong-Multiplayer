using GameControllers;
using Network.Packets.Enum;
using UnityEngine;

namespace Field
{
    public class RocketController : MonoBehaviour
    {
        public PlayerPlace position;
        private Vector2 fieldSize => GameController.Instance.FieldSize;
        private Vector2 rocketSize => GameController.Instance.RocketSize;

        private float maxPosX => fieldSize.x / 2 - rocketSize.x / 2;
        private float minPosX => -maxPosX;


        public void MovePlayerWithRatio(float ratio)
        {
            transform.Translate(new Vector3(ratio * 100, 0, 0));
            CheckPosition();
        }

        public void MoveToPosition(float xPos)
        {
            var currentpos = transform.position;
            currentpos.x = xPos;
            transform.position = currentpos;
            CheckPosition();
        }

        void CheckPosition()
        {
            var position = transform.position;
            if (position.x > maxPosX)
            {
                position.x = maxPosX;
                transform.position = position;
            }
            else if (position.x < minPosX)
            {
                position.x = minPosX;
                transform.position = position;
            }
        }
    }
}
