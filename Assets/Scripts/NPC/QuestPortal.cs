using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPortal : MonoBehaviour
{
    [HideInInspector]
    public bool isTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
            transform.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
