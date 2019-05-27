using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Health : MonoBehaviour
    {
        public Text healthText;
        public int health = 3;
        
        void Start()
        {
            healthText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            healthText.text = $"{health} hp";
        }
    }
}
