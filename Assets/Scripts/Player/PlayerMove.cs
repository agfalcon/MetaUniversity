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

    public float currentSpeed = 7f; // 현재 속도
    public float walkSpeed = 7f; // 걷기 속도
    public float runSpeed = 7f; // 달리기 속도
    public float stat = 5f; // 스태미너
    [HideInInspector]
    public float maxStat = 5f; // 최대 스태미너

    public Image statImg; // 스태미너 게이지

    CharacterController cc;
    Animator anim;

    float h, v;

    // 중력 변수
    float gravitiy = -10f;
    // 수직 속력 변수
    float yVelocity = 0f;
    // 점프력 변수
    public float jumpPower = 3f;
    [SerializeField]
    bool isJump = false;
    bool isRun = false;
    bool isWalk = false;

    public bool isF = false; // 플레이어가 상호작용 중인지

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

        // 메인 카메라를 기준으로 방향 변경
        dir = Camera.main.transform.TransformDirection(dir);

        yVelocity += gravitiy * Time.deltaTime;
        dir.y = yVelocity;

        if (!IsCheckGrounded() && isF) // 점프상태에서 상호작용 시 캐릭터가 공중에서 멈춘상태로 상호작용 하는 것을 방지
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
