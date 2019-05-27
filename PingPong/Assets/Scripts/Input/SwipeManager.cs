using System;
using UnityEngine;

namespace Input
{
    public class SwipeManager : MonoBehaviour
    {
        public static SwipeManager Instance;
        private Vector3 lastMousePosition = Vector3.zero;

        public event Action<float> OnSwiping;

        private void Awake()
        {
            Instance = this;
        }

        private void FixedUpdate()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                if (lastMousePosition == Vector3.zero)
                    lastMousePosition = UnityEngine.Input.mousePosition;
                else
                {
                    var ratio = (UnityEngine.Input.mousePosition.x - lastMousePosition.x) / Screen.width;
                    lastMousePosition = UnityEngine.Input.mousePosition;
                    OnSwiping?.Invoke(ratio);
                }
            }
            else
            {
                lastMousePosition = Vector3.zero;
            }
        }
    }
}
