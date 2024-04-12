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
        }
        else
        {
            teleport.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked ;
        }
    }

    public void DungeonButton()
    {
        SceneManager.LoadScene("Dungeon Map");
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("Home");
    }
}