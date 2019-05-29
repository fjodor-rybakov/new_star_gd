using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Reset : MonoBehaviour
    {
        public GameController gameController;
        
        public void OnClickReset()
        {
             gameController.CleanField();
        }
    }
}
