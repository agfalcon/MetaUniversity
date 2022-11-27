using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 200f;
    float mx = 0;
    float my = 0;
    public GameObject UIDataO;
    UIData uiData;

    void Start()
    {
        uiData = UIDataO.GetComponent<UIData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!uiData.isStart)
            return;
        // ���콺 �Է� ���� �޾ƿ�
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        // ȸ�� �� ������ ���콺 �Է� ���� ����
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        my = Mathf.Clamp(my, -80f, 80f);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
