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

    // NPC �̸� �� �⺻ ��ȭ
    public string npcName;
    public string[] npcTalk;

    public GameObject questBtn;
    public TMP_Text questBtnText;
    // NPC ����Ʈ ��ȭ(�� �迭�� �� ����Ʈ ��, ù ������ ����Ʈ �̸����� �� ��)
    public string[] npcQuestTalk;
    int npcQuestIndex = 0;
    
    [HideInInspector]
    public int currentQuestIndex = 0;

    Dictionary<int, string[]> npcQuestList;
    string questName;

    string curNpcTalk;
    int npcTalkIndex = 0;
    bool isTriggerInPlayer = false;

    void Awake()
    {
        SetQuestList();
    }

    void Update()
    {
        if (isTriggerInPlayer)
        {
            RotationNPCtoPlayer();
            FirstTalkWithPlayer();
            //TalkSkipOrNext();
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
            TalkManager.Instance.FirstTalkWithPlayer(npcName, npcTalk);
        }
    }

    void TalkSkipOrNext()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TalkManager.Instance.isCor) // ��ȭ ��ŵ
        {
            TalkManager.Instance.QuickStopChat(curNpcTalk);
            print("Chat Skip");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerMove.Instance.isF && TalkManager.Instance.isTalk && !TalkManager.Instance.isCor) // NPC�� ��ȭ �� ������ ��� �� �� ���� �������� �Ѿ�� ����
        {
            if (npcTalkIndex >= npcTalk.Length) // ������ ��ȭ���
            {
                if(currentQuestIndex < npcQuestIndex && !PlayerMove.Instance.isQuesting) // ������ �� �ִ� ����Ʈ�� �����ϰ� ���� �÷��̾ �ٸ� ����Ʈ�� �ϰ� ���� ���� ���
                {
                    questName = npcQuestList[currentQuestIndex][0];
                    questBtn.SetActive(true);
                    questBtnText.text = questName;

                    QuestManager.Instance.QuestEnter(questName, npcQuestList[currentQuestIndex], this);
                }
                else // ����Ʈ�� ���ų� �ٸ� ����Ʈ�� �������� ���
                {
                    print("Chat End");
                    TalkManager.Instance.NPCChatExit();
                    npcTalkIndex = 0;
                }
                return;
            }
            print("next Chat");

            curNpcTalk = npcTalk[npcTalkIndex];
            TalkManager.Instance.NPCChat(curNpcTalk);
            npcTalkIndex++;
        }
    }

    public void OnClickQuestBtn()
    {
        print("hi Im quest");
    }

}
