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

    // 중력 변수
    float gravitiy = -10f;
    // 수직 속력 변수
    float yVelocity = 0f;
    // 점프력 변수
    public float jumpPower = 3f;
    [SerializeField]
    bool isJump = false;
    bool isRun = false;

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
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        //dir = dir.normalized;

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

    }

    bool IsCheckGrounded()
    {
        if (cc.isGrounded) return true;
        // 발사하는 광선의 초기 위치와 방향
        // 약간 신체에 박혀 있는 위치로부터 발사하지 않으면 제대로 판정할 수 없을 때가 있다.
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        // 탐색 거리
        var maxDistance = 1.5f;
        // 광선 디버그 용도
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);
        // Raycast의 hit 여부로 판정
        // 지상에만 충돌로 레이어를 지정
        return Physics.Raycast(ray, maxDistance);
    }

    void Jump()
    {
        if (isJump && cc.collisionFlags == CollisionFlags.CollidedBelow)
        {
            isJump = false;
            print("isJump && isGrounded");
        }

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            print("playerJump");
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
