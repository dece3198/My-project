using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bell : Singleton<Bell>
{
    [SerializeField] private GameObject bellUi;
    [SerializeField] private GameObject OpenUi;
    [SerializeField] private TextMeshProUGUI PersonnelText;
    [SerializeField] private TextMeshProUGUI openText;
    public float alphaSpeed;
    public int Personnel = 0;
    private bool isUi = false;
    private Color color;

    private IEnumerator guestCO;

    private void Awake()
    {
        color = openText.color;
        SetColor(0);
    }

    private void Update()
    {
        if(openText.gameObject.activeSelf)
        {
            color.a = Mathf.Lerp(color.a , 0 ,Time.deltaTime * alphaSpeed);
            openText.color = color;
        }
    }

    private void SetColor(float alpha)
    {
        color.a = alpha;
        openText.color = color;
    }

    public void Add(int Number)
    {
        Personnel += Number;
        PersonnelText.text = Personnel.ToString() + " / 5";
    }

    public void OnOff()
    {
        isUi = !isUi;
        if(isUi)
        {
            Cursor.lockState = CursorLockMode.None;
            PlayerCamera.instance.enabled = false;
            bellUi.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            PlayerCamera.instance.enabled = true;
            bellUi.SetActive(false);
        }
    }

    public void Open()
    {
        for (int i = 0; i < RoadManager.instance.roads.Length; i++)
        {
            RoadManager.instance.roads[i].isClose = false;
        }
        Cursor.lockState = CursorLockMode.Locked;
        PlayerCamera.instance.enabled = true;
        guestCO = GuestGenerator.instance.GuestCo();
        StartCoroutine(guestCO);
        OpenUi.SetActive(true);
        bellUi.SetActive(false);
        SetColor(1);
        openText.text = "°¡°Ô ¿ÀÇÂ";
    }

    public void Close()
    {
        for(int i = 0; i < RoadManager.instance.roads.Length; i++)
        {
            RoadManager.instance.roads[i].isClose = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        PlayerCamera.instance.enabled = true;
        StopCoroutine(guestCO);
        OpenUi.SetActive(false);
        bellUi.SetActive(false);
        SetColor(1);
        openText.text = "°¡°Ô ´ÝÀ½";
    }
}
