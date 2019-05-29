using Assets;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public Sprite star;

        private GameObject _gamePlayObject;
        private GameController _gameController;

        void Awake()
        {
            _gamePlayObject = GameObject.Find("GamePlay");
            _gameController = _gamePlayObject.GetComponent<GameController>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                ClickPlayer();
        }
    
        private void ClickPlayer()
        {
            if (Camera.main == null) return;
            Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(clickPoint, Vector2.zero);

            if (!hit.collider) return;
            var hitCell = hit.transform;
            var cell = hitCell.GetComponent<Cell>();
            var sprite = hitCell.GetComponent<SpriteRenderer>().sprite;

            // if select star on bar
            if (!cell) star = sprite;
        
            if (sprite || !cell) return;
        
            // if set star at cell
            hitCell.GetComponent<SpriteRenderer>().sprite = star;
            var coords = new Coord {X = cell.posCell, Y = cell.posColl};
            _gameController.starsList.Add(new Star{Coords = coords, Sprite = sprite});
            _gameController.CurrentCountStars++;
        }
    }
}
