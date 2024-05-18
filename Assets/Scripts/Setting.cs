using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private GameObject settingUi;

    private void Update()
    {
        if(playerCamera != null)
        {
            playerCamera.sensitivity = slider.value;
        }

        if(freeLook != null)
        {
            freeLook.m_XAxis.m_MaxSpeed = slider.value;
            freeLook.m_YAxis.m_MaxSpeed = (slider.value / 75);
        }

    }

    public void Xbutton()
    {
        settingUi.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

}
