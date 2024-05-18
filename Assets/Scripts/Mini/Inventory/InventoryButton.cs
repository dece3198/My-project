using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
    public GoldManager goldManager;

    private void Awake()
    {
        instance = this;
        SetColor(0);
        if(SceneManager.GetActiveScene().name == "Home")
        {
            playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        }
        useText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(goldManager == null)
        {
            goldManager = GameObject.Find("Canvas1").transform.GetChild(2).GetComponent<GoldManager>();
        }
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
            if (slot.item.useType == UseType.Return)
            {
                SceneManager.LoadScene("Home");
                slot.MinusCount(1);
            }

            if (slot.amount < 1)
            {
                return;
            }
            else
            {
                if (slot.item.useType == UseType.HpUp)
                {
                    StartCoroutine(TextCo());
                    playerState.HpUp(slot.amount);
                }
                else if (slot.item.useType == UseType.Gold)
                {
                    goldManager.gold += slot.item.state.amount;
                }
                slot.MinusCount(1);
                SetColor(0);
            }
        }
        slot = null;
    }

    public void InformationButton(Slot slot)
    {
        information.SetActive(true);
        image.sprite = slot.itemImage.sprite;
        nameText.text = slot.item.state.name;
        textB.text = slot.item.state.content;
        if (slot.amount <= 0)
        {
            if (slot.item.useType != UseType.Return && slot.item.useType != UseType.None)
            {
                textA.text = "??";
            }
            else
            {
                textA.gameObject.SetActive(false);
            }
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
        useText.text = slot.amount.ToString() + "을 회복했습니다";
        yield return new WaitForSeconds(2f);
        useText.gameObject.SetActive(false);
    }
}
