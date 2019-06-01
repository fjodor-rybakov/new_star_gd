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
    public readonly List<List<Transform>> Sprites = new List<List<Transform>>();

    public GameObject gameInterface;
    public GameObject gameMenu;
    public GameObject gameLose;
    public GameObject gameBar;
    public GameObject helpMenu;
    public GameObject successText;
    public GameObject failureText;

    public Image purpleStar;
    public Image orangeStar;
    public Image greenStar;
    private readonly List<Star> _winDataCells = new List<Star>();
    public List<Star> starsList = new List<Star>();
    
    private float _x = -1.85f;
    private float _y = 1.75f;
    public int maxCountStars = 4;
    private const int DelayTime = 4;
    private const int CountCells = 24;
    public int level;
    
    public float targetTimeDelay;
    public bool isActiveDelay;
    public bool isGame;

    public GameObject buttonDone;
    public Button buttonMenu;
    public Health health;
    public Score score;
    public Timer timer;

    void Awake()
    {
        InitField();
        buttonMenu.GetComponent<Menu>().OnClickMenu();
        successText.SetActive(false);
        failureText.SetActive(false);
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
                isGame = true;
            }
        }
        if (starsList.Count != maxCountStars || buttonDone.activeSelf) return;
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

            Sprites.Add(tempSpritesList);
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
        maxCountStars = 4;
        gameBar.SetActive(true);
        SetRandomStars();
        StartDelay();
    }
        
    
    public void OnClickDone()
    {
        var isWin = CheckWin();
        CleanField();
        starsList.Clear();
        if (isWin)
        {
            StartCoroutine(ShowSuccess());
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

            StartCoroutine(ShowFailure());
            ShowStars();
        }
        
        timer.isActive = false;
        StartDelay();
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
            var coords = Tools.GetCoordsPair();

            var sprite = GetStarSprite(color);

            Sprites[coords.X][coords.Y].GetComponent<SpriteRenderer>().sprite = sprite;
            _winDataCells.Add(new Star{Coords = coords, Sprite = sprite, Color = color});
        }
        
        Tools.AlreadyExistCoors.Clear();
    }

    public Sprite GetStarSprite(EColors color)
    {
        switch (color)
        {
            case EColors.Green:
                return orangeStar.sprite;
            case EColors.Orange:
                return purpleStar.sprite;
            case EColors.Purple:
                return greenStar.sprite;
            case EColors.None:
                return null;
            default:
                return null;
        }
    }

    private void ShowStars()
    {
        foreach (var item in _winDataCells)
        {
            Sprites[item.Coords.X][item.Coords.Y].GetComponent<SpriteRenderer>().sprite = item.Sprite;
        }
    }

    public void Undo()
    {
        if (starsList.Count <= 0) return;
        var star = starsList.ElementAt(starsList.Count - 1);
        Sprites[star.Coords.X][star.Coords.Y].GetComponent<SpriteRenderer>().sprite = null;
        starsList.RemoveAt(starsList.Count - 1);
        buttonDone.SetActive(false);
    }
    
    public void CleanField()
    {
        foreach (var keys in Sprites)
        foreach (var value in keys)
        {
            value.GetComponent<SpriteRenderer>().sprite = null;
            buttonDone.SetActive(false);
        }
        starsList.Clear();
    }

    public void SetAble(bool isEnable)
    {
        foreach (var keys in Sprites)
        foreach (var value in keys)
        {
            value.GetComponent<BoxCollider2D>().enabled = isEnable;
        }
    }

    private bool CheckWin()
    {
        return _winDataCells.All(
            item => Sprites[item.Coords.X][item.Coords.Y].GetComponent<SpriteRenderer>().sprite == item.Sprite);
    }

    private IEnumerator ShowSuccess()
    {
        successText.SetActive(true);
        yield return new WaitForSeconds(1);
        successText.SetActive(false);
    }

    private IEnumerator ShowFailure()
    {
        failureText.SetActive(true);
        yield return new WaitForSeconds(1);
        failureText.SetActive(false);
    }

    private void StartDelay()
    {
        SetAble(false);
        isActiveDelay = true;
        targetTimeDelay = 0;
        isGame = false;
    }
}