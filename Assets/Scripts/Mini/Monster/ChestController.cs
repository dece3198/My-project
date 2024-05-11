using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ChestOnOffState
{
    Open, Close
}

public class ChestOpen : BaseState<ChestController>
{
    public override void Enter(ChestController chestController)
    {
        chestController.animator.SetTrigger("Open");
        chestController.chestInvertoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        if (chestController.saveItem != null)
        {
            for (int i = 0; i < chestController.itemNumber; i++)
            {
                chestController.AcquireItem(chestController.saveItem[i]);
            }
        }
    }

    public override void Exit(ChestController chestController)
    {

    }

    public override void Update(ChestController chestController)
    {
        if(chestController.detector.SubTarget == null)
        {
            chestController.ChangeState(ChestOnOffState.Close);
        }
    }
}

public class ChestClose : BaseState<ChestController>
{
    public override void Enter(ChestController chestController)
    {
        chestController.animator.SetTrigger("Close");
        if(chestController.chestInvertoryUI != null)
        {
            chestController.chestInvertoryUI.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        if(SceneManager.GetActiveScene().name == "Dungeon Map")
        {
            for (int i = 0; i < chestController.slots.Length; i++)
            {
                chestController.slots[i].ClearSlot();
            }
        }
    }

    public override void Exit(ChestController chestController)
    {

    }

    public override void Update(ChestController chestController)
    {

    }
}


public class ChestController : MonoBehaviour
{
    public ChestOnOffState state;
    public Animator animator;
    public GameObject chestInvertoryUI;
    public ViewDetector detector;
    public int itemNumber = 0;
    public List<Item> item = new List<Item>();
    [SerializeField] private GameObject slotsParent;
    public Slot[] slots;
    public List<Item> saveItem = new List<Item>();
    private StateMachine<ChestOnOffState, ChestController> stateMachine = new StateMachine<ChestOnOffState, ChestController>();

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine.Reset(this);
        stateMachine.AddState(ChestOnOffState.Open, new ChestOpen());
        stateMachine.AddState(ChestOnOffState.Close, new ChestClose());
        ChangeState(ChestOnOffState.Close);
    }

    private void Start()
    {
        SaveChest();
    }

    private void Update()
    {
        detector.FindSubTarget();
        stateMachine.Update();
        if(SceneManager.GetActiveScene().name == "Home")
        {
            if (chestInvertoryUI == null)
            {
                if(SceneManager.GetActiveScene().name == "Home")
                chestInvertoryUI = GameObject.Find("Canvas1").transform.GetChild(0).gameObject;
                slotsParent = GameObject.Find("Canvas1").transform.GetChild(0).gameObject;
            }
        }
        slots = slotsParent.GetComponentsInChildren<Slot>();
    }

    public void SaveChest()
    {
        if (SceneManager.GetActiveScene().name == "Dungeon Map")
        {
            for (int i = 0; i < itemNumber; i++)
            {
                int rand = Random.Range(0, item.Count);
                saveItem.Add(item[rand]);
            }
        }
    }
    public void ChangeState(ChestOnOffState nextState)
    {
        state = nextState;
        stateMachine.ChangeState(nextState);
    }

    public void AcquireItem(Item _item, int count = 1)
    {
        if (_item.itemType != ItemType.Equipment)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item == _item)
                    {
                        slots[i].PlusCount(count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                if (slots[i].gameObject.activeSelf)
                {
                    slots[i].AddItem(_item, count);
                    return;
                }
            }
        }
    }
}
