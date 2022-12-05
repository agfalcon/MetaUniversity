using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject obj1;
    public GameObject UIDataO;
    public GameObject MainCharacter;
    public GameObject Help;
    UIData uiData;
    // Start is called before the first frame update
    public void GameStart()
    {
        obj1.SetActive(false);
        Help.SetActive(true);
        uiData = UIDataO.GetComponent<UIData>();
        uiData.isStart = true;
        MainCharacter.SetActive(true);
        
        
    }
}
    