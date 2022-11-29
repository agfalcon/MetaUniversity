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
            NPCMove npc = npcs[i].GetComponent<NPCMove>();
            if(npc.npcId != curNpc.npcMove.npcId) // 현재 퀘스트의 NPC가 아니라면
            {
                NPCTrigger nt = npcs[i].GetComponentInChildren<NPCTrigger>();
                nt.exclaimMark.SetActive(false);
                nt.questionMark.SetActive(false);
            }
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

    public void QuestEnter(NPCTrigger npc)
    {
        curNpc = npc;
        isQuesting = true;
        curQuestList.SetActive(true);

        curQuestName = npc.npcQuestList[npc.currentQuestIndex][0]; 
        SetOffAllNpcMark();
        
        Debug.Log("현재 수락한 퀘스트 이름: " + curQuestName);
    }

    // 퀘스트 성공 시 해당 npc의 다음 퀘스트 진행을 위해 npc.currentQuestIndex를 증가 시켜줘야 함

    public void QuestExit()
    {
        isQuesting = false;
        curQuestList.SetActive(false);
        SetOnAllNpcMark();
    }

}
