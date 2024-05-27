using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GuestIdle : BaseState<GuestController>
{
    public override void Enter(GuestController guest)
    {
    }

    public override void Exit(GuestController guest)
    {
    }

    public override void Update(GuestController guest)
    {
    }
}

public class GuestWalk : BaseState<GuestController>
{
    public override void Enter(GuestController guest)
    {
        guest.animator.SetBool("Walk", true);
        if(guest.transform.tag == "Event" || guest.transform.tag == "Guest")
        {
            guest.agent.SetDestination(guest.navManager.transform.position);
        }
        else
        {
            guest.agent.SetDestination(guest.destination.transform.position);
        }
        if(guest.soldierAnimatorA != null && guest.soldierAnimatorB != null)
        {
            guest.soldierAnimatorA.SetBool("Walk", true);
            guest.soldierAnimatorB.SetBool("Walk", true);
            guest.soldierAnimatorC.SetBool("Walk", true);
        }
    }

    public override void Exit(GuestController guest)
    {
    }

    public override void Update(GuestController guest)
    {
    }
}

public class GuestWorry : BaseState<GuestController>
{
    int rand;
    public override void Enter(GuestController guest)
    {
        guest.agent.ResetPath();
        guest.animator.Play("WorryLoopTime");
        guest.animator.SetBool("Walk", false);
    }

    public override void Exit(GuestController guest)
    {
    }

    public override void Update(GuestController guest)
    {
        if (guest.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            rand = Random.Range(0, 100);
            if (rand <  (30 * guest.classType[guest.buyItem.itemClass]))
            {
                guest.ChangeState(GuestState.Buy);
            }
            else if (rand > (30 * guest.classType[guest.buyItem.itemClass]) && rand < 91)
            {
                guest.ChangeState(GuestState.Out);
            }
            else
            {
                guest.ChangeState(GuestState.Angry);
            }
        }
    }

}

public class GuestBuy : BaseState<GuestController>
{
    int percentage = 0;

    public override void Enter(GuestController guest)
    {
        guest.animator.Play("Buy");
        GoldManager.instance.goldTextB.gameObject.SetActive(true);
        GoldManager.instance.goldTextB.text = "+" + ((guest.buyItem.item.percentage * guest.classType[guest.buyItem.itemClass] + guest.hiltType[guest.buyItem.item.hiltType])).ToString();
    }

    public override void Exit(GuestController guest)
    {
        percentage = 0;
        guest.buyItem.buyType = BuyType.None;
        guest.buyItem.ClearSlot();
        guest.buyItem = null;
    }

    public override void Update(GuestController guest)
    {
        if(guest.buyItem != null)
        {
            if (percentage < ((guest.buyItem.item.percentage * guest.classType[guest.buyItem.itemClass]) + guest.hiltType[guest.buyItem.item.hiltType]))
            {
                percentage += 1;
                GoldManager.instance.gold += 1;
            }
            else
            {
                if (guest.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    guest.ChangeState(GuestState.Out);
                }
            }
        }
    }
}

public class GuestAngry : BaseState<GuestController>
{
    public override void Enter(GuestController guest)
    {
        guest.animator.Play("Angry");
        guest.StartCoroutine(AngryCo(guest));
    }

    public override void Exit(GuestController guest)
    {
    }

    public override void Update(GuestController guest)
    {
    }

    private IEnumerator AngryCo(GuestController _guest)
    {
        yield return new WaitForSeconds(3);
        _guest.ChangeState(GuestState.Out);
    }
}

public class GuestOut : BaseState<GuestController>
{
    public override void Enter(GuestController guest)
    {
        guest.animator.SetBool("Walk",true);
        guest.agent.SetDestination(guest.navManager.transform.position);
    }

    public override void Exit(GuestController guest)
    {
    }

    public override void Update(GuestController guest)
    {
    }
}

public class GuestEvent : BaseState<GuestController>
{
    public override void Enter(GuestController guest)
    {
        guest.animator.SetBool("Walk", false);
        guest.animator.SetBool("Worry", false);
        guest.animator.Play("Worry");
        if(guest.soldierAnimatorA != null)
        {
            guest.soldierAnimatorA.SetBool("Walk", false);
            guest.soldierAnimatorB.SetBool("Walk", false);
            guest.soldierAnimatorC.SetBool("Walk", false);
        }
        guest.eventManager.AddTalk(0);
        guest.eventManager.gameObject.SetActive(true);
    }

    public override void Exit(GuestController guest)
    {
    }

    public override void Update(GuestController guest)
    {
    }
}

public class GuestEventOut : BaseState<GuestController>
{
    public override void Enter(GuestController guest)
    {
        guest.animator.Play("Walk");
        guest.animator.SetBool("Walk", true);
        if(guest.soldierAnimatorA != null)
        {
            guest.soldierAnimatorA.SetBool("Walk", true);
            guest.soldierAnimatorB.SetBool("Walk", true);
            guest.soldierAnimatorC.SetBool("Walk", true);
        }
        guest.agent.SetDestination(guest.navManager.transform.position);
    }

    public override void Exit(GuestController guest)
    {
    }

    public override void Update(GuestController guest)
    {
    }
}

public class GuestController : MonoBehaviour
{
    public GuestState state;
    public Animator animator;
    public NavMeshAgent agent;
    public GameObject destination;
    public Slot buyItem;
    public NavManager navManager;
    private StateMachine<GuestState,GuestController> stateMachine = new StateMachine<GuestState,GuestController>();
    public Dictionary<ClassType,float> classType = new Dictionary<ClassType,float>();
    public Dictionary<HiltType, int> hiltType = new Dictionary<HiltType, int>();
    public EventManager eventManager;
    public Animator soldierAnimatorA;
    public Animator soldierAnimatorB;
    public Animator soldierAnimatorC;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Reset(this);
        stateMachine.AddState(GuestState.Idle, new GuestIdle());
        stateMachine.AddState(GuestState.Walk, new GuestWalk());
        stateMachine.AddState(GuestState.Worry, new GuestWorry());
        stateMachine.AddState(GuestState.Buy, new GuestBuy());
        stateMachine.AddState(GuestState.Angry, new GuestAngry());
        stateMachine.AddState(GuestState.Out, new GuestOut());
        stateMachine.AddState(GuestState.Event, new GuestEvent());
        stateMachine.AddState(GuestState.EventOut, new GuestEventOut());
        ChangeState(GuestState.Idle);
        classType.Add(ClassType.None, 0);
        classType.Add(ClassType.Normal, 1);
        classType.Add(ClassType.Rare, 1.5f);
        classType.Add(ClassType.Unique, 2);
        classType.Add(ClassType.Legend, 4);
        hiltType.Add(HiltType.BasicSwordHilt, 0);
        hiltType.Add(HiltType.LuxurySwordHilt, 130);
    }

    private void OnEnable()
    {
        if(eventManager != null)
        {
            eventManager.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void ChangeState(GuestState guestState)
    {
        state = guestState;
        stateMachine.ChangeState(guestState);
    }
}
