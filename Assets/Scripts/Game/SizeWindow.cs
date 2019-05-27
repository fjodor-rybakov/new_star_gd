using UnityEngine;

namespace Game
{
    public class SizeWindow : MonoBehaviour
    {
        public int width = 1000;
        public int height = 1000;

        void Awake()
        {
            Screen.SetResolution(width, height, false);
        }
    }
}
