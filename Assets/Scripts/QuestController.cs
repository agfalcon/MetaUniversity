using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public GameObject playerUI;
    public GameObject questUI;
    public GameObject player;
    public GameObject parent;
    float rotateSpeed = 5f;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 lookatVec = player.transform.position - parent.transform.position;
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, Quaternion.LookRotation(lookatVec), Time.deltaTime *rotateSpeed);
            // parent.transform.LookAt(other.gameObject.transform.position);
        }
    }

}
