using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                    if (weaponPoint[i].transform.childCount <= 0)
                    {
                        GameObject sword = Instantiate(slots[i].item.itemPrefab, weaponPoint[i].transform);
                        sword.transform.position = weaponPoint[i].transform.position;
                        sword.transform.rotation = weaponPoint[i].transform.rotation;
                    }
                }
                else
                {
                    if (weaponPoint[i].transform.childCount > 0)
                    {
                        for(int j = 0; j < weaponPoint[i].transform.childCount; j++)
                        {
                           Destroy(weaponPoint[i].transform.GetChild(j).gameObject);
                        }
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
