using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enhancement : MonoBehaviour
{
    [SerializeField] private Image effect;
    [SerializeField] private Slot slot;
    [SerializeField] private TextMeshProUGUI percentage;
    private Animator effectAnimator;
    int number = 0;
    private bool parent = false;

    private void Awake()
    {
        effectAnimator = effect.GetComponent<Animator>();
    }

    private void Update()
    {
        if (slot.item != null)
        {
            percentage.transform.parent.gameObject.SetActive(true);
            percentage.text = slot.item.percentage.ToString() + "%";
        }
        else
        {
            percentage.transform.parent.gameObject.SetActive(false);
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

    public void EnhancementButton()
    {
        if(effectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rank Up") || effectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rank Down") || effectAnimator.GetCurrentAnimatorStateInfo(0).IsName("effect Animation"))
        {
        }
        else
        {
            if (slot.item != null)
            {
                number = Random.Range(0, 100);
                Item item = slot.item.upGrade[0];
                if (slot.item.upGrade != null)
                {
                    if (number <= slot.item.percentage)
                    {
                        effectAnimator.SetBool("Up", true);
                        effectAnimator.Play("effect Animation");
                        slot.MinusCount(1);
                        Inventory.instance.AcquireItem(item);
                    }
                    else
                    {
                        effectAnimator.SetBool("Up", false);
                        effectAnimator.Play("effect Animation");
                        slot.MinusCount(1);
                    }
                }
                else
                {
                    Inventory.instance.AcquireItem(slot.item,slot.itemCount);
                    slot.ClearSlot();
                }
            }
        }
    }

}
