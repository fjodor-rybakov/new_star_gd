using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickReset);
    }

    void OnClickReset()
    {
        var gamePlayObject = GameObject.Find("GamePlay");
        gamePlayObject.GetComponent<GameController>().CleanField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
