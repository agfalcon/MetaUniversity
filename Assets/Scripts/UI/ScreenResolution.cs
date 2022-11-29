using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolution : MonoBehaviour
{
    public GameObject resolutionDropDownObject;
    Dropdown resolutionDropDown;
    List<Resolution> resolutions = new List<Resolution>();
    // Start is called before the first frame update
    void Start()
    {
        resolutionDropDown = resolutionDropDownObject.GetComponent<Dropdown>();
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitUI()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropDown.options.Clear();

        foreach(Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + " X "  + item.height;
            resolutionDropDown.options.Add(option);
        }

        resolutionDropDown.RefreshShownValue();
    }
 
}
