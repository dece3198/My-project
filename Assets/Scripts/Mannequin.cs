using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponPoint;
    [SerializeField] private Slot[] slots;
    [SerializeField] private GameObject slotUi;

    private void Update()
    {
        if(slots.Length > 0)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemType == ItemType.Equipment)
                    {
                        for (int j = 0; j < weaponPoint[i].transform.childCount; j++)
                        {
                            if (slots[i].item == weaponPoint[i].transform.GetChild(j).GetComponent<ItemPickUp>().item)
                            {
                                if (weaponPoint[i].transform.GetChild(j).gameObject.activeSelf == false)
                                {
                                    weaponPoint[i].transform.GetChild(j).gameObject.SetActive(true);
                                    Generator.instance.isItem = true;
                                }
                            }
                            else
                            {
                                if (weaponPoint[i].transform.GetChild(j).gameObject.activeSelf)
                                {
                                    weaponPoint[i].transform.GetChild(j).gameObject.SetActive(false);
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < weaponPoint[i].transform.childCount; j++)
                    {
                        weaponPoint[i].transform.GetChild(j).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void OnOff()
    {
        slotUi.SetActive(true);
    }

    public void XButton()
    {
        slotUi.SetActive(false);
    }
}
