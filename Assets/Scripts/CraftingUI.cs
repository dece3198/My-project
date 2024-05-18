using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private Image swordUi;
    [SerializeField] private Image spearUi;
    [SerializeField] private Image curImage;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Slot slot;
    [SerializeField] private Item[] swordItems;
    private GameObject clickUi;

    private Dictionary<BladeType, int> blade = new Dictionary<BladeType, int>();
    private Dictionary<HiltType, int> hilt = new Dictionary<HiltType, int>();

    public int itemNumber = 0;
    int rand = 0;

    private void Awake()
    {
        blade.Add(BladeType.Iron_SingleEdge, 1);
        blade.Add(BladeType.Iron_DoubleEdged, 2);

        hilt.Add(HiltType.BasicSwordHilt, 11);
        hilt.Add(HiltType.LuxurySwordHilt, 22);
    }

    private void RandomPercentage(Item _item)
    {
        rand = UnityEngine.Random.Range(0, 100);
        if(rand <= 50)
        {
            _item.classType = ClassType.Normal;
        }
        else if(rand > 50 && rand <= 85)
        {
            _item.classType = ClassType.Rare;
        }
        else if(rand > 85 && rand < 95)
        {
            _item.classType = ClassType.Unique;
        }
        else
        {
            _item.classType = ClassType.Legend;
        }
    }

    private void SetColor(Image image, float alpha)
    {

        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public void SwordUi()
    {
        swordUi.gameObject.SetActive(true);
        spearUi.gameObject.SetActive(false);
        SetColor(spearUi.transform.parent.GetComponent<Image>(), 0.5f);
        SetColor(swordUi.transform.parent.GetComponent<Image>(), 1);
    }

    public void SpearUi()
    {
        spearUi.gameObject.SetActive(true);
        swordUi.gameObject.SetActive(false);
        SetColor(swordUi.transform.parent.GetComponent<Image>(), 0.5f);
        SetColor(spearUi.transform.parent.GetComponent<Image>(), 1);
    }

    public void SwordButton()
    {
        clickUi = EventSystem.current.currentSelectedGameObject;
        curImage.sprite = clickUi.GetComponent<Image>().sprite;
    }

    public void Xbutton()
    {
        gameObject.SetActive(false);
        curImage.sprite = sprite;
    }

    public void OkButton()
    {
        if(slot.item.bladeType != BladeType.None && slot.item != null && curImage.sprite != sprite)
        {
            itemNumber += blade[slot.item.bladeType] + hilt[clickUi.GetComponent<ItemPickUp>().item.hiltType];
            switch (itemNumber)
            {
                case 12 : AddItem(swordItems[0]); break;
                case 13 : AddItem(swordItems[1]); break;
            }
        }
        itemNumber = 0;
    }

    private void AddItem(Item _item)
    {
        if (GoldManager.instance.gold >= clickUi.GetComponent<ItemPickUp>().item.percentage)
        {
            RandomPercentage(_item);
            Inventory.instance.AcquireItem(_item);
            itemNumber = 0;
            slot.MinusCount(1);
            curImage.sprite = sprite;
            GoldManager.instance.gold -= clickUi.GetComponent<ItemPickUp>().item.percentage;
            clickUi = null;
        }
    }
}
