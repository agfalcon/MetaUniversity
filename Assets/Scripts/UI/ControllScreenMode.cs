using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllScreenMode : MonoBehaviour
{
    public GameObject ScreenModeDropDown;
    Dropdown Dropdown;

    // Start is called before the first frame update
    void Start()
    {
        Dropdown = ScreenModeDropDown.GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetMode()
    {

    }
}
