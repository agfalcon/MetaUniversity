using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void GameStart()
    {
        GameObject obj1 = GameObject.Find("StartCanvas");
        obj1.SetActive(false);
    }
}
    