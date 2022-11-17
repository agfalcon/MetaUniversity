using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

    CharacterController cc;
    Vector3 dir;

    public float currentSpeed = 0f;
    public float walkSpeed = 1f;
    public float runSpeed = 1.5f;

    public Transform[] movePose;

    public bool isMoving = true;
    bool isReverse = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();

        anim.SetBool("isWalk", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
            NPCMoveToMovePose();
        else
            NPCStop();

        dir.Normalize();
        cc.Move(dir * Time.deltaTime * currentSpeed);
    }

    void NPCMoveToMovePose()
    {
        currentSpeed = walkSpeed;

        if (Vector3.Distance(transform.position, movePose[1].position) > 0.5f && !isReverse)
        {
            dir = movePose[1].position - transform.position;
        }
        else if (Vector3.Distance(transform.position, movePose[0].position) > 0.5f && isReverse)
        {
            dir = movePose[0].position - transform.position;
        }

    }

    void NPCStop()
    {
        currentSpeed = 0f;
    }
}
