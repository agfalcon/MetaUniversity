using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public GameObject curQuestList;
    public TMP_Text curQuestName_T;
    public TMP_Text curQuestDo_T;

    public bool isQuesting = false;
    [HideInInspector]
    public string curQuestName;
    NPCTrigger curNpc;

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

            if(nt.npcQuestTalk != null && nt.currentQuestIndex < nt.npcQuestIndex)
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

    void Questing()
    {
        switch (curNpc.npcMove.npcId)
        {
            case 1000:

                break;
            case 1001:
                curNpc.currentQuestIndex++;
                break;
            default:
                break;
        }
    }

    public void QuestExit()
    {
        isQuesting = false;
        QuestUIControll(false);
    }

}
