using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerCamera playerCamera;
    private bool inventoryActivated = false;
    private void Awake()
    {
        mainUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        IKey();
    }

    private void IKey()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated)
            {
                mainUI.SetActive(true);
                if (playerController != null)
                {
                    playerController.isAttack = false;
                }
                if (playerCamera != null)
                {
                    PlayerCamera.instance.enabled = false;
                }
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                mainUI.SetActive(false);
                InventoryButton.instance.information.SetActive(false);
                if (playerController != null)
                {
                    playerController.isAttack = true;
                }
                if (playerCamera != null)
                {
                    PlayerCamera.instance.enabled = true;
                }
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

}
