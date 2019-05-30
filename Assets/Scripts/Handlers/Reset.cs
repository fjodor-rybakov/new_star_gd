using UnityEngine;

namespace Handlers
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
