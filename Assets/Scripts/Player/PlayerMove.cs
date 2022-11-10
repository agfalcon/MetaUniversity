using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    CharacterController cc;
    // �߷� ����
    float gravitiy = -20f;
    // ���� �ӷ� ����
    float yVelocity = 0f;
    // ������ ����
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

        // ���� ī�޶� �������� ���� ����
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
