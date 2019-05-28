using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Score : MonoBehaviour
    {
        // Start is called before the first frame update
        public Text scoreText;
        public int score;
        
        void Start()
        {
            scoreText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            scoreText.text = $"{score:0}";
        }
    }
}
