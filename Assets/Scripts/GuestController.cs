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
        if(guest.destination != null)
        {
            guest.agent.SetDestination(guest.destination.transform.position);
            guest.destination = null;
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
        guest.animator.Play("Worry");
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
            if (rand < 30)
            {
                guest.ChangeState(GuestState.Buy);
            }
            else if (rand > 30 && rand < 61)
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
        if (guest.slot != null)
        {
            guest.slot.ClearSlot();
        }
    }

    public override void Exit(GuestController guest)
    {
    }

    public override void Update(GuestController guest)
    {
        if(guest.buyItem != null)
        {
            if (percentage < (guest.buyItem.item.percentage * guest.classType[guest.buyItem.itemClass]))
            {
                percentage += 1;
                GoldManager.instance.gold += 1;
            }
            else
            {
                percentage = 0;
                guest.buyItem = null;
            }
        }

        if (guest.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            guest.ChangeState(GuestState.Out);
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
        guest.slot.buyType = BuyType.None;
        Generator.instance.isItem = true;
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
    public Slot slot;
    private StateMachine<GuestState,GuestController> stateMachine = new StateMachine<GuestState,GuestController>();
    public Dictionary<ClassType,float> classType = new Dictionary<ClassType,float>();

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
        classType.Add(ClassType.None, 0);
        classType.Add(ClassType.Normal, 1);
        classType.Add(ClassType.Rare, 1.5f);
        classType.Add(ClassType.Unique, 2);
        classType.Add(ClassType.Legend, 4);
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
