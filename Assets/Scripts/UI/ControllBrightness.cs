using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllBrightness : MonoBehaviour
{
    public GameObject Scroll;
    public GameObject light;
    Slider slider;
    Light mainLight;
    // Start is called before the first frame update
    void Start()
    {
        slider = Scroll.GetComponent<Slider>();
        mainLight = light.GetComponent<Light>();
        slider.value = mainLight.intensity / 2;
    }

    // Update is called once per frame
    void Update()
    {
        mainLight.intensity = slider.value * 2;
    }
}
