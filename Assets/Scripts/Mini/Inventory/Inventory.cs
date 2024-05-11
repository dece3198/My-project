using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] private GameObject slotsParent;
    public Slot[] slots;
    private bool isIButton = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        slots = slotsParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        Ikey();
    }

    private void Ikey()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            isIButton = !isIButton;
            if(isIButton)
            {
                slotsParent.SetActive(true);
            }
            else
            {
                slotsParent.SetActive(false);
            }
            
        }
    }

    public void AcquireItem(Item _item, int count = 1)
    {
        if(_item.itemType != ItemType.Equipment)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item == _item)
                    {
                        slots[i].PlusCount(count);
                        return;
                    }
                }
            }
        }

        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                if (slots[i].gameObject.activeSelf)
                {
                    slots[i].AddItem(_item, count);
                    return;
                }
            }
        }
    }

    public void AcquireAItem(Item _item,int amount, int count = 1)
    {
        if (_item.itemType != ItemType.Equipment)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item == _item)
                    {
                        if(slots[i].amount != amount)
                        {
                            continue;
                        }
                        else
                        {
                            slots[i].PlusCount(count);
                            return;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                if (slots[i].gameObject.activeSelf)
                {
                    slots[i].AddItem(_item, count);
                    slots[i].amount = amount;
                    slots[i].isAmount = false;
                    return;
                }
            }
        }
    }
}
