using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        public Text counterText;
        public float targetTime;
        public bool isActive;
    
        void Start()
        {
            counterText = GetComponent<Text>();
        }

        void Update()
        {
            if(isActive){
                targetTime += Time.deltaTime;
            }
            counterText.text = $"{targetTime:00}";
        }
    }
}
