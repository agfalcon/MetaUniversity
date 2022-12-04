using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public GameObject UIDataO;
    UIData uiData;

    void Awake()
    {
        uiData = UIDataO.GetComponent<UIData>();
    }


    // Update is called once per frame
    void Update()
    {
        if (uiData.isStart)
            transform.position = target.position;
    }
}
