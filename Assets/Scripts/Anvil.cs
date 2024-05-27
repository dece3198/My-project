using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Anvil : MonoBehaviour
{
    public static Anvil instance;
    public GameObject ingredient;
    public GameObject point;
    public int itemNumber = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private Image skipImage;
    private bool isRight = false;
    private bool isLeft = false;
    private bool ismid = false;
    private bool isHammeringCool = true;
    private bool isSkip = false;
    private int skip = 1;

    private void Awake()
    {
        instance = this;
    }

    public void LeftButton()
    {
        if(ingredient != null)
        {
            if(isHammeringCool)
            {
                StartCoroutine(HammeringCo());
                if (ingredient.transform.GetChild(0).transform.localScale.z > 30)
                {
                    animator.SetTrigger("Hammering");
                    ingredient.transform.GetChild(0).transform.localScale -= new Vector3(0, 0, 10f);
                    ingredient.transform.GetChild(0).transform.position -= new Vector3(0, 0.005f, 0);
                    isLeft = true;
                }
                else
                {
                    if (isLeft)
                    {
                        ingredient.transform.GetChild(0).gameObject.SetActive(false);
                        itemNumber += 3;
                        isLeft = false;
                    }
                }
            }
        }
    }

    public void RightButton()
    {
        if (ingredient != null)
        {
            if(isHammeringCool)
            {
                StartCoroutine(HammeringCo());
                if (ingredient.transform.GetChild(1).transform.localScale.z > 30)
                {
                    animator.SetTrigger("Hammering");
                    ingredient.transform.GetChild(1).transform.localScale -= new Vector3(0, 0, 10f);
                    ingredient.transform.GetChild(1).transform.position -= new Vector3(0, 0.005f, 0);
                    isRight = true;
                }
                else
                {
                    if (isRight)
                    {
                        ingredient.transform.GetChild(1).gameObject.SetActive(false);
                        itemNumber += 2;
                        isRight = false;
                    }
                }
            }
        }
    }

    public void MiddleButton()
    {
        if (ingredient != null)
        {
            if(isHammeringCool)
            {
                StartCoroutine(HammeringCo());
                if (ingredient.transform.localScale.z < 1.5f)
                {
                    animator.SetTrigger("Hammering");
                    ingredient.transform.localScale += new Vector3(0, 0, 0.05f);
                    ingredient.transform.GetChild(2).transform.localScale += new Vector3(0, 0, 0.05f);
                    ismid = true;
                }
                else
                {
                    if (ismid)
                    {
                        itemNumber += 1;
                        ismid = false;
                    }
                }
            }
        }
    }

    public void MakeButton()
    {
        if(itemNumber >= 3)
        {
            switch (itemNumber)
            {
                case 4 : AddItem(ingredient.GetComponent<ItemPickUp>().item.upGrade[0]); break;
                case 6 : AddItem(ingredient.GetComponent<ItemPickUp>().item.upGrade[1]); break;
            }
        }
    }

    private void AddItem(Item _item)
    {
        Destroy(ingredient);
        ingredient = null;
        Inventory.instance.AcquireItem(_item);
        itemNumber = 0;
    }

    private IEnumerator HammeringCo()
    {
        isHammeringCool = false;
        yield return new WaitForSeconds(skip);
        isHammeringCool = true;
    }

    public void SkipButton()
    {
        isSkip = !isSkip;
        if(isSkip)
        {
            skip = 0;
            SetColor(0.5f);
        }
        else
        {
            skip = 1;
            SetColor(1);
        }
    }

    private void SetColor(float alpha)
    {
        Color color = skipImage.color;
        color.a = alpha;
        skipImage.color = color;
    }
}
