using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum BuyType
{
    None,Sale
}


public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerClickHandler
{
    public Item item;
    public Image itemImage;
    public int itemCount;
    public int amount;
    public bool isAmount;
    public ClassType itemClass;
    public BuyType buyType;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] private GameObject outLine;
    [SerializeField] private GameObject dragSlot;
    [SerializeField] private InventoryButton button;
    [SerializeField] private Image classImage;

    private Dictionary<ClassType,Color> itemColors = new Dictionary<ClassType, Color>();

    private void Awake()
    {
        outLine.SetActive(false);
        isAmount = true;
        itemColors.Add(ClassType.None, Color.white);
        itemColors.Add(ClassType.Normal,Color.gray);
        itemColors.Add(ClassType.Rare, Color.green);
        itemColors.Add(ClassType.Unique, Color.yellow);
        itemColors.Add(ClassType.Legend, Color.red);
    }


    private void Update()
    {
        if (itemCount <= 0)
        {
            ClearSlot();
        }
        if (dragSlot == null)
        {
            dragSlot = GameObject.Find("DragSlot").gameObject;
        }

        if(classImage != null)
        {
            if (itemClass != ClassType.None)
            {
                ClassColor();
            }
        }
    }

    public void AddItem(Item _item, int count = 1)
    {
        item = _item;
        itemCount = count; 
        itemImage.sprite = item.itemImage;
        amount = _item.state.amount;
        itemClass = item.classType;
        if (item.itemType != ItemType.Equipment)
        {
            countText.transform.parent.gameObject.SetActive(true);
            countText.text = itemCount.ToString();
        }
        else
        {
            countText.transform.parent.gameObject.SetActive(false);
        }

        if(item.classType == ClassType.None)
        {
            classImage.gameObject.SetActive(false);
        }
        else
        {
            classImage.gameObject.SetActive(true);
        }
        SetColor(1);
    }

    public void PlusCount(int count)
    {
        itemCount += count;
        countText.text = itemCount.ToString();
    }

    public void MinusCount(int count)
    {
        itemCount -= count;
        countText.text = itemCount.ToString();
    }

    public void TextCount(int count)
    {
        itemCount = count;
        countText.text = itemCount.ToString();
    }


    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
        countText.transform.parent.gameObject.SetActive(false);
        amount = 0;
        itemClass = ClassType.None;
        if(classImage != null)
        {
            classImage.gameObject.SetActive(false);
        }
    }

    private void ClassColor()
    {
        Color color = classImage.color;
        color = itemColors[itemClass];
        classImage.color = color;
    }

    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outLine.SetActive(true);
        if(item != null)
        {
            if(InventoryButton.instance != null)
            {
                InventoryButton.instance.InformationButton(this);
            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outLine.SetActive(false);
        if(item != null)
        {
            if (InventoryButton.instance != null)
            {
                InventoryButton.instance.information.SetActive(false);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                if(dragSlot != null)
                {
                    if (transform.parent.transform.tag != "Teleport" || item.classType == ClassType.None)
                    {
                        DragSlot.instance.dragSlot = this;
                        DragSlot.instance.DragSetImage();
                        DragSlot.instance.transform.position = eventData.position;
                        DragSlot.instance.isAmount = isAmount;
                        DragSlot.instance.amount = amount;
                        DragSlot.instance.classType = itemClass;
                    }
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                DragSlot.instance.transform.position = eventData.position;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        ClassType _tempType = itemClass;
        int _tempItemCount = itemCount;
        bool _isAmount = isAmount;
        int _amount = amount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        isAmount = DragSlot.instance.isAmount;
        amount = DragSlot.instance.amount;
        itemClass = DragSlot.instance.classType;

        if (DragSlot.instance.dragSlot.item == _tempItem)
        {
            if (DragSlot.instance.dragSlot.item.itemType != ItemType.Equipment)
            {
                if(DragSlot.instance.dragSlot != this)
                {
                    PlusCount(_tempItemCount);
                    DragSlot.instance.dragSlot.ClearSlot();
                }
                return;
            }
        }

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
            DragSlot.instance.dragSlot.isAmount = _isAmount;
            DragSlot.instance.dragSlot.amount = _amount;
            DragSlot.instance.dragSlot.itemClass = _tempType;
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(InventoryButton.instance != null)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (button != null)
                {
                    if (item != null)
                    {
                        button.slot = this;
                        button.transform.position = eventData.position;
                        button.gameObject.SetActive(true);
                        button.SetColor(1);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (button != null)
                {
                    button.slot = null;
                    button.gameObject.SetActive(false);
                    button.SetColor(0);
                }
            }
        }
    }
}
