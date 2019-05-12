using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform cell;
    public Transform cellsParent;
    private Transform[,] sprites;

    public Transform purpleStar;
    public Transform orangeStar;
    public Transform greenStar;
    public Transform selectBarParent;
    private List<Transform> stars = new List<Transform>();
    private float x = -1.85f;
    private float y = 1.75f;
    public int maxCountStars = 4;

    void Awake()
    {
        InitField();
        CreateSelectBar();
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
            Transform tempObj = Instantiate(cell, new Vector3(x, y, 0), Quaternion.identity);
            sprites[arrColl, arrCell] = tempObj;
            tempObj.transform.SetParent(cellsParent.transform);
            tempObj.GetComponent<Cell>().posCell = arrCell;
            tempObj.GetComponent<Cell>().posColl = arrColl;

            arrColl++;
            x += 1.85f;

            if (i % 4 != 0) continue;

            arrColl = 0;
            arrCell++;
            x = -1.85f;
            y -= 1.85f;
        }
    }

    void SetRandomStars()
    {
        /*Random rand = new Random();

        for (int i = 0; i < maxCountStars; i++)
        {

        }*/
    }

    void CreateSelectBar()
    {
        Transform tempObjPurpleStar = Instantiate(purpleStar, new Vector3(x + 1, y, 0), Quaternion.identity);
        Transform tempObjOrangeStar = Instantiate(orangeStar, new Vector3(x + 2.85f, y, 0), Quaternion.identity);
        Transform tempObjGreenStar = Instantiate(greenStar, new Vector3(x + 4.7f, y, 0), Quaternion.identity);

        tempObjPurpleStar.transform.SetParent(selectBarParent.transform);
        tempObjOrangeStar.transform.SetParent(selectBarParent.transform);
        tempObjGreenStar.transform.SetParent(selectBarParent.transform);

        stars.Add(tempObjPurpleStar);
        stars.Add(tempObjOrangeStar);
        stars.Add(tempObjGreenStar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
