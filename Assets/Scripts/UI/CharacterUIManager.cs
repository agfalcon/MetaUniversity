using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterUIManager : MonoBehaviour
{
    public GameObject escUI;
    public GameObject HelpUI;
    public GameObject PlayerUI;
    public bool isESC = false;
    public bool isM = false;
    public bool isHelp = false;
    float timer;
    float timer2;
    float timer3;
    float waitingTime;
    bool isWait = false;
    bool isWait2 = false;
    bool isWait3 = false;
    public GameObject MainCharacter;
    public GameObject MainCam;
    public GameObject MiniCam;
    Camera camera;
    public GameObject MiniMap;
    public GameObject CharPlace;
    // Start is called before the first frame update
    void Start()
    {
        timer3 = 0.0f;
        timer2 = 0.0f;
        timer = 0.0f;
        waitingTime = 0.2f;
        camera = MiniCam.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        timer3 += Time.deltaTime;
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
        if (timer3 > waitingTime)
        {
            timer3 = 0;
            isWait3 = false;
        }
        isEsc();
        isMap();
        isF1();
    }

    public void isEsc()
    {
        if (!isWait && isESC && Input.GetKey(KeyCode.Escape))
        {
            PlayerUI.SetActive(true);
            escUI.SetActive(false);
            isESC = false;
            isWait = true;
            timer = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = true;
            MainCharacter.GetComponent<PlayerMove>().enabled = true;
        }
        else if (!isWait && !isESC && Input.GetKey(KeyCode.Escape))
        {
            PlayerUI.SetActive(false);
            isESC = true;
            escUI.SetActive(true);
            isWait = true;
            timer = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = false;
            MainCharacter.GetComponent<PlayerMove>().enabled = false;

        }
    }
    public void isMap()
    {
        if (!isWait2 && isM && Input.GetKey(KeyCode.M))
        {
            PlayerUI.SetActive(true);
            CharPlace.SetActive(false);
            MiniMap.SetActive(false);
            camera.enabled = false;
            isM = false;
            isWait2 = true;
            timer2 = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = true;
            MainCharacter.GetComponent<PlayerMove>().enabled = true;
        }
        else if (!isWait2 && !isM && Input.GetKey(KeyCode.M))
        {
            PlayerUI.SetActive(false);
            CharPlace.SetActive(true);
            MiniMap.SetActive(true);
            MiniCam.transform.position = new Vector3(MainCharacter.transform.position.x, MiniCam.transform.position.y, MainCharacter.transform.position.z);
            camera.enabled = true;
            isM = true;
            isWait2 = true;
            timer2 = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = false;
            MainCharacter.GetComponent<PlayerMove>().enabled = false;

        }
    }
    public void isF1()
    {
        if (!isWait3 && isHelp && Input.GetKey(KeyCode.F1))
        {
            PlayerUI.SetActive(true);
            HelpUI.SetActive(false);
            isHelp = false;
            isWait3 = true;
            timer3 = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = true;
            MainCharacter.GetComponent<PlayerMove>().enabled = true;
        }
        else if (!isWait3 && !isHelp && Input.GetKey(KeyCode.F1))
        {
            PlayerUI.SetActive(false);
            isHelp = true;
            HelpUI.SetActive(true);
            isWait3 = true;
            timer3 = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = false;
            MainCharacter.GetComponent<PlayerMove>().enabled = false;

        }
    }

}