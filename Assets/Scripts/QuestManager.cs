using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// 퀘스트를 클리어하면 curNpc의 currentQuestIndex++를 해줘 해당 NPC가 다음 퀘스트를 플레이어에게 제공하도록 해야 함

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance = null;
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    public GameObject successImg;
    public GameObject curQuestList;
    public TMP_Text curQuestName_T;
    public TMP_Text curQuestDo_T;

    public bool isQuesting = false;
    [HideInInspector]
    public string curQuestName;
    [HideInInspector]
    public NPCTrigger curNpc; // 현재 진행중인 퀘스트의 NPCTrigger

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

    void SetOffAllNpcMark()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        for (int i = 0; i < npcs.Length; i++)
        {
            NPCTrigger nt = npcs[i].GetComponentInChildren<NPCTrigger>();
            nt.exclaimMark.SetActive(false);
            nt.questionMark.SetActive(false);
        }
    }

    void SetOnAllNpcMark()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        for (int i = 0; i < npcs.Length; i++)
        {
            NPCTrigger nt = npcs[i].GetComponentInChildren<NPCTrigger>();

            if(nt.npcQuestTalk.Length != 0 && nt.currentQuestIndex < nt.npcQuestIndex) // 퀘스트가 있고 아직 수행할 퀘스트가 있는 NPC일 경우 Mark On
            {
                nt.exclaimMark.SetActive(false);
                nt.questionMark.SetActive(true);
            }
        }
    }
    
    void QuestUIControll(bool flag)
    {
        if (flag)
            SetOffAllNpcMark();
        else
            SetOnAllNpcMark();

        curQuestList.SetActive(flag);
        curNpc.exclaimMark.SetActive(flag);
        
        curQuestName_T.text = "";
        curQuestDo_T.text = "";
    }

    void UpdateQuestList()
    {
        curQuestList.SetActive(true);
        curQuestName_T.text = curQuestName;
        foreach(var txt in curNpc.npcQuestDescList[curNpc.currentQuestIndex])
        {
            curQuestDo_T.text += txt + '\n';
        }
    }

    public void QuestEnter(NPCTrigger npc)
    {
        curNpc = npc;
        isQuesting = true;

        curQuestName = npc.npcQuestList[npc.currentQuestIndex][0];
        QuestUIControll(true);
        UpdateQuestList();
    }

    // 퀘스트 성공 시 해당 npc의 다음 퀘스트 진행을 위해 npc.currentQuestIndex를 증가 시켜줘야 함

    public void Questing()
    {
        switch (curNpc.npcMove.npcId)
        {
            case 1000:

                break;
            case 1001:
                NPC_1001_Quest();
                break;
            case 3000:
                NPC_3000_Quest();
                break;
            default:
                break;
        }
    }

    public void QuestExit()
    {
        curNpc.currentQuestIndex++;

        isQuesting = false;
        QuestUIControll(false);
    }

    void SuccessQuest()
    {
        PlayerMove.Instance.maxStat += 0.5f; // 퀘스트 성공 시 캐릭터 스태미너 0.5씩 증가

        successImg.SetActive(true);
        StartCoroutine("SuccessImgFadeOut");
    }

    IEnumerator SuccessImgFadeOut()
    {
        float time = 0f;
        Color color = successImg.GetComponent<Image>().color;

        while (time <= 8f)
        {
            time += Time.deltaTime;

            if(time < 1f) // fade In
            {
                color.a += Time.deltaTime / 1f;
                successImg.GetComponent<Image>().color = color;
            }
            else if(time > 7f) // fade Out
            {
                color.a -= Time.deltaTime / 1f;
                successImg.GetComponent<Image>().color = color;
            }

            yield return null;
        }

        successImg.SetActive(false);
    }

    void NPC_1001_Quest()
    {
        string[] talkList = new string[] { 
            "금오공과대학교(Kumoh National Institute of Technology)는 대한민국의 교육부 소속 4년제 국립공과대학교이다.",
            "1979년에 박정희 대통령의 지시로 박정희 대통령의 고향인 경북 구미시에 공학계열 중심의 금오공과대학교가 설립인가 되었다.",
            "1990년에 국립대학으로 개편되고 1993년에 현재의 이름으로 바뀌었다. 2004년 12월 30일에 교사 이전을 완료하고, 2005년부터 현재의 구미시 양호동 신캠퍼스를 쓰고 있다.",
            "영문 이니셜은 Kit이며, 약칭으로 ‘금오공대’라고 부른다.",
            "금오공대에 대한 설명은 여기까지야."};

        TalkManager.Instance.FirstInteractWithPlayer(curNpc);
        TalkManager.Instance.TalkOrQuest(talkList);

        StartCoroutine("NPC_1001_QuestCor");
    }

    IEnumerator NPC_1001_QuestCor()
    {
        while (true)
        {
            if (!TalkManager.Instance.isTalk)
                break;
            yield return null;
        }

        QuestExit();
        Invoke("SuccessQuest", 0.5f);
    }

    void NPC_3000_Quest()
    {
        string[] talkList = new string[] { };
    }

}
