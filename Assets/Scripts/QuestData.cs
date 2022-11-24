using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questName; // 퀘스트 이름
    public int[] npcId; // 해당 퀘스트에 참여하는 npc의 순서를 npcID로 저장

    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
