using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;
    public Slot dragSlot;
    public Image slotImage;
    public int amount;
    public bool isAmount;
    public ClassType classType;

    private void Awake()
    {
        instance = this;
        SetColor(0);
    }

    public void DragSetImage()
    {
        slotImage.sprite = dragSlot.itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float alpha)
    {
        Color color = slotImage.color;
        color.a = alpha;
        slotImage.color = color;
    }
}
