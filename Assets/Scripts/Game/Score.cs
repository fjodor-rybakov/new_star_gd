using UnityEngine;

namespace Game
{
    public class Score : MonoBehaviour
    {
        // Start is called before the first frame update
        public TMPro.TextMeshProUGUI scoreText;
        public int score;
        
        // Update is called once per frame
        void Update()
        {
            scoreText.text = $"{score:0}";
        }
    }
}
