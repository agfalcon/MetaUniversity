using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestController : MonoBehaviour
{
    public GameObject playerUI;
    public GameObject questUI;
    
    public GameObject player;
    public GameObject parent;

    public TMP_Text questInfoText;
    public TMP_Text nameText;
    string questText;

    public UnityEvent onPlayerEntered;

    bool isF = false;

    float rotateSpeed = 5f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 lookatVec = player.transform.position - parent.transform.position;
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, Quaternion.LookRotation(lookatVec), Time.deltaTime * rotateSpeed);
        }

        if (Input.GetKey(KeyCode.F) && !isF)
        {
            isF = true;

            playerUI.SetActive(false);
            questUI.SetActive(true);

            nameText.text = "±èµ¿ÇÏ";
            questInfoText.text = "";
            
            questText = "³ª´Â NPC ±èµ¿ÇÏ´Ù. ½Ã¹ß»õ³¢µé¾Æ";

            onPlayerEntered.Invoke();
        }
    }


    public void StartQuestInfoRoutine()
    {
        StartCoroutine(QuestInfoRoutine(questText));
    }

    IEnumerator QuestInfoRoutine(string text)
    {
        foreach(char letter in text.ToCharArray())
        {
            questInfoText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        PlayerMove.Instance.SetMoveSpeed(PlayerMove.Instance.walkSpeed);

        isF = false;
        questInfoText.text = "";
    }

}
