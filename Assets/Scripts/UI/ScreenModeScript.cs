using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenModeScript : MonoBehaviour
{
    public GameObject UIDataO;
    UIData uiData;
    public GameObject screen;
    TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        uiData = UIDataO.GetComponent<UIData>();
        dropdown = screen.GetComponent<TMP_Dropdown>();
    }

    // Update is called once per frame
    public void isChanged()
    {
        if(dropdown.value == 0)
        {
            uiData.screenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        if(dropdown.value == 1)
        {
            uiData.screenMode = FullScreenMode.Windowed;
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
 