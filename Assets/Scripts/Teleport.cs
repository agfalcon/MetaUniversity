using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    AsyncOperation asyncLoad;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.F) && !PlayerMove.Instance.isF)
            {
                UIInstance.Instance.LoadScene();
            }
        }
    }

}