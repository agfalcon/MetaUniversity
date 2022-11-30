using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    public GameObject escUI;
    public bool isESC = false;
    public bool isM = false;
    float timer;
    float timer2;
    float waitingTime;
    bool isWait = false;
    bool isWait2 = false;
    public GameObject MainCharacter;
    public GameObject MainCam;
    public GameObject MiniCam;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        timer2 = 0.0f;
        timer = 0.0f;
        waitingTime = 0.2f;
        camera = MiniCam.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        if (timer > waitingTime)
        {
            //Action
            timer = 0;
            isWait = false;
        }
        if (timer2 > waitingTime)
        {
            timer2 = 0;
            isWait2 = false;
        }
        isEsc();
        isMap();
    }

    public void isEsc()
    {
        if (!isWait && isESC && Input.GetKey(KeyCode.Escape))
        {
            escUI.SetActive(false);
            isESC = false;
            isWait = true;
            timer = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = true;
        }
        else if (!isWait && !isESC && Input.GetKey(KeyCode.Escape))
        {
            isESC = true;
            escUI.SetActive(true);
            isWait = true;
            timer = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = false;

        }
    }
    public void isMap()
    {
        if (!isWait2 && isM && Input.GetKey(KeyCode.M))
        {
            camera.enabled = false;
            isM = false;
            isWait2 = true;
            timer2 = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = true;
        }
        else if (!isWait2 && !isM && Input.GetKey(KeyCode.M))
        {
            camera.enabled = true;
            isM = true;
            isWait2 = true;
            timer2 = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = false;

        }
    }

}