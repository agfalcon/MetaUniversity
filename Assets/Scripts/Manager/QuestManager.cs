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

        Questing();
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
            case 2005:
                NPC_2005_Quest();
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
        PlayerMove.Instance.maxStat += 0.5f; // ����Ʈ ���� �� ĳ���� ���¹̳� 0.5�� ����

        successImg.SetActive(true);
        StartCoroutine(nameof(SuccessImgFadeOut));
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

    void QuestEndAndSuccess()
    {
        QuestExit();
        Invoke(nameof(SuccessQuest), 0.5f);
    }

    void NPC_1001_Quest()
    {
        string[] talkList = new string[] {
            "�ݿ��������б�(Kumoh National Institute of Technology)�� ���ѹα��� ������ �Ҽ� 4���� �����������б��̴�.",
            "1979�⿡ ������ ������� ���÷� ������ ������� ������ ��� ���̽ÿ� ���а迭 �߽��� �ݿ��������б��� �����ΰ� �Ǿ���.",
            "1990�⿡ ������������ ����ǰ� 1993�⿡ ������ �̸����� �ٲ����. 2004�� 12�� 30�Ͽ� ���� ������ �Ϸ��ϰ�, 2005����� ������ ���̽� ��ȣ�� ��ķ�۽��� ���� �ִ�.",
            "���� �̴ϼ��� Kit�̸�, ��Ī���� ���ݿ����롯��� �θ���.",
            "�ݿ����뿡 ���� ������ ���������."};

        TalkManager.Instance.FirstInteractWithPlayer(curNpc);
        TalkManager.Instance.TalkOrQuest(talkList);

        StartCoroutine(nameof(NPC_1001_QuestCor));
    }

    IEnumerator NPC_1001_QuestCor()
    {
        yield return new WaitUntil(() => TalkManager.Instance.isTalk == false);
        QuestEndAndSuccess();
    }

    void NPC_2005_Quest()
    {
        curQuestName_T.text = "�̿��� ã�ƺ���";
        GameObject parentPos = GameObject.Find("NPC_Move_Pose"); // Find()�� ��Ȱ��ȭ �Ǿ��ִ� ��ü�� ã�� ����
        GameObject portal = parentPos.transform.GetChild(0).gameObject;
        
        StartCoroutine(nameof(NPC_2005_QuestCor), portal);
    }

    IEnumerator NPC_2005_QuestCor(GameObject portal)
    {
        string[] text = { "��! �ȳ�!", "��! �������ַ� �°ž�?",
                          "���� ~ �� ������ �Ұ�!!", "�ݿ�����Ƽ���� ���� �Ϸ� ��������"};
        QuestPortal qp = portal.GetComponent<QuestPortal>();
        
        portal.SetActive(true);
        TalkManager.Instance.isTalk = true;

        Debug.Log("!isTrigger");
        while (true)
        {
            if (qp.isTrigger)
            {
                TalkManager.Instance.isTalk = false;
                TalkManager.Instance.TalkOrQuest(text);
                yield return new WaitUntil(() => qp.isTrigger == true && !qp.GetComponent<AudioSource>().isPlaying);
                portal.SetActive(false);
                break;
            }
            yield return null;
        }
        yield return new WaitUntil(() => TalkManager.Instance.isTalk == false);


        QuestEndAndSuccess();
    }

    void NPC_3000_Quest()
    {
        GameObject parentPos = GameObject.Find("QuestPosition"); // Find()�� ��Ȱ��ȭ �Ǿ��ִ� ��ü�� ã�� ����
        GameObject[] portal = new GameObject[5];
        portal[0] = parentPos.transform.GetChild(0).gameObject;
        portal[1] = parentPos.transform.GetChild(1).gameObject;
        portal[2] = parentPos.transform.GetChild(2).gameObject;
        portal[3] = parentPos.transform.GetChild(3).gameObject;
        portal[4] = parentPos.transform.GetChild(4).gameObject;

        StartCoroutine(nameof(NPC_3000_QuestCor), portal);
    }

    IEnumerator NPC_3000_QuestCor(GameObject[] portal)
    {
        int curIndex = 0;
        int portalLen = portal.Length;

        while (curIndex < portalLen)
        {
            QuestPortal qp = portal[curIndex].GetComponent<QuestPortal>();

            portal[curIndex].SetActive(true);
            yield return new WaitUntil(() => qp.isTrigger == true && !qp.GetComponent<AudioSource>().isPlaying); // qp�� isTrigger�� ���� true�� �ɶ����� ���
            portal[curIndex].SetActive(false);
            
            curIndex++;
        }

        QuestEndAndSuccess();
    }
}
