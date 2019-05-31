using System.IO;
using UnityEngine;

namespace Handlers
{
    public class Menu : MonoBehaviour
    {
        public GameController gameController;
        private const string FileName = @"/PlayerSave.json";

        public void OnClickMenu()
        {
            gameController.cellsParent.SetActive(false);
            gameController.gameInterface.SetActive(false);
            gameController.timer.isActive = false;
            gameController.isActiveDelay = false;
            gameController.gameLose.SetActive(false);
            gameController.gameMenu.SetActive(true);
            gameController.helpMenu.SetActive(false);

            /*if (gameController.health.health == 0) return;
            using (var sw = new StreamWriter(Application.persistentDataPath + FileName))
            {
                sw.WriteLine($"{gameController.score} {gameController.timer} {gameController.health} {gameController.level}");
            }*/
        }
    }
}
