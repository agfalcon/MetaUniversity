using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject obj1;
    public GameObject UIDataO;
    public GameObject MainCharacter;
    public GameObject PlayerUI;
    UIData uiData;
    // Start is called before the first frame update
    public void GameStart()
    {
        PlayerUI.SetActive(true);
        obj1.SetActive(false);
        uiData = UIDataO.GetComponent<UIData>();
        uiData.isStart = true;
        MainCharacter.SetActive(true);
    }
}
    