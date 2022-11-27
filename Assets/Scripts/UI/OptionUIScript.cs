using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUIScript : MonoBehaviour
{
    public GameObject ESCUI;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ESCUI.SetActive(false);
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            ESCUI.SetActive(true);
        }
    }
}
