using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ����Ʈ�� Ŭ�����ϸ� curNpc�� currentQuestIndex++�� ���� �ش� NPC�� ���� ����Ʈ�� �÷��̾�� �����ϵ��� �ؾ� ��

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
            if(npc.npcId != curNpc.npcMove.npcId) // ���� ����Ʈ�� NPC�� �ƴ϶��
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
        
        Debug.Log("���� ������ ����Ʈ �̸�: " + curQuestName);
    }

    // ����Ʈ ���� �� �ش� npc�� ���� ����Ʈ ������ ���� npc.currentQuestIndex�� ���� ������� ��

    public void QuestExit()
    {
        isQuesting = false;
        curQuestList.SetActive(false);
        SetOnAllNpcMark();
    }

}
