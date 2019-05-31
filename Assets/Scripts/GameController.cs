using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Assets;
using Game;
using Handlers;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform cell;
    public GameObject cellsParent;
    private readonly List<List<Transform>> _sprites = new List<List<Transform>>();

    public GameObject gameInterface;
    public GameObject gameMenu;
    public GameObject gameLose;
    public GameObject gameBar;
    public GameObject helpMenu;

    public Transform purpleStar;
    public Transform orangeStar;
    public Transform greenStar;
    private readonly List<Star> _winDataCells = new List<Star>();
    public List<Star> starsList = new List<Star>();
    public int CurrentCountStars { get; set; }
    
    private float _x = -1.85f;
    private float _y = 1.75f;
    public int maxCountStars = 4;
    private const int DelayTime = 3;
    private const int CountCells = 24;
    public int level;
    
    public float targetTimeDelay;
    public bool isActiveDelay;

    public GameObject buttonDone;
    public Button buttonMenu;
    public Health health;
    public Score score;
    public Timer timer;

    void Awake()
    {
        InitField();
        buttonMenu.GetComponent<Menu>().OnClickMenu();
    }

    void Start()
    {
        buttonDone.SetActive(false);
    }

    void Update()
    {
        if (isActiveDelay)
        {
            targetTimeDelay += Time.deltaTime;

            if (targetTimeDelay >= 3)
            {
                CleanField();
                SetAble(true);
                timer.isActive = true;
                isActiveDelay = false;
            }
        }
        if (CurrentCountStars != maxCountStars || buttonDone.activeSelf) return;
        buttonDone.SetActive(true);
    }
    
    public IEnumerator DelayExec()
    {
        SetAble(false);
        yield return new WaitForSeconds(DelayTime);
        CleanField();
        SetAble(true);
        timer.isActive = true;
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
    
    public void NewGame()
    {
        gameMenu.SetActive(false);
        cellsParent.SetActive(true);
        gameInterface.SetActive(true);
        CleanField();
        timer.isActive = false;
        health.health = 3;
        score.score = 0;
        timer.targetTime = 0;
        CurrentCountStars = 0;
        maxCountStars = 4;
        gameBar.SetActive(true);
        SetRandomStars();
        StartDelay();
        // StartCoroutine(DelayExec());
    }
        
    
    public void OnClickDone()
    {
        var isWin = CheckWin();
        CleanField();
        starsList.Clear();
        if (isWin)
        {
            level++;
            if (level % 5 == 0)
            {
                maxCountStars++;
                health.health++;
            }

            score.score += level * 1000 / (int)timer.targetTime;
            SetRandomStars();
            timer.targetTime = 0;
        }
        else
        {
            health.health--;
            if (health.health <= 0)
            {
                timer.isActive = false;
                gameBar.SetActive(false);
                SetAble(false);
                gameLose.SetActive(true);
                return;
            }
            
            ShowStars();
        }
        
        timer.isActive = false;
        StartDelay();
        // StartCoroutine(DelayExec());
    }

    private void SetRandomStars()
    {
        var rand = new System.Random();
        CleanField();
        starsList.Clear();
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

    public void Undo()
    {
        if (starsList.Count <= 0) return;
        var star = starsList.ElementAt(starsList.Count - 1);
        _sprites[star.Coords.X][star.Coords.Y].GetComponent<SpriteRenderer>().sprite = null;
        starsList.RemoveAt(starsList.Count - 1);
        CurrentCountStars--;
        buttonDone.SetActive(false);
    }
    
    public void CleanField()
    {
        foreach (var keys in _sprites)
        foreach (var value in keys)
        {
            value.GetComponent<SpriteRenderer>().sprite = null;
            CurrentCountStars = 0;
            buttonDone.SetActive(false);
        }
    }

    public void SetAble(bool isEnable)
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

    private void StartDelay()
    {
        SetAble(false);
        isActiveDelay = true;
        targetTimeDelay = 0;
    }
}