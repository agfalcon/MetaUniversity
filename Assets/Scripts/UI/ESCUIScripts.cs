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
    bool isEsc; 
    
    private void Start()
    {
        isEsc= MainCharacter.GetComponent<CharacterUIManager>().isESC;
    }

    public void btnBack()
    {
        obj1.SetActive(false);
        isEsc = false;
        MainCam.GetComponent<CamRotate>().enabled = true;
    }
    
    public void btnSetting()
    {
        Option.SetActive(true);
    }

    public void btnToStart()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        StartCoroutine(LoadMyAsyncScene());
        //SceneManager.LoadScene("Kumoh_Main");
    }

    IEnumerator LoadMyAsyncScene()
    {
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Kumoh_Main");

        // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
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
