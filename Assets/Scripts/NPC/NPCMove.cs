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

    public bool moveStopAlways;
    public bool isMoving = true;
    public bool isReverse = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();

        if(isMoving && !moveStopAlways)
            anim.SetBool("isWalk", true);
        else if(movePose.Length > 0 && !moveStopAlways)
        {
            isMoving = true;
            anim.SetBool("isWalk", true);
            SetMovePoseY();
        }

    }

    void Update()
    {
        if (moveStopAlways)
            return;

        if(isMoving)
        {
            NPCMoveToMovePose();
            anim.SetBool("isWalk", true);
        }
        else
        {
            NPCStop();
            anim.SetBool("isWalk", false);
        }

        dir.Normalize();
        cc.Move(dir * Time.deltaTime * currentSpeed);
    }

    void SetMovePoseY()
    {
        foreach(var pos in movePose)
        {
            pos.position = new Vector3(pos.position.x, transform.position.y, pos.position.z);
        }
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
