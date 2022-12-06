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
    [HideInInspector]
    public float maxStat = 5f; // �ִ� ���¹̳�

    public Image statImg; // ���¹̳� ������

    CharacterController cc;
    Animator anim;

    float h, v;

    // �߷� ����
    float gravitiy = -10f;
    // ���� �ӷ� ����
    float yVelocity = 0f;
    // ������ ����
    public float jumpPower = 3f;
    [SerializeField]
    bool isJump = false;
    bool isRun = false;
    bool isWalk = false;

    public bool isF = false; // �÷��̾ ��ȣ�ۿ� ������

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
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        // ���� ī�޶� �������� ���� ����
        dir = Camera.main.transform.TransformDirection(dir);

        yVelocity += gravitiy * Time.deltaTime;
        dir.y = yVelocity;

        if (!IsCheckGrounded() && isF) // �������¿��� ��ȣ�ۿ� �� ĳ���Ͱ� ���߿��� ������·� ��ȣ�ۿ� �ϴ� ���� ����
        {
            print("!IsCheckGrounded");
            cc.Move(new Vector3(0f, yVelocity, 0f) * 3f * Time.deltaTime);
        }

        if (!isF)
        {
            Jump();
            Move();
            cc.Move(dir * currentSpeed * Time.deltaTime);
        }
        UpdateAnim();
    }

    bool IsCheckGrounded()
    {
        if (cc.isGrounded) return true;
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        var maxDistance = 1.5f;
        return Physics.Raycast(ray, maxDistance);
    }

    void UpdateAnim()
    {
        anim.SetFloat("speed", currentSpeed);
    }

    void Jump()
    {
        if (isJump && cc.collisionFlags == CollisionFlags.CollidedBelow)
        {
            Invoke(nameof(SetIsJumpFalse), 0.5f);
        }

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            yVelocity = jumpPower;
            isJump = true;
        }
    }

    void SetIsJumpFalse()
    {
        isJump = false;
    }

    void Move()
    {
        if (!isRun && (h != 0 || v != 0))
        {
            isWalk = true;
            currentSpeed = walkSpeed;
        }
        else if (isF)
        {
            isWalk = false;
            currentSpeed = 0f;
        }
        else isWalk = false;

        if (!isRun && Input.GetButton("LShift") && stat >= 0.5)
        {
            currentSpeed = runSpeed;
            isRun = true;
            isWalk = false;
        }
        else if(Input.GetButtonUp("LShift") || stat <= 0)
        {
            currentSpeed = walkSpeed;
            isRun = false;
            isWalk = true;
        }
        else if(!isWalk && !isRun)
        {
            currentSpeed = 0;
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
