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

    string npcName;
    string[] npcTalk;

    string curNpcTalk;
    int npcTalkIndex = 0;

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
        if (isTalk)
        {
            TalkSkipOrNext();
        }
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

    public void FirstTalkWithPlayer(string npcName_, string[] npcTalk_)
    {
        npcName = npcName_;
        npcTalk = npcTalk_;
        PlayerMove.Instance.isF = true;
        NPCChatEnter(npcName);

        curNpcTalk = npcTalk[npcTalkIndex];
        NPCChat(curNpcTalk);
        npcTalkIndex++;
    }

    void TalkSkipOrNext()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCor) // 대화 스킵
        {
            QuickStopChat(curNpcTalk);
            print("Chat Skip");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerMove.Instance.isF && isTalk && !isCor) // NPC의 대화 한 문장이 출력 된 후 다음 문장으로 넘어가기 위함
        {
            if (npcTalkIndex >= npcTalk.Length) // 마지막 대화라면
            {
               /* if (currentQuestIndex < npcQuestIndex && !PlayerMove.Instance.isQuesting) // 수행할 수 있는 퀘스트가 존재하고 현재 플레이어가 다른 퀘스트를 하고 있지 않은 경우
                {
                    questName = npcQuestList[currentQuestIndex][0];
                    questBtn.SetActive(true);
                    questBtnText.text = questName;

                    QuestManager.Instance.QuestEnter(questName, npcQuestList[currentQuestIndex], this);
                }
                else // 퀘스트가 없거나 다른 퀘스트를 수행중인 경우
                {
                    print("Chat End");
                    NPCChatExit();
                    npcTalkIndex = 0;
                }*/
                return;
            }
            print("next Chat");

            curNpcTalk = npcTalk[npcTalkIndex];
            NPCChat(curNpcTalk);
            npcTalkIndex++;
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
