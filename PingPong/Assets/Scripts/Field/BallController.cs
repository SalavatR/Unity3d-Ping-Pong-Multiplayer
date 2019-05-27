using UnityEngine;

namespace Field
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : MonoBehaviour
    {
        public Rigidbody rigidBody;
        [SerializeField] private Renderer rend;

        private float yPos;

        public Color Color
        {
            get => rend.material.color;
            set => rend.material.color = value;
        }

        private void OnValidate()
        {
            if (!rigidBody) rigidBody = GetComponent<Rigidbody>();
            if (!rend) rend = GetComponent<Renderer>();
        }

        private void Start()
        {
            yPos = transform.position.y;
        }

        public void SetPositionAndVelocity(float xPos, float zPos, float xVel, float zVel)
        {
            SetPosition(new Vector3(xPos, yPos, zPos));
            SetVelocity(new Vector3(xVel, 0, zVel));
        }

        public void SetVelocity(Vector3 value)
        {
            rigidBody.velocity = value;
        }

        public void SetPosition(Vector3 value)
        {
            transform.position = value;
        }
    }
}
