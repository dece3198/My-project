using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    [SerializeField] private GameObject craftingUi;

    public void OnOff()
    {
        craftingUi.SetActive(true);
    }
}
