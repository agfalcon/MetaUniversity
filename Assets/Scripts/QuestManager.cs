using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public GameObject successImg;
    public GameObject curQuestList;
    public TMP_Text curQuestName_T;
    public TMP_Text curQuestDo_T;

    public bool isQuesting = false;
    [HideInInspector]
    public string curQuestName;
    [HideInInspector]
    public NPCTrigger curNpc; // ���� �������� ����Ʈ�� NPCTrigger

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

            if(nt.npcQuestTalk.Length != 0 && nt.currentQuestIndex < nt.npcQuestIndex) // ����Ʈ�� �ְ� ���� ������ ����Ʈ�� �ִ� NPC�� ��� Mark On
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

    // ����Ʈ ���� �� �ش� npc�� ���� ����Ʈ ������ ���� npc.currentQuestIndex�� ���� ������� ��

    public void Questing()
    {
        switch (curNpc.npcMove.npcId)
        {
            case 1000:

                break;
            case 1001:
                NPC_1001_Quest();
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
        PlayerMove.Instance.maxStat += 0.5f; // ����Ʈ ���� �� ĳ���� ���¹̳� 0.5�� ����

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
            "�ݿ��������б�(Kumoh National Institute of Technology)�� ���ѹα��� ������ �Ҽ� 4���� �����������б��̴�.",
            "1979�⿡ ������ ������� ���÷� ������ ������� ������ ��� ���̽ÿ� ���а迭 �߽��� �ݿ��������б��� �����ΰ� �Ǿ���.",
            "1990�⿡ ������������ ����ǰ� 1993�⿡ ������ �̸����� �ٲ����. 2004�� 12�� 30�Ͽ� ���� ������ �Ϸ��ϰ�, 2005����� ������ ���̽� ��ȣ�� ��ķ�۽��� ���� �ִ�.",
            "���� �̴ϼ��� Kit�̸�, ��Ī���� ���ݿ����롯��� �θ���."};

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

}
