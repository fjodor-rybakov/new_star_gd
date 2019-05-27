using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Assets;
using Game;
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
    public int level;
    public int CurrentCountStars { get; set; }
    
    public GameObject buttonPrefab;
    private GameObject _canvas;
    private Health _health;
    private Score _score;
    private Timer _timer;

    void Awake()
    {
        InitField();
        SetRandomStars();
    }

    void Start()
    {
        _health = GameObject.Find("Health").GetComponentInChildren<Text>().GetComponent<Health>();
        _score = GameObject.Find("Score").GetComponentInChildren<Text>().GetComponent<Score>();
        _timer = GameObject.Find("Timer").GetComponentInChildren<Text>().GetComponent<Timer>();
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
        SetAble(true);
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
            tempObj.GetComponent<BoxCollider2D>().enabled = false;
            tempSpritesList.Add(tempObj);

            arrColl++;
            _x += 1.85f;
            if (i % 4 != 0) continue;

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
        var isWin = CheckWin();
        Debug.Log(CheckWin());
        CleanField();
        SetAble(false);
        if (isWin)
        {
            Debug.Log("You win!");
            level++;
            if (level % 5 == 0)
            {
                maxCountStars++;
                _health.health++;
            }

            _score.score += level * 1000 / (_timer.minutes * 60 + _timer.seconds);
            SetRandomStars();
        }
        else
        {
            _health.health--;
            if (_health.health <= 0)
            {
                Debug.Log("You lose!");
            }
            
            ShowStars();
        }
        
        StartCoroutine(DelayExec());
    }

    private void SetRandomStars()
    {
        var rand = new System.Random();
        _winDataCells.Clear();
        for (var i = 0; i < maxCountStars; i++)
        {
            var color = (EColors) rand.Next(Enum.GetNames(typeof(EColors)).Length - 1);
            Sprite sprite = null;
            var coords = Tools.GetCoordsPair();

            if (color == EColors.Green) sprite = orangeStar.GetComponent<SpriteRenderer>().sprite;
            else if (color == EColors.Orange) sprite = purpleStar.GetComponent<SpriteRenderer>().sprite;
            else if (color == EColors.Purple) sprite = greenStar.GetComponent<SpriteRenderer>().sprite;

            _sprites[coords.X][coords.Y].GetComponent<SpriteRenderer>().sprite = sprite;
            _winDataCells.Add(new Star{Coords = coords, Sprite = sprite});
        }
        
        Tools.AlreadyExistCoors.Clear();
    }

    private void ShowStars()
    {
        foreach (var item in _winDataCells)
        {
            _sprites[item.Coords.X][item.Coords.Y].GetComponent<SpriteRenderer>().sprite = item.Sprite;
        }
    }
    
    public void CleanField()
    {
        foreach (var keys in _sprites)
        foreach (var value in keys)
        {
            value.GetComponent<SpriteRenderer>().sprite = null;
            CurrentCountStars = 0;
            _canvas.SetActive(false);
        }
    }

    private void SetAble(bool isEnable)
    {
        foreach (var keys in _sprites)
        foreach (var value in keys)
        {
            value.GetComponent<BoxCollider2D>().enabled = isEnable;
        }
    }

    private bool CheckWin()
    {
        return _winDataCells.All(
            item => _sprites[item.Coords.X][item.Coords.Y].GetComponent<SpriteRenderer>().sprite == item.Sprite);
    }
}