using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float p_speed = 5.0f;

    void Start()
    {

    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        h = h * p_speed * Time.deltaTime;
        v = v * p_speed * Time.deltaTime;

        transform.Translate(Vector3.right * h);
        transform.Translate(Vector3.forward * v);

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("충돌 감지");
    }

    private void OnTriggerEnter(Collider other)
    {
        print("트리거 감지");

        /*Destroy(other.gameObject);*/

        other.gameObject.SetActive(false);
    }
}
