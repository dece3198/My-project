using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : Singleton<ItemStorage>
{
    public Item[] items;
    public GameObject[] prefab;
    private int rand = 0;

    public void ItemDropA(GameObject monster)
    {
        rand = Random.Range(0, 100);
        GameObject item;
        if (rand < 50)
        {
            item = Instantiate(items[0].itemPrefab);
            item.transform.position = monster.transform.position;
        }
        else if(rand > 40 && rand < 90)
        {
            item = Instantiate(items[3].itemPrefab);
            item.transform.position = monster.transform.position;
        }
        else if(rand > 90 && rand < 98)
        {
            item = Instantiate(items[1].itemPrefab);
            item.transform.position = monster.transform.position;
        }
        else
        {
            item = Instantiate(items[6].itemPrefab);
            item.transform.position = monster.transform.position;
        }
    }
}
