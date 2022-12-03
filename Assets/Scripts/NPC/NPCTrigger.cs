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

    // NPC 퀘스트 대화(한 배열에 한 퀘스트 씩, 첫 문장은 퀘스트 이름으로 쓸 것), npcQuestTalk와 npcQuestDesc는 Length(개수)가 같아야 함
    public string[] npcQuestTalk;
    public string[] npcQuestDesc;
    
    [HideInInspector]
    public int currentQuestIndex = 0;
    [HideInInspector]
    public int npcQuestIndex = 0;

    public Dictionary<int, string[]> npcQuestList;
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
        Vector3 to = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        Vector3 from = new Vector3(parent.transform.position.x, 0f, parent.transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(to - from);
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, rotation, Time.deltaTime * npcMove.rotateSpeed);
        
    }

    void FirstTalkWithPlayer()
    {
        if (Input.GetKeyDown(KeyCode.F) && !PlayerMove.Instance.isF && !TalkManager.Instance.isTalk)
        {
            TalkManager.Instance.FirstInteractWithPlayer(this);
            TalkManager.Instance.TalkOrQuest(0);
            /*if (QuestManager.Instance.isQuesting && QuestManager.Instance.curNpc == this) // 퀘스트를 수락중인 상태에서 해당 NPC와 대화하는 경우
            {
                QuestManager.Instance.Questing();
            }
            else
            {
                TalkManager.Instance.TalkOrQuest(0);
            }*/
        }
    }

}
