using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

    CharacterController cc;
    Vector3 destination;

    public float currentSpeed = 0f;
    public float walkSpeed = 0.1f;
    public float runSpeed = 0.07f;

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

        cc.Move(destination * Time.deltaTime * currentSpeed);
    }

    void NPCMoveToMovePose()
    {
        currentSpeed = walkSpeed;

        if (Vector3.Distance(transform.position, movePose[1].position) > 0.5f && !isReverse)
        {
            destination = movePose[1].position - transform.position;
        }
        else if(Vector3.Distance(transform.position, movePose[0].position) > 0.5f && isReverse)
        {
            destination = movePose[0].position - transform.position;
        }

    }

    void NPCStop()
    {
        currentSpeed = 0f;
    }
}
