using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeController : MonoBehaviour
{
    public GameObject Scroll;
    public GameObject Sound;
    Slider slider;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        slider = Scroll.GetComponent<Slider>();
        audio = Sound.GetComponent<AudioSource>();
        slider.value = audio.volume;
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = slider.value;
    }
}
