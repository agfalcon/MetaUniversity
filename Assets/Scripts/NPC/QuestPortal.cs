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
            transform.gameObject.GetComponent<AudioSource>().Play();
            isTrigger = true;

        }
    }
}
