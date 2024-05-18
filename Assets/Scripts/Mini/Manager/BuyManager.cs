using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    [SerializeField] private Slot slot;
    [SerializeField] private Guest guest;

    public void Accept()
    {
        if(slot.item != null)
        {
            if (slot.item == guest.talk.talks[guest.rand].item)
            {
                if (slot.amount <= 0)
                {
                    return;
                }
                else
                {
                    InventoryButton.instance.goldManager.gold += slot.amount;
                    guest.ChangeState(GuestState.Out);
                    if(slot.itemCount > 1)
                    {
                        slot.MinusCount(1);
                        Inventory.instance.AcquireAItem(slot.item,slot.amount,slot.itemCount);
                    }
                    else
                    {
                        slot.MinusCount(1);
                    }

                }
            }
        }
    }

    public void Refuse()
    {
        if(slot.item != null)
        {
            Inventory.instance.AcquireItem(slot.item, slot.itemCount);
        }
        guest.ChangeState(GuestState.Out);
    }
}
