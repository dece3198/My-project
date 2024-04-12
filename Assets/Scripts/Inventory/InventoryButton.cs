using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryButton : MonoBehaviour
{
    public static InventoryButton instance;
    public Slot slot;
    public Image buttonImageA;
    public GameObject information;
    [SerializeField] private TextMeshProUGUI useText;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI textA;
    [SerializeField] private TextMeshProUGUI textB;
    [SerializeField] private float time;

    private void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
        SetColor(0);
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        useText.gameObject.SetActive(false);
    }



    public void SetColor(float alpha)
    {
        Color color = buttonImageA.color;
        color.a = alpha;
        buttonImageA.color = color;
        buttonImageA.GetComponentInChildren<TextMeshProUGUI>().color = color;
    }

    public void UseButton()
    {
        if(slot.item.itemType == ItemType.Used)
        {
            if(slot.amount < 1)
            {
                return;
            }
            else
            {
                slot.MinusCount(1);
                SetColor(0);
                if (slot.item.useType == UseType.HpUp)
                {
                    StartCoroutine(TextCo());
                    if(time == 0)
                    {

                    }
                    playerState.HpUp(slot.amount);
                }
            }
        }
    }

    public void InformationButton(Slot slot)
    {
        information.SetActive(true);
        image.sprite = slot.itemImage.sprite;
        nameText.text = slot.item.state.name;
        textB.text = slot.item.state.content;
        if (slot.item.state.amount == 0)
        {
            textA.gameObject.SetActive(false);
        }
        else
        {
            textA.gameObject.SetActive(true);
            textA.text = slot.amount.ToString();
        }
    }

    IEnumerator TextCo()
    {
        useText.gameObject.SetActive(true);
        useText.text = slot.amount.ToString() + "�� ȸ���߽��ϴ�";
        yield return new WaitForSeconds(2f);
        useText.gameObject.SetActive(false);
    }
}