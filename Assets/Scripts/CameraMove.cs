using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera curCam;
    [SerializeField] private CinemachineVirtualCamera moveCam;
    private bool iscam = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerControllerA>() != null)
        {
            iscam = !iscam;
            if(iscam)
            {
                curCam.Priority = 10;
                moveCam.Priority = 11;
            }
            else
            {
                curCam.Priority = 11;
                moveCam.Priority = 10;
            }
        }
    }
}
