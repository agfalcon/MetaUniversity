using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject obj1;
    public GameObject UIDataO;
    public GameObject PlayerUI;
    public GameObject MainCharacter;
    UIData uiData;
    // Start is called before the first frame update
    public void GameStart()
    {
        obj1.SetActive(false);
        uiData = UIDataO.GetComponent<UIData>();
        uiData.isStart = true;
        MainCharacter.GetComponent<AudioSource>().Play();
        PlayerUI.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Invoke(nameof(CameraRotateEnabled), 1f);
    }

    void CameraRotateEnabled()
    {
        Camera.main.GetComponent<CamRotate>().enabled = true;
        MainCharacter.GetComponent<PlayerRotate>().enabled = true;

    }
}
    