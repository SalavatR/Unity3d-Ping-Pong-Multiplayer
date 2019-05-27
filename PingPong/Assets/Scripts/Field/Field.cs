using UnityEngine;

namespace Field
{
    public class Field : MonoBehaviour
    {
        public static Field Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}
