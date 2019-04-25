using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeWindow : MonoBehaviour
{
    public int width = 1000;
    public int height = 1000;

    void Awake()
    {
        Screen.SetResolution(width, height, false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
