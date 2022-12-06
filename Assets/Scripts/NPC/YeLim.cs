using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeLim : MonoBehaviour
{
    public NPCTrigger myTrigger;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (TalkManager.Instance.isTalk && TalkManager.Instance.npc == myTrigger)
        {
            anim.SetBool("isTalk", TalkManager.Instance.isTalk);
        }
        else if (!TalkManager.Instance.isTalk && TalkManager.Instance.npc == myTrigger)
        {
            anim.SetBool("isTalk", TalkManager.Instance.isTalk);
        }

    }
}

