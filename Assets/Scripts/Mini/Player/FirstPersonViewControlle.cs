using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonViewControlle : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private CharacterController characterController;
    private Vector3 moveVec;
    private float moveY;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Gravity();
    }

    private void Move()
    {
        moveVec = (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward) * moveSpeed;
        characterController.Move(moveVec * Time.deltaTime);
    }

    private void Gravity()
    {
        if(characterController.isGrounded)
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
