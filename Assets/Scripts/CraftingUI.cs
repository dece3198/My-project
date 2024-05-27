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
    private GameObject clickUi;

    private Dictionary<BladeType, int> blade = new Dictionary<BladeType, int>();
    private Dictionary<HiltType, int> hilt = new Dictionary<HiltType, int>();

    int rand = 0;

    private void Awake()
    {
        hilt.Add(HiltType.BasicSwordHilt, 0);
        hilt.Add(HiltType.LuxurySwordHilt, 1);
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
        if(slot.item != null)
        {
            if (slot.item.bladeType != BladeType.None && curImage.sprite != sprite)
            {
                if (slot.item.upGrade != null)
                {
                    AddItem(slot.item.upGrade[hilt[clickUi.GetComponent<ItemPickUp>().item.hiltType]]);
                }
            }
        }
    }

    private void AddItem(Item _item)
    {
        if (GoldManager.instance.gold >= clickUi.GetComponent<ItemPickUp>().item.percentage)
        {
            RandomPercentage(_item);
            Inventory.instance.AcquireItem(_item);
            slot.MinusCount(1);
            curImage.sprite = sprite;
            GoldManager.instance.gold -= clickUi.GetComponent<ItemPickUp>().item.percentage;
            clickUi = null;
        }
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
}
