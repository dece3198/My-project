using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MixManager : MonoBehaviour
{
    [SerializeField] private Slot[] slots;
    [SerializeField] private Item[] items;
    public int itemNumber = 0;
    private Dictionary<IngredientType, int> ingredient = new Dictionary<IngredientType, int>();
    private bool parent = false;


    private void Awake()
    {
        ingredient.Add(IngredientType.GlassBottle, 0);
        ingredient.Add(IngredientType.Gem_1, 1);
        ingredient.Add(IngredientType.Gem_2, 2);
    }

    public void OnOff()
    {
        parent = !parent;
        if(parent)
        {
            slots[0].transform.parent.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            slots[0].transform.parent.transform.parent.gameObject.SetActive(false);
        }
    }

    public void MixButton()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.ingredientType == IngredientType.None)
                {
                    return;
                }
                else
                {
                    itemNumber += ingredient[slots[i].item.ingredientType];
                }
            }
        }
        switch(itemNumber)
        {
            case 0: return;
            case 1: Inventory.instance.AcquireItem(items[0]); break;
            case 2: Inventory.instance.AcquireItem(items[1]); break;
                default: return;
        }

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].MinusCount(1);
        }
        itemNumber = 0;
    }


}
