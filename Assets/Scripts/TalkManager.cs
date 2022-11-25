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
        if (Input.GetKeyDown(KeyCode.Space) && isCor) // ��ȭ ��ŵ
        {
            QuickStopChat(curNpcTalk);
            print("Chat Skip");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerMove.Instance.isF && isTalk && !isCor) // NPC�� ��ȭ �� ������ ��� �� �� ���� �������� �Ѿ�� ����
        {
            if (npcTalkIndex >= npcTalk.Length) // ������ ��ȭ���
            {
               /* if (currentQuestIndex < npcQuestIndex && !PlayerMove.Instance.isQuesting) // ������ �� �ִ� ����Ʈ�� �����ϰ� ���� �÷��̾ �ٸ� ����Ʈ�� �ϰ� ���� ���� ���
                {
                    questName = npcQuestList[currentQuestIndex][0];
                    questBtn.SetActive(true);
                    questBtnText.text = questName;

                    QuestManager.Instance.QuestEnter(questName, npcQuestList[currentQuestIndex], this);
                }
                else // ����Ʈ�� ���ų� �ٸ� ����Ʈ�� �������� ���
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
