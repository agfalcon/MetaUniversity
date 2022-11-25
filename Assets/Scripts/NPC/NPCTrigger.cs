using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NPCTrigger : MonoBehaviour
{
    public NPCMove npcMove;
    public GameObject player;
    public GameObject parent;

    // NPC 이름 및 기본 대화
    public string npcName;
    public string[] npcTalk;

    public GameObject questBtn;
    public TMP_Text questBtnText;
    // NPC 퀘스트 대화(한 배열에 한 퀘스트 씩, 첫 문장은 퀘스트 이름으로 쓸 것)
    public string[] npcQuestTalk;
    
    [HideInInspector]
    public int currentQuestIndex = 0;
    [HideInInspector]
    public int npcQuestIndex = 0;
    public Dictionary<int, string[]> npcQuestList;
    string questName;

    string curNpcTalk;
    int npcTalkIndex = 0;
    bool isTriggerInPlayer = false;

    void Awake()
    {
        SetQuestList();
    }

    void Update()
    {
        if (isTriggerInPlayer)
        {
            RotationNPCtoPlayer();
            FirstTalkWithPlayer();
        }
    }

    void SetQuestList()
    {
        npcQuestList = new Dictionary<int, string[]>();

        foreach(string q in npcQuestTalk )
        {
            string[] quest = q.Split("//");
            npcQuestList.Add(npcQuestIndex, quest);

            npcQuestIndex++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggerInPlayer = true;

            npcTalkIndex = 0;
            npcMove.isMoving = false;
            npcMove.anim.SetBool("isWalk", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggerInPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggerInPlayer = false;

            npcMove.isMoving = true;
            npcMove.anim.SetBool("isWalk", true);
        }
    }


    void RotationNPCtoPlayer()
    {
        Vector3 lookatVec = player.transform.position - parent.transform.position;
        parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, Quaternion.LookRotation(lookatVec), Time.deltaTime * npcMove.rotateSpeed);
    }

    void FirstTalkWithPlayer()
    {
        if (Input.GetKey(KeyCode.F) && !PlayerMove.Instance.isF && !TalkManager.Instance.isTalk)
        {
            TalkManager.Instance.FirstInteractWithPlayer(this);
        }
    }

    public void OnClickQuestBtn()
    {
        print("hi Im quest");
    }

}
