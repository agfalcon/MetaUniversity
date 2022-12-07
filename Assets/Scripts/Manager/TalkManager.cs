using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    public GameObject skipImg;
    public GameObject questBtn;
    public GameObject questInteraction;
    public TMP_Text npcTalkText;
    public TMP_Text nameText;
    public TMP_Text questBtnText;
    public TMP_Text skipText;

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
    int currentQuestIndex = 0;
    Dictionary<int, string[]> npcQuestList;
    string questName;

    bool isQuestTalk = false;
    bool isQuestTalkLast = false;
    
    [HideInInspector]
    public NPCTrigger npc;

    void Awake()
    {
        if (instance == null)
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

        if (isQuestTalkLast) // ����Ʈ ��ư�� ���� ����Ʈ�� ���� ��ȭ ���� �� �� ��ȭ�� �������� ��
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                QuestTalkExit();
                QuestManager.Instance.QuestEnter(npc); // ������ ����Ʈ�� �����ϴ� npc�� NPCTrigger�� ���� 
            }
            else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Space))
            {
                QuestTalkExit();
            }
        }

        if (onQuestBtn) // ����Ʈ ��ư�� ���� ����
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                questBtn.SetActive(false);
                onQuestBtn = false;
                QuestTalkEnter();
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
        npcTalk_OR_Quest_Index = 0;
        UIOnOFF(isTalk);

        nameText.text = npcName;
        PlayerMove.Instance.SetMoveSpeed(0);
    }

    public void NPCChat(string npcTalk)
    {
        skipText.text = "Skip(Space bar)";
        dropDownImg.SetActive(false);
        StartCoroutine(nameof(TalkRoutine), npcTalk);
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

    IEnumerator TalkRoutine(string text)
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
            case 0: // npc �⺻ ��ȭ�� ���
                npcTalk_OR_Quest_List = npcTalk;
                curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                break;
            case 1: // npc ����Ʈ�� ���
                npcTalk_OR_Quest_List = npcQuestList[currentQuestIndex];
                questName = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                npcTalk_OR_Quest_Index++;
                curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                break;
        }

        NPCChat(curNpcTalk_OR_Quest_Text);
        npcTalk_OR_Quest_Index++;
    }

    public void TalkOrQuest(string[] talkList)
    {
        PlayerMove.Instance.isF = true;
        NPCChatEnter(npcName);

        npcTalk_OR_Quest_List = talkList;

        curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
        NPCChat(curNpcTalk_OR_Quest_Text);
        npcTalk_OR_Quest_Index++;
    }

    void TalkSkipOrNext()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCor) // ��ȭ ��ŵ
        {
            QuickStopChat(curNpcTalk_OR_Quest_Text);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerMove.Instance.isF && isTalk && !isCor) // NPC�� ��ȭ �� ������ ��� �� �� ���� �������� �Ѿ�� ����
        {
            if (npcTalk_OR_Quest_Index >= npcTalk_OR_Quest_List.Length) // ������ ��ȭ���
            {
                skipText.text = "��ȭ ����(Space bar)";

                if (currentQuestIndex < npcQuestIndex && !QuestManager.Instance.isQuesting && !isQuestTalk) // ������ �� �ִ� ����Ʈ�� �����ϰ� ���� �÷��̾ �ٸ� ����Ʈ�� �ϰ� ���� ���� ���
                {
                    questName = npcQuestList[currentQuestIndex][0];
                    questBtn.SetActive(true);
                    onQuestBtn = true;
                    questBtnText.text = questName + "(Y)";

                }
                else if (isQuestTalk) // ����Ʈ�� ���� �̾߱Ⱑ �������� ���
                {
                    isQuestTalkLast = true;
                    questInteraction.SetActive(true);
                }
                else // �⺻ NPC�� ��ȭ�� �������� ���
                {
                    NPCChatExit();
                }
                return;
            }

            curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
            NPCChat(curNpcTalk_OR_Quest_Text);
            npcTalk_OR_Quest_Index++;
        }
    }

    void QuestTalkEnter()
    {
        isQuestTalk = true;

        NPCChatExit();
        TalkOrQuest(1);
    }

    void QuestTalkExit()
    {
        isQuestTalk = false;
        isQuestTalkLast = false;
        questInteraction.SetActive(false);
        NPCChatExit();
    }

    public void QuickStopChat(string npcTalk)
    {
        if (isCor)
        {
            StopCoroutine(nameof(TalkRoutine));
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
