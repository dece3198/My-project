using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum GuestState
{
    Idle,Walk,Buy,Out,Worry, Angry, Event, EventOut
}


public class IdleState : BaseState<Guest>
{
    public override void Enter(Guest guest)
    {
        guest.animator.SetBool("Walk", false);
    }

    public override void Exit(Guest guest)
    {

    }

    public override void Update(Guest guest)
    {

    }
}

public class WalkState : BaseState<Guest>
{
    public override void Enter(Guest guest)
    {
        guest.animator.SetBool("Walk", true);
        guest.agent.SetDestination(guest.roadManager.transform.position);
    }

    public override void Exit(Guest guest)
    {

    }

    public override void Update(Guest guest)
    {

    }

}

public class BuyState : BaseState<Guest>
{
    public override void Enter(Guest guest)
    {
        guest.animator.SetBool("Walk", false);
        guest.buyUi.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Add(guest);
    }

    public override void Exit(Guest monster)
    {
    }

    public override void Update(Guest monster)
    {
    }

    private void Add(Guest guest)
    {
        guest.rand = Random.Range(0, guest.talk.talks.Length);
        guest.guestImage.sprite = guest.talk.talks[guest.rand].image;
        guest.guestName.text = guest.talk.talks[guest.rand].talkerName;
        guest.conversation.text = guest.talk.talks[guest.rand].conversation;
        guest.price.text = guest.talk.talks[guest.rand].price;
    }     
}

public class OutState : BaseState<Guest>
{
    public override void Enter(Guest guest)
    {
        guest.animator.SetBool("Walk", true);
        guest.buyUi.SetActive(false);
    }

    public override void Exit(Guest guest)
    {

    }

    public override void Update(Guest guest)
    {

    }
}


public class Guest : MonoBehaviour
{
    public GuestState guestState;
    public Animator animator;
    public NavMeshAgent agent;
    private StateMachine<GuestState, Guest> stateMachine = new StateMachine<GuestState, Guest>();
    public GameObject roadManager;
    public GameObject buyUi;
    public Talk talk;
    public Image guestImage;
    public TextMeshProUGUI guestName;
    public TextMeshProUGUI conversation;
    public TextMeshProUGUI price;
    public int rand;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Reset(this);
        stateMachine.AddState(GuestState.Idle, new IdleState());
        stateMachine.AddState(GuestState.Walk, new WalkState());
        stateMachine.AddState(GuestState.Buy, new BuyState());
        stateMachine.AddState(GuestState.Out, new OutState());
        ChangeState(GuestState.Idle);
        buyUi.SetActive(false);
    }

    private void OnEnable()
    {
        roadManager = GameObject.Find("RoadManager").gameObject;
    }  


    private void Start()
    {
        ChangeState(GuestState.Walk);
    }

    public void ChangeState(GuestState _guestState)
    {
        guestState = _guestState;
        stateMachine.ChangeState(guestState);
    }

}
