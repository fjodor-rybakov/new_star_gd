using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform cell;
    public Transform cellsParent;
    private Transform[,] sprites;

    private float x = -1.85f;
    private float y = 1.75f;

    void Awake()
    {
        InitField();
    }

    void Start()
    {
        
    }

    void InitField()
    {
        int arrColl = 0, arrCell = 0;
        sprites = new Transform[4, 4];
        for (int i = 1; i <= 16; i++)
        {
            Transform tempObj = (Transform)Instantiate(cell, new Vector3(x, y, 0), Quaternion.identity);
            sprites[arrColl, arrCell] = tempObj;
            tempObj.transform.SetParent(cellsParent.transform);
            tempObj.GetComponent<Cell>().posCell = arrCell;
            tempObj.GetComponent<Cell>().posColl = arrColl;

            arrColl++;
            x += 1.85f;

            if (i % 4 == 0)
            {
                arrColl = 0;
                arrCell++;
                x = -1.85f;
                y -= 1.85f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
