using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixManager : MonoBehaviour
{
    [SerializeField] private Slot[] slots;
    [SerializeField] private Item[] items;
    [SerializeField] private GameObject spawnPoint;
    public int itemNumber = 0;
    private Dictionary<IngredientType, int> ingredient = new Dictionary<IngredientType, int>();
    private bool parent = false;
    private bool isSpawn = false;
    [SerializeField] private Slider slider;
    float min;
    public float max;
    Item item;


    private void Awake()
    {
        ingredient.Add(IngredientType.GlassBottle, 11);
        ingredient.Add(IngredientType.Gem_1, 1);
        ingredient.Add(IngredientType.Gem_2, 2);
        ingredient.Add(IngredientType.Iron, 3);
        max = 10f;
    }

    private void Update()
    {
        if(isSpawn)
        {
            min += Time.deltaTime;
            slider.value = min / max;
        }
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
        if(slider != null)
        {
            if (slider.gameObject.activeSelf)
            {
                return;
            }
        }

        if(spawnPoint != null)
        {
            if (spawnPoint.transform.childCount > 0)
            {
                return;
            }
        }


        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.ingredientType == IngredientType.None)
                {
                    itemNumber = 0;
                    return;
                }
                else
                {
                    itemNumber += ingredient[slots[i].item.ingredientType];
                    item = slots[i].item;
                }
            }
        }

        switch(itemNumber)
        {
            case 0: itemNumber = 0; return;
            case 12: Inventory.instance.AcquireItem(items[0]); break;
            case 13: Inventory.instance.AcquireItem(items[1]); break;
            case 3: StartCoroutine(SpawnCo(item.upGrade[0])); break;
            default: itemNumber = 0; return;
        }

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].MinusCount(1);
        }
        itemNumber = 0;
    }

    private IEnumerator SpawnCo(Item _item)
    {
        isSpawn = true;
        slider.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        GameObject Mineral = Instantiate(_item.itemPrefab);
        Anvil.instance.ingredient = Mineral;
        Mineral.transform.position = spawnPoint.transform.position;
        Mineral.transform.parent = spawnPoint.transform;
        slider.gameObject.SetActive(false);
        isSpawn = false;
        min = 0;
    }
}
