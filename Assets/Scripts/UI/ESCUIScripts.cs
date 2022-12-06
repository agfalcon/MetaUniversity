using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCUIScripts : MonoBehaviour
{
    public GameObject obj1;
    public GameObject MainCam;
    public GameObject MainCharacter;
    public GameObject Option;
    public GameObject PlayerUI;
    bool isEsc;

    private void Start()
    {
        isEsc = MainCharacter.GetComponent<CharacterUIManager>().isESC;
    }

    public void btnBack()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerUI.SetActive(true);
        obj1.SetActive(false);
        isEsc = false;
        MainCam.GetComponent<CamRotate>().enabled = true;
        MainCharacter.GetComponent<PlayerRotate>().enabled = true;
        MainCharacter.GetComponent<PlayerMove>().enabled = true;
    }

    public void btnSetting()
    {
        Option.SetActive(true);
    }

    public void btnToStart()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        StartCoroutine(LoadMyAsyncScene());
    }

    IEnumerator LoadMyAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Kumoh_Main");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void btnGameExit()
    {
        Application.Quit();
    }
}
