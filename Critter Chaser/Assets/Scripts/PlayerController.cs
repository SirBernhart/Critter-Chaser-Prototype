using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody rb;
    private Transform cameraPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraPos = transform.GetChild(0).GetChild(0);
    }

    Vector3 moveDirection = Vector3.zero; 
    Vector3 auxPoint = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        CalculateMoveDirection();
        Debug.Log(moveDirection);
        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    // Calculates the vector3 that points from the camera to the player so as to get the direction it is looking at
    private void CalculateMoveDirection()
    {
        auxPoint = transform.position;
        auxPoint.y = cameraPos.position.y;
        moveDirection = auxPoint - cameraPos.position;
        moveDirection = moveDirection.normalized;
    }
}
