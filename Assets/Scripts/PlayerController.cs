using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;

    public bool isMove;
    public bool isAttack = true;
    private float moveY;

    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraPoint;

    
    [SerializeField] private Transform atkPoint;
    [SerializeField] private Vector3 attackRang;
    [SerializeField] private LayerMask layerMask;
    private IInteractable preTarget;
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private GroundChecker groundChecker;

    private Vector3 moveVec;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(isAttack == true)
        {
            PlayerMove();
            if (Input.GetMouseButtonDown(0))
            {
                animator.Play("Attack");
                Interaction();
                StartCoroutine(AttackCool());
            }
        }
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

    IEnumerator AttackCool()
    {
        isAttack = false;
        yield return new WaitForSeconds(1.25f);
        isAttack = true;
    }

    private void Interaction()
    {
        Collider[] targets = Physics.OverlapBox(atkPoint.position, attackRang, transform.rotation, layerMask);

        if(targets.Length <= 0)
        {
            preTarget = null;
            return;
        }

        for(int i = 0; i < targets.Length; i++)
        {
            IInteractable target = targets[i].GetComponent<IInteractable>();

            if(preTarget != target)
            {
                preTarget = target;
            }
            if(isAttack == true)
            {
                preTarget?.TakeHit(damage);
            }
        }
    }

    private void PlayerMove()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if(moveInput.magnitude > 0)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        if(!isMove)
        {
            return;
        }

        Vector3 forwarVec = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z);
        Vector3 rightVec = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z);

        moveVec = moveInput.x * rightVec + moveInput.z * forwarVec;
        transform.localRotation = Quaternion.LookRotation(moveVec);

        characterController.Move(moveVec * moveSpeed * Time.deltaTime);
    }

    private void Gravity()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            moveY = jumpSpeed * Time.deltaTime;
        }

        if(groundChecker.IsGrounded)
        {
            moveY = 0;
        }
        else
        {
            moveY += Physics.gravity.y * Time.deltaTime;
        }
        characterController.Move(Vector3.up * moveY * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(atkPoint.position, attackRang);
    }
}
