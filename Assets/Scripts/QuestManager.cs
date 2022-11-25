using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void QuestEnter(string questName, string[] questList, NPCTrigger npcTrigger)
    {
        isQuesting = true;
        PlayerMove.Instance.isQuesting = true;
    }

    public void QuestIng()
    {

    }
    
}
