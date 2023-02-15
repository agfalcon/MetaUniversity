using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject obj1;
    public GameObject UIDataO;
    public GameObject PlayerUI;
    public GameObject MainCharacter;
    public TMP_InputField nameField;
    public Animator nameFieldAnim;
    UIData uiData;
    public void GameStart()
    {
        if (nameField.text == "")
        {
            nameFieldAnim.SetTrigger("Error");
            return;
        }
       
        PlayerMove.Instance.playerName = nameField.text;
        Debug.Log(PlayerMove.Instance.playerName);
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
    