using UnityEngine;

namespace Handlers
{
    public class Continue : MonoBehaviour
    {
        public GameController gameController;

        public void OnClickContinue()
        {
            if (gameController.health.health == 0) return;
            gameController.cellsParent.SetActive(true);
            gameController.gameInterface.SetActive(true);
            gameController.gameMenu.SetActive(false);
            if (gameController.isGame) gameController.timer.isActive = true;
            else gameController.isActiveDelay = true;
        }
    }
}
