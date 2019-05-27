using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Reset : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClickReset);
        }

        void OnClickReset()
        {
            var gamePlayObject = GameObject.Find("GamePlay");
            gamePlayObject.GetComponent<GameController>().CleanField();
        }
    }
}
