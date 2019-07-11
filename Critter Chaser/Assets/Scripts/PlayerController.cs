using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraPos = transform.GetChild(0).GetChild(0);
    }

    Vector3 forwardSpeed = new Vector3(2, 0, 0);
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + forwardSpeed);
        }
    }
}
