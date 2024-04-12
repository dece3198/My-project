using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveInventory : MonoBehaviour
{
    public static SaveInventory Instance;

    [SerializeField] private GameObject saveInventory;
    [SerializeField] private GameObject inventory;
    public Slot[] saveSlots;
    public Slot[] slots;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        saveSlots = saveInventory.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        if (inventory == null)
        {
            inventory = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
            slots = inventory.GetComponentsInChildren<Slot>();
            for(int i = 0; i < saveSlots.Length; i++)
            {
                if(saveSlots[i].item != null)
                {
                    InventoryV(i);
                }
            }
        }
        else
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item != saveSlots[i].item)
                    {
                        InventoryC(i);
                    }
                    else if (slots[i].itemCount != saveSlots[i].itemCount)
                    {
                        InventoryC(i);
                    }
                }
                else
                {
                    saveSlots[i].ClearSlot();
                }
            }
        }
    }

    public void InventoryC(int count)
    {
        saveSlots[count].AddItem(slots[count].item, slots[count].itemCount);
        
        if(saveSlots[count].item.useType != UseType.None)
        {
            saveSlots[count].amount = slots[count].amount;
        }
    }
    public void InventoryV(int count)
    {
        slots[count].AddItem(saveSlots[count].item, saveSlots[count].itemCount);
        if (saveSlots[count].item.useType != UseType.None)
        {
            slots[count].amount = saveSlots[count].amount;
        }
    }

}
