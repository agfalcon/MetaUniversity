using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public NPCMove npcMove;
    public GameObject player;
    public GameObject parent;
    public GameObject questionMark;
    public GameObject exclaimMark;

    // NPC 이름 및 기본 대화
    public string npcName;
    public string[] npcTalk;

    // NPC 퀘스트 대화(한 배열에 한 퀘스트 씩, 첫 문장은 퀘스트 이름으로 쓸 것)
    public string[] npcQuestTalk;
    
    [HideInInspector]
    public int currentQuestIndex = 0;
    [HideInInspector]
    public int npcQuestIndex = 0;
    public Dictionary<int, string[]> npcQuestList;

    public string[] npcQuestDesc;
    public Dictionary<int, string[]> npcQuestDescList;

    bool isTriggerInPlayer = false;

    void Awake()
    {
        FirstSetting();
    }

    void Update()
    {
        if (isTriggerInPlayer)
        {
            RotationNPCtoPlayer();
            FirstTalkWithPlayer();
        }

    }

    void FirstSetting()
    {
        if(npcQuestTalk.Length != 0 && npcQuestDesc.Length != 0)
        {
            SetQuestList();
        }
    }

    void SetQuestList()
    {
        npcQuestList = new Dictionary<int, string[]>();
        npcQuestDescList = new Dictionary<int, string[]>();

        foreach (string q in npcQuestTalk)
        {
            string[] quest = q.Split("//");
            string[] desc = npcQuestDesc[npcQuestIndex].Split("//");
            
            npcQuestList.Add(npcQuestIndex, quest);
            npcQuestDescList.Add(npcQuestIndex, desc);
            npcQuestIndex++;
        }

        questionMark.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggerInPlayer = true;
            npcMove.isMoving = false;
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
        }
    }


    void RotationNPCtoPlayer()
    {
        Vector3 lookatVec = player.transform.position - parent.transform.position;
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, Quaternion.LookRotation(lookatVec), Time.deltaTime * npcMove.rotateSpeed);
        /*Vector3 test = new Vector3(player.transform.position.x, parent.transform.position.y, player.transform.position.z);
        parent.transform.LookAt(test);*/
    }

    void FirstTalkWithPlayer()
    {
        if (Input.GetKey(KeyCode.F) && !PlayerMove.Instance.isF && !TalkManager.Instance.isTalk)
        {
            TalkManager.Instance.FirstInteractWithPlayer(this);
            TalkManager.Instance.TalkOrQuest(0);
        }
    }

}
