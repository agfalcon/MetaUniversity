using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private static PlayerMove instance = null;

    public static PlayerMove Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    public float currentSpeed = 7f; // ���� �ӵ�
    public float walkSpeed = 7f; // �ȱ� �ӵ�
    public float runSpeed = 7f; // �޸��� �ӵ�
    public float stat = 5f; // ���¹̳�
    float maxStat = 5f; // �ִ� ���¹̳�

    public Image statImg; // ���¹̳� ������

    CharacterController cc;

    // �߷� ����
    float gravitiy = -20f;
    // ���� �ӷ� ����
    float yVelocity = 0f;
    // ������ ����
    public float jumpPower = 3f;
    bool isJump = false;
    bool isRun = false;

    public bool isF = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        //dir = dir.normalized;

        // ���� ī�޶� �������� ���� ����
        dir = Camera.main.transform.TransformDirection(dir);

        Jump();
        Move();

        yVelocity += gravitiy * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * currentSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (isJump && cc.collisionFlags == CollisionFlags.CollidedBelow)
        {
            isJump = false;
        }

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            yVelocity = jumpPower;
            isJump = true;
        }
    }

    void Move()
    {
        if (!isRun && Input.GetButton("LShift") && stat >= 0.5)
        {
            currentSpeed = runSpeed;
            isRun = true;
        }
        else if(Input.GetButtonUp("LShift") || stat <= 0)
        {
            currentSpeed = walkSpeed;
            isRun = false;
        }

        if (isRun)
        {
            stat -= Time.deltaTime;
        }
        else if (!isRun || stat <= maxStat)
        {
            stat += Time.deltaTime;
        }

        stat = Mathf.Clamp(stat, 0, maxStat);
        statImg.fillAmount = stat / maxStat;
    }

    public void SetMoveSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
