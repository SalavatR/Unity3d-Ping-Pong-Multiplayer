using System;
using Network.Packets.Enum;
using UnityEngine;

namespace Field
{
    public class Gate : MonoBehaviour
    {
        public PlayerPlace position = PlayerPlace.Bottom;

        public event Action<PlayerPlace> OnGoal;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball")) OnGoal?.Invoke(position);
        }
    }
}
