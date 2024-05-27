using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopUi;

    private void Awake()
    {
        shopUi.SetActive(false);
    }

    public void OnOff()
    {
        shopUi.SetActive(true);
    }

    public void XButton()
    {
        shopUi.SetActive(false);
    }
}
