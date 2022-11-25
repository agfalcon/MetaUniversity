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
    public GameObject questionMark;
    public GameObject exclaimMark;

    // NPC �̸� �� �⺻ ��ȭ
    public string npcName;
    public string[] npcTalk;

    // NPC ����Ʈ ��ȭ(�� �迭�� �� ����Ʈ ��, ù ������ ����Ʈ �̸����� �� ��)
    public string[] npcQuestTalk;
    
    [HideInInspector]
    public int currentQuestIndex = 0;
    [HideInInspector]
    public int npcQuestIndex = 0;
    public Dictionary<int, string[]> npcQuestList;

    bool isTriggerInPlayer = false;

    void Awake()
    {
        FirstSetting();
    }

    void Update()
    {
        if (isTriggerInPlayer)
        {
            RotationNPCtoPlayer();
            FirstTalkWithPlayer();
        }

    }

    void FirstSetting()
    {
        if(npcQuestTalk != null)
        {
            SetQuestList();
        }
    }

    void SetQuestList()
    {
        npcQuestList = new Dictionary<int, string[]>();

        foreach(string q in npcQuestTalk )
        {
            string[] quest = q.Split("//");
            npcQuestList.Add(npcQuestIndex, quest);
            npcQuestIndex++;
        }

        questionMark.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggerInPlayer = true;
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
            TalkManager.Instance.FirstInteractWithPlayer(this);
            TalkManager.Instance.TalkOrQuest(0);
        }
    }

    public void OnClickQuestBtn()
    {
        print("hi Im quest");
    }

}
