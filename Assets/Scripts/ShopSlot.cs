using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private TextMeshProUGUI priceText;
    public int price;

    private void Awake()
    {
        priceText.text = price.ToString() + "Gold";
    }

    public void BuyButton()
    {
        if(GoldManager.instance.gold >= price)
        {
            GoldManager.instance.gold -= price;
            Inventory.instance.AcquireItem(item);
        }
    }
}
