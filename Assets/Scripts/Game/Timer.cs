using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        public Text counterText;

        public int seconds, minutes;
    
        void Start()
        {
            counterText = GetComponent<Text>();
        }

        void Update()
        {
            minutes = (int) (Time.time / 60f);
            seconds = (int) (Time.time % 60f);
            counterText.text = $"{minutes:00} : {seconds:00}";
        }
    }
}
