using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestController : MonoBehaviour
{
    private static QuestController instance = null;
    public static QuestController Instance 
    { 
        get 
        {
            if (instance == null)
                return null;

            return instance;
        } 
    }

    public GameObject playerUI;
    public GameObject questUI;
    public TMP_Text questInfoText;
    public TMP_Text nameText;

    public bool isTalk = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {

    }

    public void NPCChatEnter(string npcName)
    {
        isTalk = true;

        playerUI.SetActive(false);
        questUI.SetActive(true);

        nameText.text = npcName;
        PlayerMove.Instance.SetMoveSpeed(0);

        Debug.Log("NPCChatEnter");
    }

    public void NPCChat(string questText)
    {

        StartCoroutine(QuestInfoRoutine(questText));
        Debug.Log("NPCChat");
    }

    public void NPCChatExit()
    {
        nameText.text = "";
        questInfoText.text = "";

        playerUI.SetActive(true);
        questUI.SetActive(false);

        PlayerMove.Instance.SetMoveSpeed(PlayerMove.Instance.walkSpeed);

        Debug.Log("NPCChatExit");
    }

    IEnumerator QuestInfoRoutine(string text)
    {
        questInfoText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            questInfoText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        isTalk = false;
        PlayerMove.Instance.isF = false;

        NPCChatExit();
    }
}
