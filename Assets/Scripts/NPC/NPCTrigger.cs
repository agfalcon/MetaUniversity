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
    public GameObject ps;

    // NPC 이름 및 기본 대화
    public string[] npcName;
    public string[] npcTalk;

    // NPC 퀘스트 대화(questBox: 박스 목록에 들어갈 텍스트 입력, npcQuestTalk: 박스 선택 시 진행되는 대화 입력)
    public string[] questBox;
    public string[] npcQuestTalkName;
    public string[] npcQuestTalk;
    
    [HideInInspector]
    public int currentQuestIndex = 0;
    [HideInInspector]
    public int npcQuestIndex = 0;
    [HideInInspector]
    public int npcQuestTalkNameIndex = 0;

    public Dictionary<int, string[]> npcQuestList;
    public Dictionary<int, string[]> npcQuestTalkNameList;
    
    public bool isQuiz = false;
    public bool isQuestExist = true;
    [HideInInspector]
    public bool isTriggerInPlayer = false;

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
        if(npcQuestTalk.Length != 0)
        {
            SetQuestList();
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void SetQuestList()
    {
        npcQuestList = new Dictionary<int, string[]>();
        npcQuestTalkNameList = new Dictionary<int, string[]>();

        for (int i = 0; i < questBox.Length; i++)
        {
            string[] questTalk = npcQuestTalk[i].Split("//");

            npcQuestList.Add(npcQuestIndex, questTalk);
            npcQuestIndex++;
        }

        for (int i = 0; i < npcQuestTalkName.Length; i++)
        {
            string[] temp = npcQuestTalkName[i].Split("//");
            npcQuestTalkNameList.Add(npcQuestTalkNameIndex, temp);
            npcQuestTalkNameIndex++;
        }
        questionMark.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggerInPlayer = true;
            npcMove.isMoving = false;

            InteractionManager.Instance.inTalkArea = true;
            InteractionManager.Instance.SetInteractionImg();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggerInPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggerInPlayer = false;
            npcMove.isMoving = true;

            InteractionManager.Instance.inTalkArea = false;
            InteractionManager.Instance.SetInteractionImg();
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
        if (Input.GetKeyDown(KeyCode.F) && !PlayerMove.Instance.isF && !TalkManager.Instance.isTalk && npcTalk.Length != 0)
        {
            TalkManager.Instance.FirstInteractWithPlayer(this);
            TalkManager.Instance.TalkOrQuest(0);
        }
    }

}
