using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenResolution : MonoBehaviour
{
    FullScreenMode screenMode;
    public GameObject resolutionDropDownObject;
    TMP_Dropdown resolutionDropDown;
    List<Resolution> resolutions = new List<Resolution>();
    int resolutionNum;

    public GameObject UIDataO;
    UIData uiData;
    // Start is called before the first frame update
    void Start()
    {
        uiData = UIDataO.GetComponent<UIData>();
        resolutionDropDown = resolutionDropDownObject.GetComponent<TMP_Dropdown>();
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitUI()
    {
        for(int i=0; i< Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        resolutionDropDown.options.Clear();

        int optionNum = 0;
        foreach(Resolution item in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.width + " X "  + item.height;
            resolutionDropDown.options.Add(option);
             
            if (item.width == Screen.width && item.height == Screen.height)
                resolutionDropDown.value = optionNum;
            optionNum++;
        }
        resolutionDropDown.RefreshShownValue();
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, uiData.screenMode);
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

}
