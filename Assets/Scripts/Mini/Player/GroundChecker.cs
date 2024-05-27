using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Transform point;
    [SerializeField, Range(0, 1)] private float distance;
    [SerializeField] private CinemachineVirtualCamera curCam;
    [SerializeField] private CinemachineVirtualCamera moveCam;
    private RaycastHit hit;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        IsGrounded = Physics.Raycast(point.position, Vector3.down, distance, layerMask);
        if (SceneManager.GetActiveScene().name == "Blacksmith")
        {
            Debug.DrawRay(point.position, Vector3.down * distance, Color.red);
            if (Physics.Raycast(point.position, Vector3.down, out hit, distance, layerMask))
            {
                if (hit.transform.tag == "Smithy")
                {
                    curCam.Priority = 11;
                    moveCam.Priority = 10;
                }
                else if(hit.transform.tag == "Shop")
                {
                    curCam.Priority = 10;
                    moveCam.Priority = 11;
                }
            }
        }
    }
}
