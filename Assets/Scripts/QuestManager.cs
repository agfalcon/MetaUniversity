using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId; // ����Ʈ id
    public int questActionIndex; // ���� �������� ����Ʈ �ε����� ����
    public GameObject[] questObject; // ����Ʈ�� �ʿ��� ������Ʈ ����

    Dictionary<int, QuestData> questList; // ����Ʈ ������ ����

    void Start()
    {
        questList = new Dictionary<int, QuestData>();
        GeneratedData();
    }

    void GeneratedData()
    {
        questList.Add(10, new QuestData("�赿�Ͽ� ��ȭ�ϱ�", new int[] { 1000}));
    }

    void NextQuest() // ����Ʈ�� �����ϸ� questId�� 10�� ������Ű�� questActionIndex�� 0���� �ʱ�ȭ �� ���� ����Ʈ�� ������ �� �ְ� �Ѵ�
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject() // ����Ʈ�� ���Ǵ� ������Ʈ ���� �Լ�
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id) // id�� npc �÷��̾ ��ȭ�ϴ� ������ �´ٸ� questActionIndex�� 1�� ����, ��ȭ�ϴ� ������ �������̶�� ���� ����Ʈ ����
    {
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        ControlObject();

        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
            Debug.Log(questId);
        }

        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        return questList[questId].questName;
    }
}
