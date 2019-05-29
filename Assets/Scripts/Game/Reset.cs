using UnityEngine;

namespace Game
{
    public class Reset : MonoBehaviour
    {
        public GameController gameController;
        
        public void OnClickReset()
        {
             gameController.Undo();
        }
    }
}
