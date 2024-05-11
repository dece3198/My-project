using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopUi;
    bool isShop = false;

    private void Awake()
    {
        shopUi.SetActive(false);
    }

    public void OnOff()
    {
        isShop = !isShop;
        if(isShop)
        {
            shopUi.SetActive(true);
        }
        else
        {
            shopUi.SetActive(false);
        }
    }

    public void XButton()
    {
        shopUi.SetActive(false);
    }
}
