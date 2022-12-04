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
        // AsyncOperation�� ���� Scene Load ������ �� �� �ִ�.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Kumoh_Main");

        // Scene�� �ҷ����� ���� �Ϸ�Ǹ�, AsyncOperation�� isDone ���°� �ȴ�.
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
