using UnityEngine;

namespace Handlers
{
    public class Menu : MonoBehaviour
    {
        public GameController gameController;

        private bool _isOpen;

        public void OnClickMenu()
        {
            gameController.cellsParent.SetActive(false);
            gameController.gameInterface.SetActive(false);
            gameController.timer.isActive = false;
            gameController.gameLose.SetActive(false);
            gameController.gameMenu.SetActive(true);
        }
    }
}
