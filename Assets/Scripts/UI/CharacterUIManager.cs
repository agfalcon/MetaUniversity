using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    public GameObject escUI;
    public bool isESC = false;
    float timer;
    float waitingTime;
    bool isWait = false;
    public GameObject MainCharacter;
    public GameObject MainCam;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        waitingTime = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            //Action
            timer = 0;
            isWait = false;
        }
        isEsc();
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
        else if(!isWait && !isESC && Input.GetKey(KeyCode.Escape))
        {
            isESC = true;
            escUI.SetActive(true);
            isWait = true;
            timer = 0.0f;
            MainCam.GetComponent<CamRotate>().enabled = false;
            
        }
    }

}
