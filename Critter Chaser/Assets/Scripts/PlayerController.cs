using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxMoveSpeed;
    public float moveSpeedAcceleration;
    public float moveSpeedDeacceleration;
    private float currentMoveSpeed;
    public float jumpForce;
    public CharacterController characterController;
    public Transform cameraPivot;
    public Transform playerCamera;
    public Transform playerGraphics;
    public Animator animator;

    private Vector3 moveDirection;
    Vector3 lookDirection;
    Vector3 inputDirection;
    public float gravityScale;

    public float rotationSpeed;

    private bool isJumping;
    float vertical, horizontal;

    private void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        if (vertical != 0f || horizontal != 0f)
        {
            Vector3 lookDirection = GetMoveDirection();
            lookDirection.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0f, lookDirection.z));
            playerGraphics.rotation = Quaternion.Slerp(playerGraphics.rotation, newRotation, rotationSpeed * Time.deltaTime);

            if(currentMoveSpeed < maxMoveSpeed)
            {
                currentMoveSpeed += moveSpeedAcceleration;
            }
            animator.SetFloat("InputMove", currentMoveSpeed/maxMoveSpeed);
        }
        else
        {
            if(currentMoveSpeed > 0 && characterController.isGrounded)
            {
                currentMoveSpeed -= moveSpeedDeacceleration;
                if (currentMoveSpeed < 0)
                {
                    currentMoveSpeed = 0;
                }
                animator.SetFloat("InputMove", currentMoveSpeed / maxMoveSpeed);
            }
        }

        if (characterController.isGrounded)
        {
            isJumping = false;
            animator.SetFloat("Jumping", 0);
            moveDirection.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                isJumping = true;
            }
        }
        else
        {
            if (isJumping)
            {
                animator.SetFloat("Jumping", 1);
            }
            else
            {
                animator.SetFloat("Jumping", -1);
            }
            animator.SetFloat("InputMove", 0);
        }

        float yStore = moveDirection.y;
        moveDirection = GetMoveDirection() * currentMoveSpeed;
        moveDirection.y = yStore;

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public Vector3 GetMoveDirection()
    {
        Vector3 auxiliaryPoint = new Vector3(cameraPivot.position.x, playerCamera.position.y, cameraPivot.position.z);
        Vector3 relativeForward = (auxiliaryPoint - playerCamera.position).normalized;

        return ((relativeForward * vertical) + (cameraPivot.right * horizontal)).normalized;
    }
}
