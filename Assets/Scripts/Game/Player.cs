﻿using System.Linq;
using Assets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public Sprite star;
        public GameController gameController;
        
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
        
            // if set star at cell
            hitCell.GetComponent<SpriteRenderer>().sprite = star;
            var coords = new Coord {X = cell.posCell, Y = cell.posColl};
            var existElement = gameController.starsList.Where(item => item.Coords.X == coords.X && item.Coords.Y == coords.Y).ToList();
            if (existElement.Count != 0)
            {
                gameController.starsList
                    .Where(item => item.Coords.X == coords.X && item.Coords.Y == coords.Y)
                    .Select(starObj =>
                    {
                        starObj.Sprite = sprite;
                        return starObj;
                    });
            }
            else
            {
                gameController.starsList.Add(new Star{Coords = coords, Sprite = sprite});
            }
        }

        public void PurpleClick()
        {
            star = gameController.purpleStar.sprite;
        }
        
        public void OrangeClick()
        {
            star = gameController.orangeStar.sprite;
        }
        
        public void GreenClick()
        {
            star = gameController.greenStar.sprite;
        }
    }
}
