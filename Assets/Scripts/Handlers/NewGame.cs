using UnityEngine;

namespace Handlers
{
    public class NewGame : MonoBehaviour
    {
        public GameController gameController;

        public void OnClickNewGame()
        {
            gameController.NewGame();
        }
    }
}
