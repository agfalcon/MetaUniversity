using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.F) && !PlayerMove.Instance.isF)
            {
                switch (SceneManager.GetActiveScene().buildIndex)
                {
                    case 0:
                        SceneManager.LoadScene(1);
                        break;
                    case 1:
                        SceneManager.LoadScene(0);
                        break;

                }
            }
        }
    }
}
