using UnityEngine;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI counterText;
        public float targetTime;
        public bool isActive;
    

        void Update()
        {
            if(isActive) targetTime += Time.deltaTime;

            counterText.text = $"{targetTime:00}";
        }
    }
}
