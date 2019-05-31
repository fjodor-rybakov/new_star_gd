using UnityEngine;

namespace Handlers
{
    public class Help : MonoBehaviour
    {
        public GameController gameController;

        public void OnClickHelp()
        {
            gameController.helpMenu.SetActive(true);
            gameController.gameMenu.SetActive(false);
        }
    }
}
