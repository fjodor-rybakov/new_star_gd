using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform cell;
    public Transform cellsParent;
    private Transform[,] sprites;

    public Sprite randS;
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
        SetRandomStars();
        StartCoroutine(DelayExec());
    }

    void Start()
    {
        
    }

    IEnumerator DelayExec()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        CleanField();
        print(Time.time);
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
        var rand = new System.Random();

        for (int i = 0; i < maxCountStars; i++)
        {
            var num = rand.Next(0, 3);
            if (num == 0)
                sprites[rand.Next(0, 4), rand.Next(0, 4)].GetComponent<SpriteRenderer>().sprite = orangeStar.GetComponent<SpriteRenderer>().sprite;
             else if (num == 1)
                sprites[rand.Next(0, 4), rand.Next(0, 4)].GetComponent<SpriteRenderer>().sprite = purpleStar.GetComponent<SpriteRenderer>().sprite;
            else if (num == 2) 
                sprites[rand.Next(0, 4), rand.Next(0, 4)].GetComponent<SpriteRenderer>().sprite = greenStar.GetComponent<SpriteRenderer>().sprite;
        }


    }

    void CleanField()
    {
        foreach (var x in sprites)
        {
            x.GetComponent<SpriteRenderer>().sprite = null;
        }
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
