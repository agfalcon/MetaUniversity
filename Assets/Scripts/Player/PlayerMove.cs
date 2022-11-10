using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    CharacterController cc;
    // 중력 변수
    float gravitiy = -20f;
    // 수직 속력 변수
    float yVelocity = 0f;
    // 점프력 변수
    public float jumpPower = 3f;
    bool isJump = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        // 메인 카메라를 기준으로 방향 변경
        dir = Camera.main.transform.TransformDirection(dir);

        if(isJump && cc.collisionFlags == CollisionFlags.CollidedBelow)
        {
            isJump = false;
        }

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            yVelocity = jumpPower;
            isJump = true;
        }


        yVelocity += gravitiy * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);
    }

}
