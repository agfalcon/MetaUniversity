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
    public string[] npcTalk;
    string curNpcTalk;
    int npcTalkIndex = 0;
    bool isTriggerInPlayer = false;

    void Awake()
    {
    }

    void Update()
    {
        if (isTriggerInPlayer)
        {
            RotationNPCtoPlayer();
            FirstTalkWithPlayer();
            TalkSkipOrNext();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggerInPlayer = true;

            npcTalkIndex = 0;
            npcMove.isMoving = false;
            npcMove.anim.SetBool("isWalk", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggerInPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggerInPlayer = false;

            npcMove.isMoving = true;
            npcMove.anim.SetBool("isWalk", true);
        }
    }


    void RotationNPCtoPlayer()
    {
        Vector3 lookatVec = player.transform.position - parent.transform.position;
        parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, Quaternion.LookRotation(lookatVec), Time.deltaTime * npcMove.rotateSpeed);
    }

    void FirstTalkWithPlayer()
    {
        if (Input.GetKey(KeyCode.F) && !PlayerMove.Instance.isF && !TalkManager.Instance.isTalk)
        {
            PlayerMove.Instance.isF = true;
            TalkManager.Instance.NPCChatEnter(npcName);

            curNpcTalk = npcTalk[npcTalkIndex];
            TalkManager.Instance.NPCChat(curNpcTalk);
            npcTalkIndex++;
        }
    }

    void TalkSkipOrNext()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TalkManager.Instance.isCor) // 대화 스킵
        {
            TalkManager.Instance.QuickStopChat(curNpcTalk);
            print("Chat Skip");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerMove.Instance.isF && TalkManager.Instance.isTalk && !TalkManager.Instance.isCor) // NPC의 대화 한 문장이 출력 된 후 다음 문장으로 넘어가기 위함
        {
            if (npcTalkIndex >= npcTalk.Length)
            {
                print("Chat End");
                TalkManager.Instance.NPCChatExit();
                npcTalkIndex = 0;
                return;
            }
            print("next Chat");

            curNpcTalk = npcTalk[npcTalkIndex];
            TalkManager.Instance.NPCChat(curNpcTalk);
            npcTalkIndex++;
        }
    }

}
