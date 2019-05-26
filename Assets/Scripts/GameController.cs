using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform cell;
    public Transform cellsParent;
    private readonly List<List<Transform>> _sprites = new List<List<Transform>>();

    public Transform purpleStar;
    public Transform orangeStar;
    public Transform greenStar;
    private readonly List<Star> _winDataCells = new List<Star>();
    
    private float _x = -1.85f;
    private float _y = 1.75f;
    public int maxCountStars = 4;
    private const int DelayTime = 3;
    private const int CountCells = 24;
    private const int CountRows = 4;
    private const int CountCols = 6;
    public int CurrentCountStars { get; set; }
    private static readonly List<Coord> AlreadyExistCoors = new List<Coord>();
    
    public GameObject buttonPrefab;
    private GameObject _canvas;

    void Awake()
    {
        InitField();
        SetRandomStars();
    }

    void Start()
    {
        StartCoroutine(DelayExec());
        CreatedButtonDone();
        _canvas.SetActive(false);
    }

    void Update()
    {
        if (CurrentCountStars == maxCountStars && !_canvas.activeSelf) _canvas.SetActive(true);
    }
    
    private IEnumerator DelayExec()
    {
        yield return new WaitForSeconds(DelayTime);
        CleanField();
    }
    
    private void InitField()
    {
        int arrColl = 0, arrCell = 0;
        var tempSpritesList = new List<Transform>();

        for (var i = 1; i <= CountCells; i++)
        {
            var tempObj = Instantiate(cell, new Vector3(_x, _y, 0), Quaternion.identity);
            tempObj.transform.SetParent(cellsParent.transform);
            tempObj.GetComponent<Cell>().posCell = arrCell;
            tempObj.GetComponent<Cell>().posColl = arrColl;
            tempSpritesList.Add(tempObj);

            arrColl++;
            _x += 1.85f;
            if (i % CountRows != 0) continue;

            _sprites.Add(tempSpritesList);
            tempSpritesList = new List<Transform>();
            arrColl = 0;
            arrCell++;
            _x = -1.85f;
            _y -= 1.85f;
        }
    }
    
    void CreatedButtonDone()
    {
        _canvas = Instantiate(buttonPrefab);
        var button = _canvas.GetComponentInChildren<Button>();

        button.GetComponent<Button>().onClick.AddListener(OnClickDone);
        button.GetComponentInChildren<Text>().text = "Done!";
    }
    void OnClickDone()
    {
        Debug.Log(CheckWin());
    }

    private void SetRandomStars()
    {
        var rand = new System.Random();

        for (var i = 0; i < maxCountStars; i++)
        {
            var color = (EColors) rand.Next(Enum.GetNames(typeof(EColors)).Length - 1);
            Sprite sprite = null;
            var coords = GetCoordsPair();

            if (color == EColors.Green) sprite = orangeStar.GetComponent<SpriteRenderer>().sprite;
            else if (color == EColors.Orange) sprite = purpleStar.GetComponent<SpriteRenderer>().sprite;
            else if (color == EColors.Purple) sprite = greenStar.GetComponent<SpriteRenderer>().sprite;

            _sprites[coords.X][coords.Y].GetComponent<SpriteRenderer>().sprite = sprite;
            _winDataCells.Add(new Star{Coords = coords, Sprite = sprite});
        }
    }

    private static Coord GetCoordsPair()
    {
        var rand = new System.Random();
        int xPos = rand.Next(CountCols), yPos = rand.Next(CountRows);
        var coords = new Coord {X = xPos, Y =  yPos};

        while (AlreadyExistCoors.FirstOrDefault(i => i.X == xPos && i.Y == yPos) != null)
        {
            xPos = rand.Next(CountCols - 1);
            yPos = rand.Next(CountRows - 1);
            coords = new Coord {X = xPos, Y = yPos};
        }

        AlreadyExistCoors.Add(coords);

        return coords;
    }

    private void CleanField()
    {
        foreach (var keys in _sprites)
            foreach (var value in keys)
                value.GetComponent<SpriteRenderer>().sprite = null;
    }

    private bool CheckWin()
    {
        return _winDataCells.All(
            item => _sprites[item.Coords.X][item.Coords.Y].GetComponent<SpriteRenderer>().sprite == item.Sprite);
    }
}