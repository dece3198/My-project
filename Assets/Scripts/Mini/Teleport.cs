using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject teleport;
    private bool isTp = false;

    private void Update()
    {
        if (isTp)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnOff()
    {
        isTp = !isTp;
        if (isTp)
        {
            teleport.SetActive(true);
            PlayerCamera.instance.enabled = false;
        }
        else
        {
            teleport.SetActive(false);
            PlayerCamera.instance.enabled = true;
            Cursor.lockState = CursorLockMode.Locked ;
        }
    }

    public void DungeonButton()
    {
        if (GoldManager.instance.gold >= 15)
        {
            InventoryButton.instance.goldManager.gold -= 15;
            SceneManager.LoadScene("Dungeon Map");
        }
        else
        {
            return;
        }
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("Home");
    }
}
