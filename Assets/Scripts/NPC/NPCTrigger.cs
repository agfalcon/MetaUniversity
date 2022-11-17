using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NPCTrigger : MonoBehaviour
{
    public NPCMove npcMove;
    public GameObject player;
    public GameObject parent;

    public string npcName;
    public string questText;

    public float rotateSpeed = 5f;

    Vector3 npcFirstDir;

    void Awake()
    {
        npcFirstDir = npcMove.movePose[1].position - npcMove.movePose[0].position;
    }

    void Update()
    {
        PlayerExitTrigger();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            npcMove.isMoving = false;
            npcMove.anim.SetBool("isWalk", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 lookatVec = player.transform.position - parent.transform.position;
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, Quaternion.LookRotation(lookatVec), Time.deltaTime * rotateSpeed);
        }

        if (Input.GetKey(KeyCode.F) && !PlayerMove.Instance.isF && !QuestController.Instance.isTalk)
        {
            PlayerMove.Instance.isF = true;

            QuestController.Instance.NPCChatEnter(npcName);
            QuestController.Instance.NPCChat(questText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            npcMove.isMoving = true;
            npcMove.anim.SetBool("isWalk", true);
        }
    }

    void PlayerExitTrigger()
    {
        if(npcMove.isMoving)
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, Quaternion.LookRotation(npcFirstDir), Time.deltaTime * rotateSpeed);
    }

}
