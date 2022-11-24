using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId; // 퀘스트 id
    public int questActionIndex; // 현재 진행중인 퀘스트 인덱스를 저장
    public GameObject[] questObject; // 퀘스트에 필요한 오브젝트 저장

    Dictionary<int, QuestData> questList; // 퀘스트 내용을 저장

    void Start()
    {
        questList = new Dictionary<int, QuestData>();
        GeneratedData();
    }

    void GeneratedData()
    {
        questList.Add(10, new QuestData("김동하와 대화하기", new int[] { 1000}));
    }

    void NextQuest() // 퀘스트를 성공하면 questId를 10씩 증가시키고 questActionIndex를 0으로 초기화 해 다음 퀘스트를 진행할 수 있게 한다
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject() // 퀘스트에 사용되는 오브젝트 관리 함수
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

    public string CheckQuest(int id) // id와 npc 플레이어가 대화하는 순서가 맞다면 questActionIndex를 1씩 증가, 대화하는 순서가 마지막이라면 다음 퀘스트 진행
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
