using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TalkManager : MonoBehaviour
{
    private static TalkManager instance = null;
    public static TalkManager Instance 
    { 
        get 
        {
            if (instance == null)
                return null;

            return instance;
        } 
    }

    public GameObject playerUI;
    public GameObject questUI;
    public GameObject dropDownImg;
    public GameObject questBtn;
    public TMP_Text npcTalkText;
    public TMP_Text nameText;
    public TMP_Text questBtnText;

    public bool isTalk = false;
    [HideInInspector]
    public bool isCor = false;
    bool onQuestBtn = false;

    string npcName;
    string[] npcTalk;

    string[] npcTalk_OR_Quest_List;
    string curNpcTalk_OR_Quest_Text;
    int npcTalk_OR_Quest_Index = 0;

    int npcQuestIndex = 0;
    [HideInInspector]
    public int currentQuestIndex = 0;
    Dictionary<int, string[]> npcQuestList;
    string questName;

    NPCTrigger npc;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (!isTalk)
            return;

        if (onQuestBtn)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                questBtn.SetActive(false);
                onQuestBtn = false;
                QuestManager.Instance.QuestEnter();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                questBtn.SetActive(false);
                onQuestBtn = false;
                NPCChatExit();
            }
        }
        TalkSkipOrNext();
    }

    public void NPCChatEnter(string npcName)
    {
        isTalk = true;
        UIOnOFF(isTalk);

        nameText.text = npcName;
        PlayerMove.Instance.SetMoveSpeed(0);
    }

    public void NPCChat(string npcTalk)
    {
        dropDownImg.SetActive(false);
        StartCoroutine("QuestInfoRoutine", npcTalk);
    }

    public void NPCChatExit()
    {
        nameText.text = "";
        npcTalkText.text = "";
        npcTalk_OR_Quest_Index = 0;

        isTalk = false;
        PlayerMove.Instance.isF = false;
        PlayerMove.Instance.SetMoveSpeed(PlayerMove.Instance.walkSpeed);

        UIOnOFF(isTalk);
    }

    IEnumerator QuestInfoRoutine(string text)
    {
        npcTalkText.text = "";
        isCor = true;

        foreach (char letter in text.ToCharArray())
        {
            npcTalkText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        dropDownImg.SetActive(true);

        isCor = false;
    }

    public void FirstInteractWithPlayer(NPCTrigger npcTrigger)
    {
        npc = npcTrigger;
        FirstSetNpcInfo(npc);
        TalkOrQuest(0);
    }

    void FirstSetNpcInfo(NPCTrigger npcTrigger)
    {
        npc = npcTrigger;

        npcName = npc.npcName;
        npcTalk = npc.npcTalk;
        npcQuestList = npc.npcQuestList;
        npcQuestIndex = npc.npcQuestIndex;
        currentQuestIndex = npc.currentQuestIndex;

        npcTalk_OR_Quest_Index = 0;
    }

    public void TalkOrQuest(int index)
    {
        PlayerMove.Instance.isF = true;
        NPCChatEnter(npcName);

        switch (index)
        {
            case 0: // npc 기본 대화일 경우
                npcTalk_OR_Quest_List = npcTalk;
                curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                break;
            case 1: // npc 퀘스트일 경우
                npcTalk_OR_Quest_List = npcQuestList[currentQuestIndex];
                questName = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                npcTalk_OR_Quest_Index++;
                curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                break;
        }

        NPCChat(curNpcTalk_OR_Quest_Text);
        npcTalk_OR_Quest_Index++;
    }

    void TalkSkipOrNext()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCor) // 대화 스킵
        {
            QuickStopChat(curNpcTalk_OR_Quest_Text);
            print("Chat Skip");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerMove.Instance.isF && isTalk && !isCor) // NPC의 대화 한 문장이 출력 된 후 다음 문장으로 넘어가기 위함
        {
            if (npcTalk_OR_Quest_Index >= npcTalk_OR_Quest_List.Length) // 마지막 대화라면
            {
                if (currentQuestIndex < npcQuestIndex && !PlayerMove.Instance.isQuesting) // 수행할 수 있는 퀘스트가 존재하고 현재 플레이어가 다른 퀘스트를 하고 있지 않은 경우
                {
                    questName = npcQuestList[currentQuestIndex][0];
                    questBtn.SetActive(true);
                    onQuestBtn = true;
                    questBtnText.text = questName + "(Y)";

                    //QuestManager.Instance.QuestEnter();
                }
                else // 퀘스트가 없거나 다른 퀘스트를 수행중인 경우
                {
                    print("Chat End");
                    NPCChatExit();
                }
                return;
            }
            print("next Chat");

            curNpcTalk_OR_Quest_Text = npcTalk[npcTalk_OR_Quest_Index];
            NPCChat(curNpcTalk_OR_Quest_Text);
            npcTalk_OR_Quest_Index++;
        }
    }

    public void QuickStopChat(string npcTalk)
    {
        if(isCor)
        {
            print("QuickStopChat");

            StopCoroutine("QuestInfoRoutine");
            npcTalkText.text = npcTalk;
            dropDownImg.SetActive(true);
            isCor = false;
        }
    }

    void UIOnOFF(bool flag)
    {
        playerUI.SetActive(!flag);
        questUI.SetActive(flag);
    }
}
