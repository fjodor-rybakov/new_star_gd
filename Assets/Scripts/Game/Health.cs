using UnityEngine;

namespace Game
{
    public class Health : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI healthText;
        public int health;
        
        // Update is called once per frame
        void Update()
        {
            healthText.text = $"{health}";
        }
    }
}
