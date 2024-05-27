using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public Talk talk;
    public TextMeshProUGUI talkName;
    public TextMeshProUGUI talkData;
    public GameObject ButtonA;
    public GameObject ButtonB;
    public GameObject ButtonC;
    public GameObject ButtonD;
    public Slot slot;
    private int talkNumber = 0;
    private int GoldCount = 0;
    [SerializeField] private GameObject EventUi;
    [SerializeField] private GameObject maximizeButton;
    private bool isKing = false;
    private bool isNobility = false;
    private bool isAssassinm = false;
    [SerializeField] private GameObject bankruptcy;

    private void Update()
    {
        if(isKing)
        {
            King();
        }
        else if(isNobility)
        {
            Noblilty();
        }
        else if(isAssassinm)
        {
            Assassinm();
        }
    }

    public void AddTalk(int _Number)
    {
        talkName.text = talk.talks[_Number].talkerName;
        talkData.text = talk.talks[_Number].conversation;
        if (talk.talks[_Number].talkNumber == 0)
        {
            ButtonA.SetActive(false);
            ButtonB.SetActive(false);
            ButtonC.SetActive(true);
            ButtonD.SetActive(true);
        }
        else
        {
            ButtonA.SetActive(true);
            ButtonB.SetActive(true);
            ButtonC.SetActive(false);
            ButtonD.SetActive(false);
            slot.gameObject.SetActive(true);
        }
    }

    public void Before()
    {
        if(talkNumber > 0)
        {
            talkNumber -= 1;
            AddTalk(talkNumber);
        }
    }

    public void Next()
    {
        talkNumber += 1;
        AddTalk(talkNumber);
    }

    public void minimization()
    {
        EventUi.SetActive(false);
        maximizeButton.SetActive(true);
    }

    public void maximize()
    {
        EventUi.SetActive(true);
        maximizeButton.SetActive(false);
    }

    public void OkButton()
    {
        switch (talk.characterState)
        {
            case CharacterState.King : KingOn(); break;
            case CharacterState.Nobility: NobliltyOn(); break;
            case CharacterState.Assassinm: isAssassinm = true; break;
        }

    }

    private void KingOn()
    {
        isKing = true;
        GoldManager.instance.goldTextB.gameObject.SetActive(true);
        GoldManager.instance.goldTextB.text = "+" + ((slot.item.percentage + 1000) * transform.GetComponentInParent<GuestController>().classType[slot.itemClass]).ToString();
    }

    private void King()
    {
        if (slot.item != null)
        {
            if (GoldCount < ((slot.item.percentage + 1000) * transform.GetComponentInParent<GuestController>().classType[slot.itemClass]))
            {
                GoldManager.instance.gold += 1;
                GoldCount += 1;
            }
            else
            {
                isKing = false;
                slot.ClearSlot();
                EventUi.SetActive(false);
                transform.GetComponentInParent<GuestController>().ChangeState(GuestState.EventOut);
                GoldCount = 0;
            }
        }
        else
        {
            isKing = false;
        }
    }

    private void NobliltyOn()
    {
        isNobility = true;
        GoldManager.instance.goldTextB.gameObject.SetActive(true);
        GoldManager.instance.goldTextB.text = "+" + ((slot.item.percentage * transform.GetComponentInParent<GuestController>().classType[slot.itemClass]) / 2).ToString();
    }

    private void Noblilty()
    {
        if(slot.item != null)
        {
            if (slot.itemClass == ClassType.Unique || 
                slot.itemClass == ClassType.Legend || 
                slot.item.ingredientType == IngredientType.Gold)
            {
                if (GoldCount < ((slot.item.percentage * transform.GetComponentInParent<GuestController>().classType[slot.itemClass]) / 2))
                {
                    GoldManager.instance.gold += 1;
                    GoldCount += 1;
                }
                else
                {
                    isNobility = false;
                    slot.ClearSlot();
                    EventUi.SetActive(false);
                    transform.GetComponentInParent<GuestController>().ChangeState(GuestState.EventOut);
                    GoldCount = 0;
                }
            }
        }
        else
        {
            isNobility = false;
        }
    }

    private void Assassinm()
    {
        if (slot.item != null)
        {
            if (slot.itemClass == ClassType.Rare ||
               slot.itemClass == ClassType.Unique ||
               slot.itemClass == ClassType.Legend ||
               slot.item.ingredientType == IngredientType.Gold)
            {
                isAssassinm = false;
                slot.ClearSlot();
                EventUi.SetActive(false);
                transform.GetComponentInParent<GuestController>().ChangeState(GuestState.EventOut);
                GoldCount = 0;
            }
        }
        else
        {
            isAssassinm = false;
        }
    }

    public void NoButton()
    { 
        Time.timeScale = 0;
        slot.ClearSlot();
        EventUi.SetActive(false);
        bankruptcy.SetActive(true);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
