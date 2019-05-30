using UnityEngine;

namespace Game
{
    public class ResultScore : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI scoreText;
        public Score scoreObject;
        
        void Update()
        {
            scoreText.text = $"Result score: {scoreObject.score:0}";
        }
    }
}
