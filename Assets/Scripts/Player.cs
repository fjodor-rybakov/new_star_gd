using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite star;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickPlayer()
    {
        Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPoint, Vector2.zero);

        if (hit.collider)
        {
            Transform hitCell = hit.transform;

            if (hitCell.GetComponent<Star>() != null)
            {
                star = hitCell.GetComponent<SpriteRenderer>().sprite;
            }

            if (!hitCell.GetComponent<SpriteRenderer>().sprite)
            {
                hitCell.GetComponent<SpriteRenderer>().sprite = star;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ClickPlayer();
    }
}
