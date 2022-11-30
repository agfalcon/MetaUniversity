using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIData : MonoBehaviour
{
    public bool isStart;
    public bool isSoundOn;
    public FullScreenMode screenMode;
    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        isSoundOn = true;
        screenMode = FullScreenMode.FullScreenWindow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
