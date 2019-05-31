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
            if (gameController.isActiveDelay)
                gameController.timer.isActive = true;
            gameController.gameMenu.SetActive(false);
            gameController.isActiveDelay = true;
        }
    }
}
