using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    AsyncOperation asyncLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InteractionManager.Instance.inTelePortArea = true;
            InteractionManager.Instance.SetInteractionImg();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F) && !PlayerMove.Instance.isF)
            {
                MySceneManager.Instance.LoadScene();
            }
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