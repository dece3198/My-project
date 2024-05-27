using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject setting;
    private bool inventoryActivated = false;

    private void Awake()
    {
        mainUI.SetActive(false);
        if (SceneManager.GetActiveScene().name != "Blacksmith")
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        IKey();
        if (SceneManager.GetActiveScene().name != "Blacksmith")
        {
            EscKey();
        }
    }


    private void EscKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated)
            {
                setting.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                if (playerController != null)
                {
                    playerController.isAttack = false;
                    playerCamera.GetComponent<CinemachineBrain>().enabled = false;
                }
                else
                {
                    playerCamera.GetComponent<PlayerCamera>().enabled = false;
                }
            }
            else
            {
                setting.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                if (playerController != null)
                {
                    playerController.isAttack = true;
                    playerCamera.GetComponent<CinemachineBrain>().enabled = true;
                }
                else
                {
                    playerCamera.GetComponent<PlayerCamera>().enabled = true;
                }
            }
        }
    }

    private void IKey()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated)
            {
                mainUI.SetActive(true);
                if (SceneManager.GetActiveScene().name != "Blacksmith")
                {
                    if (playerController != null)
                    {
                        playerController.isAttack = false;
                        playerCamera.GetComponent<CinemachineBrain>().enabled = false;
                    }
                    else
                    {
                        playerCamera.GetComponent<PlayerCamera>().enabled = false;
                    }
                    Cursor.lockState = CursorLockMode.None;
                }
            }
            else
            {
                mainUI.SetActive(false);
                InventoryButton.instance.information.SetActive(false);
                if (SceneManager.GetActiveScene().name != "Blacksmith")
                {
                    if (playerController != null)
                    {
                        playerController.isAttack = true;
                        playerCamera.GetComponent<CinemachineBrain>().enabled = true;
                    }
                    else
                    {
                        playerCamera.GetComponent<PlayerCamera>().enabled = true;
                    }
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }

}
