using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool isQuesting = false;
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
        SetOffAllNpcMark();
    }

    public void QuestExit()
    {
        isQuesting = false;
        SetOnAllNpcMark();
    }

}
