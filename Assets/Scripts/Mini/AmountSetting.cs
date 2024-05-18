using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmountSetting : MonoBehaviour
{
    [SerializeField] private Image effect;
    [SerializeField] private Slot slot;
    [SerializeField] private TextMeshProUGUI amountText;
    private Animator animator;
    private bool parent = false;

    private void Awake()
    {
        animator = effect.GetComponent<Animator>();
    }

    private void Update()
    {
        if(slot.item != null)
        {
            amountText.transform.parent.gameObject.SetActive(true);
            amountText.text = slot.item.state.min.ToString() + "~" + slot.item.state.max.ToString();
        }
        else
        {
            amountText.transform.parent.gameObject.SetActive(false);
        }
    }

    public void OnOff()
    {
        parent = !parent;
        if(parent)
        {
            effect.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            effect.transform.parent.gameObject.SetActive(false);
        }
    }

    public void AmountButton()
    {
        if(slot.item != null)
        {
            if(slot.isAmount)
            {
                if (slot.item.useType != UseType.None)
                {
                    slot.item.state.amount = Random.Range(slot.item.state.min, slot.item.state.max);
                    Inventory.instance.AcquireAItem(slot.item, slot.item.state.amount);
                    slot.item.state.amount = 0;
                    slot.MinusCount(1);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
}
