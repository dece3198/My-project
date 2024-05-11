using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerA : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] protected float rotateSpeed;
    public bool isMove = false;
    private CharacterController characterController;
    public Animator animator;
    private Vector3 moveVec;
    private float moveY;
    private float h;
    private float v;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Gravity();

        if (isMove == true)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    protected void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        if(dir.magnitude > 0)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        if (!(h == 0 && v == 0))
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }
    }

    private void Gravity()
    {
        if (characterController.isGrounded)
        {
            moveY = 0;
        }
        else
        {
            moveY += Physics.gravity.y * Time.deltaTime;
        }
        characterController.Move(Vector3.up * moveY * Time.deltaTime);
    }

}
