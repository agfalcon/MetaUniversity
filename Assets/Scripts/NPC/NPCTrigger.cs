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

    // NPC 이름 및 기본 대화
    public string npcName;
    public string[] npcTalk;

    public GameObject questBtn;
    public TMP_Text questBtnText;
    // NPC 퀘스트 대화(한 배열에 한 퀘스트 씩, 첫 문장은 퀘스트 이름으로 쓸 것)
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
        if (Input.GetKeyDown(KeyCode.Space) && TalkManager.Instance.isCor) // 대화 스킵
        {
            TalkManager.Instance.QuickStopChat(curNpcTalk);
            print("Chat Skip");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerMove.Instance.isF && TalkManager.Instance.isTalk && !TalkManager.Instance.isCor) // NPC의 대화 한 문장이 출력 된 후 다음 문장으로 넘어가기 위함
        {
            if (npcTalkIndex >= npcTalk.Length) // 마지막 대화라면
            {
                if(currentQuestIndex < npcQuestIndex && !PlayerMove.Instance.isQuesting) // 수행할 수 있는 퀘스트가 존재하고 현재 플레이어가 다른 퀘스트를 하고 있지 않은 경우
                {
                    questName = npcQuestList[currentQuestIndex][0];
                    questBtn.SetActive(true);
                    questBtnText.text = questName;

                    QuestManager.Instance.QuestEnter(questName, npcQuestList[currentQuestIndex], this);
                }
                else // 퀘스트가 없거나 다른 퀘스트를 수행중인 경우
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
