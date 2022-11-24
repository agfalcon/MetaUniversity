using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Vector3 dir;

    CharacterController cc;

    public int npcId;
    public float currentSpeed = 0f;
    public float walkSpeed = 1f;
    public float runSpeed = 1.5f;
    public float rotateSpeed = 5f;

    public Transform[] movePose;

    public bool isMoving = true;
    public bool isReverse = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();

        anim.SetBool("isWalk", true);
    }

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
        float distance;

        if(!isReverse)
        {
            distance = Vector3.Distance(transform.position, movePose[1].position);

            if(distance > 0.5f)
            {
                dir = movePose[1].position - transform.position;
                if (0.5 <= distance && distance <= 1)
                    isReverse = true;
            }
        }
        else
        {
            distance = Vector3.Distance(transform.position, movePose[0].position);

            if (distance > 0.5f)
            {
                dir = movePose[0].position - transform.position;
                if (0.5 <= distance && distance <= 1)
                    isReverse = false;
            }
        }

        if (isMoving)
        {
            if (isReverse)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
            else
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }

    }

    void NPCStop()
    {
        currentSpeed = 0f;
    }
}
