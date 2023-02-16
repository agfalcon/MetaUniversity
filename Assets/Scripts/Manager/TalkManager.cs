using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Image talkBox;
    public GameObject userOpinion;
    public TMP_Text userOpinionText;
    public GameObject[] questBtn;
    public TMP_Text npcTalkText;
    public TMP_Text nameText;
    public TMP_Text[] questBtnText;
    public TMP_Text skipText;

    public bool isTalk = false;
    [HideInInspector]
    public bool isCor = false;
    bool onQuestBtn = false;

    int npcNameIndex = 0;
    string[] npcName;
    string[] npcTalk;

    string[] curNpcName_List;
    string[] npcTalk_OR_Quest_List;
    string curNpcTalk_OR_Quest_Text;
    int npcTalk_OR_Quest_Index = 0;

    int npcQuestIndex = 0;
    int npcQuestTalkNameIndex = 0;
    int currentQuestIndex = 0;
    Dictionary<int, string[]> npcQuestList;
    Dictionary<int, string[]> npcQuestTalkNameList;
    string[] questBox; // 퀘스트 박스 목록에 들어갈 텍스트 리스트

    bool isQuestTalk = false;
    bool isQuestTalkLast = false;

    float score;
    
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

        if (isQuestTalkLast) // 퀘스트 버튼을 눌러 퀘스트에 대한 대화 진행 후 그 대화가 마지막일 때, 퀘스트 진행 중 o
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                QuestTalkExit();
                QuestManager.Instance.QuestEnter(npc); // 수락한 퀘스트를 제공하는 npc의 NPCTrigger를 전달 
            }
            else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Space))
            {
                QuestTalkExit();
            }
        }

        if (onQuestBtn) // 퀘스트 버튼이 나온 상태, 퀘스트 진행 중 x
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                QuestBtnActive(false);
                onQuestBtn = false;
                QuestTalkEnter(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                QuestBtnActive(false);
                onQuestBtn = false;
                QuestTalkEnter(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                QuestBtnActive(false);
                onQuestBtn = false;
                QuestTalkEnter(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                QuestBtnActive(false);
                onQuestBtn = false;
                //InputTextOut(QuestTalkEnter);
                userOpinion.SetActive(true);
                MouseSetONOFF(true);
                //QuestTalkEnter(4);
            }
        }
        TalkSkipOrNext();
    }

    void MouseSetONOFF(bool flag) // true이면 커서 on, false이면 off
    {
        Cursor.visible = flag;
        if (flag)
        {
            Camera.main.GetComponent<CamRotate>().enabled = false;
            PlayerMove.Instance.GetComponent<PlayerRotate>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else 
        {
            Camera.main.GetComponent<CamRotate>().enabled = true;
            PlayerMove.Instance.GetComponent<PlayerRotate>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    delegate void MyDelegate(int index);

    void InputTextOut(MyDelegate QuestTalkEnter)
    {
        QuestTalkEnter(4);
    }

    public void FirstInteractWithPlayer(NPCTrigger npcTrigger)
    {
        npc = npcTrigger;

        npcName = npc.npcName;
        npcTalk = npc.npcTalk;
        questBox = npc.questBox;
        npcQuestList = npc.npcQuestList;
        npcQuestIndex = npc.npcQuestIndex;
        npcQuestTalkNameIndex = npc.npcQuestTalkNameIndex;
        currentQuestIndex = npc.currentQuestIndex;
        npcQuestTalkNameList = npc.npcQuestTalkNameList;
        npcTalk_OR_Quest_Index = 0;
    }

    void QuestBtnActive(bool active)
    {
        for(int i = 0; i<questBtn.Length; i++)
        {
            questBtn[i].SetActive(active);
        }

        Debug.Log($"QuestBtnActive {active}");
    }

    void QuestBtnInputText()
    {
        for (int i = 0; i < questBtnText.Length; i++)
        {
            questBtnText[i].text = $"({i + 1}) " + questBox[i];
        }
        Debug.Log("QuestBtnInputText End");
    }

    void IsPlayerName(string name)
    {
        Color color = talkBox.color;
        if (name == "Player")
        {
            nameText.text = PlayerMove.Instance.playerName;
            color = new Color32(233, 251, 233, 255);
        }
        else
        {
            nameText.text = name;
            color = new Color32(255, 241, 205, 255);
        }
        talkBox.color = color;
    }
    public void NPCChatEnter(string npcName)
    {
        isTalk = true;
        npcTalk_OR_Quest_Index = 0;
        npcNameIndex = 0;
        UIOnOFF(isTalk);

        IsPlayerName(npcName);
        PlayerMove.Instance.SetMoveSpeed(0);
    }

    public void NPCChat(string npcTalk)
    {
        skipText.text = "Skip(Space bar)";
        IsPlayerName(curNpcName_List[npcNameIndex]);
        npcNameIndex++;
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

    void SetNameList(int index)
    {
        if (index == 0)
        {
            curNpcName_List = npcName;
        }
        else
        {
            curNpcName_List = npcQuestTalkNameList[index - 1];
        }
    }

    public void TalkOrQuest(int index)
    {
        PlayerMove.Instance.isF = true;

        SetNameList(index);
        NPCChatEnter(curNpcName_List[0]);

        switch (index)
        {
            case 0: // npc 기본 대화일 경우
                curNpcName_List = npcName;
                npcTalk_OR_Quest_List = npcTalk;
                curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                break;
            case 1: // 1번 박스 눌렀을 때
                npcTalk_OR_Quest_List = npcQuestList[0];
                curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                break;
            case 2: // 2번 박스 눌렀을 때
                npcTalk_OR_Quest_List = npcQuestList[1];
                curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                break;
            case 3: // 3번 박스 눌렀을 때
                npcTalk_OR_Quest_List = npcQuestList[2];
                curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                break;
            case 4: // 4번 박스 눌렀을 때
                npcTalk_OR_Quest_List = npcQuestList[3];
                curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
                break;
        }

        if(index != 0) SetScore();
        NPCChat(curNpcTalk_OR_Quest_Text);
        npcTalk_OR_Quest_Index++;
    }

    void SetScore()
    {
        if (npcTalk_OR_Quest_List[npcTalk_OR_Quest_List.Length - 1] == "O")
        {
            score = 1f;
            PlayerMove.Instance.SetHeartFill(score);
        }
        else if (npcTalk_OR_Quest_List[npcTalk_OR_Quest_List.Length - 1] == "OO")
        {
            score = 1.5f;
            PlayerMove.Instance.SetHeartFill(score);
        }
        else
        {
            score = 0f;
        }
    }

    public void TalkOrQuest(string[] talkList)
    {
        PlayerMove.Instance.isF = true;
        NPCChatEnter(npcName[0]);

        npcTalk_OR_Quest_List = talkList;

        curNpcTalk_OR_Quest_Text = npcTalk_OR_Quest_List[npcTalk_OR_Quest_Index];
        NPCChat(curNpcTalk_OR_Quest_Text);
        npcTalk_OR_Quest_Index++;
    }

    void TalkSkipOrNext()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCor) // 대화 스킵
        {
            QuickStopChat(curNpcTalk_OR_Quest_Text);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerMove.Instance.isF && isTalk && !isCor) // NPC의 대화 한 문장이 출력 된 후 다음 문장으로 넘어가기 위함
        {
            if (npcTalk_OR_Quest_Index >= npcTalk_OR_Quest_List.Length - 1) // 마지막 대화라면
            {
                skipText.text = "";

                if (currentQuestIndex < npcQuestIndex && !QuestManager.Instance.isQuesting && !isQuestTalk) // 수행할 수 있는 퀘스트가 존재하고 현재 플레이어가 다른 퀘스트를 하고 있지 않은 경우
                {
                    npcTalkText.text = "";
                    nameText.text = PlayerMove.Instance.playerName;
                    QuestBtnInputText();
                    QuestBtnActive(true);
                    onQuestBtn = true;
                }
                else if (isQuestTalk) // 퀘스트에 대한 이야기가 마지막인 경우
                {
                    skipText.text = "대화종료(Space bar)";
                    isQuestTalkLast = true;
                    npc.questionMark.SetActive(false);
                }
                else // 기본 NPC의 대화가 마지막인 경우
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

    void QuestTalkEnter(int index)
    {
        isQuestTalk = true;

        NPCChatExit();
        TalkOrQuest(index);
    }

    void QuestTalkExit()
    {
        isQuestTalk = false;
        isQuestTalkLast = false;
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

    public void ClickedSubmitButton()
    {
        userOpinion.SetActive(false);
        userOpinionText.text = "";
        MouseSetONOFF(false);
        QuestTalkEnter(4);
    }
}
