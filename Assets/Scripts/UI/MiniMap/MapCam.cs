using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCam : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
       cam = GetComponent<Camera>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 280.0f, transform.position.z);
        if (transform.position.x <= 420.0f &&Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(1.5f, 0.0f, 0.0f);
        }
        // s->µÚ
        if (transform.position.x >= -216.0f  &&Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(-1 * 1.5f, 0.0f, 0.0f);
        }
        if (transform.position.z>=5.0f &&Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(0.0f, 0.0f, -1 * 1.5f);
        }
        if (transform.position.z <=1130.0f &&Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.0f, 0.0f, 1.5f);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel") * 30.0f;
        if (scroll > 0 && cam.orthographicSize <= 20)
            return;
        if (scroll < 0 && cam.orthographicSize > 270)
            return;
        cam.orthographicSize -= scroll;
    }
}
