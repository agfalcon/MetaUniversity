using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRotateX : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
    }
}
