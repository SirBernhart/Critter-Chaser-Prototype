using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleToCrouch : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float inputX, inputY;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
        animator.SetFloat("InputY", inputY);
        animator.SetFloat("InputX", inputX);
    }
}
