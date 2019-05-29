using UnityEngine;

namespace Game
{
    public class Pause : MonoBehaviour
    {
        public Timer timer;
        public GameController gameController;

        public void OnClickPause()
        {
            timer.isActive = !timer.isActive;
            gameController.SetAble(timer.isActive);
        }
    }
}
