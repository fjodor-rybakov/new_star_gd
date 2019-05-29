using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Menu : MonoBehaviour
    {
        public GameObject buttonRestart;
        public GameController gameController;
        public Button pause;

        private bool _isOpen;

        public void OnClickMenu()
        {
            _isOpen = !_isOpen;
            buttonRestart.SetActive(_isOpen);
            pause.GetComponent<Pause>().OnClickPause();
            pause.interactable = !_isOpen;
            gameController.SetAble(!_isOpen);
        }
    }
}
