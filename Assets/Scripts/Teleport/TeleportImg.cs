using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportImg : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InteractionManager.Instance.inTelePortArea = true;
            InteractionManager.Instance.SetInteractionImg();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InteractionManager.Instance.inTelePortArea = false;
            InteractionManager.Instance.SetInteractionImg();
        }
    }
}
