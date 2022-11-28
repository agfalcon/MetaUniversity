using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SounOnOFF : MonoBehaviour
{

    public GameObject MainCharacter; 
    AudioSource sound;
    public GameObject SoundToggle;
    Toggle toggle;
    private void Start()
    {
        sound = MainCharacter.GetComponent<AudioSource>();
        toggle = SoundToggle.GetComponent<Toggle>();
    }
    // Start is called before the first frame update
    void Update()
    {
            if (toggle.isOn)
            {
                sound.mute=false;
            }
            else if(!toggle.isOn)
            {
                sound.mute = true;
            }
    }
}
