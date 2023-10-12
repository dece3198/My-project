using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public enum ChestState
{
    Idle, Interaction, Walk, Atk,Die
}



public class IdleState : BaseState<Chest>
{
    public override void Enter(Chest monster)
    {
        monster.animator.SetTrigger("Idle");
    }

    public override void Exit(Chest monster)
    {

    }

    public override void Update(Chest monster)
    {
    }
}

public class Interaction : BaseState<Chest>
{
    public override void Enter(Chest monster)
    {
        monster.animator.SetTrigger("IdleNomal");
        monster.StartCoroutine(InteractionCo(monster));
    }

    public override void Exit(Chest monster)
    {
    }

    public override void Update(Chest monster)
    {
        if(monster.detector.Target != null)
        {
            monster.ChangeState(ChestState.Walk);
        }
    }

    private IEnumerator InteractionCo(Chest monster)
    {
        yield return new WaitForSeconds(2f);
        if(monster.detector.SubTarget != null)
        {
            monster.transform.LookAt(monster.detector.SubTarget.transform);
        }
        else
        {
            monster.ChangeState(ChestState.Idle);
        }
    }
}


public class WalkState : BaseState<Chest>
{
    public override void Enter(Chest monster)
    {
        monster.animator.SetTrigger("Walk");
    }

    public override void Exit(Chest monster)
    {

    }

    public override void Update(Chest monster)
    {
        if(monster.detector.Target != null)
        {
            monster.nav.SetDestination(monster.detector.Target.transform.position);
        }
        else
        {
            monster.nav.ResetPath();
            monster.ChangeState(ChestState.Interaction);
        }

        if(monster.detector.AtkTarget != null)
        {
            monster.nav.ResetPath();
            monster.ChangeState(ChestState.Atk);
        }
    }
}

public class AtkState : BaseState<Chest>
{

    public override void Enter(Chest monster)
    {
        monster.animator.SetTrigger("Attack");
        Interaction(monster);
    }

    public override void Exit(Chest monster)
    {

    }

    public override void Update(Chest monster)
    {

    }

    private void Interaction(Chest monster)
    {
        Collider[] targets = Physics.OverlapBox(monster.atkPoint.position, monster.attackRang, monster.transform.rotation, monster.layerMask);
        if (targets.Length <= 0)
        {
            monster.interactable = null;
            return;
        }

        for (int i = 0; i < targets.Length; i++)
        {
            IInteractable target = targets[i].GetComponent<IInteractable>();

            if (monster.interactable != target)
            {
                monster.interactable = target;
            }
            monster.interactable?.TakeHit(monster.damage);
            monster.StartCoroutine(AttackCool(monster));
        }
    }

    private IEnumerator AttackCool(Chest monster)
    {
        yield return new WaitForSeconds(2f);
        monster.ChangeState(ChestState.Interaction);
    }
}



public class Chest : Monster
{
    public Transform atkPoint;
    public Vector3 attackRang;

    public IInteractable interactable;
    public ChestState chestState;
    public MonsterType monsterType;
    private StateMachine<ChestState, Chest> stateMachine = new StateMachine<ChestState, Chest>();

    public LayerMask layerMask;

    public Animator animator;
    public NavMeshAgent nav;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        monsterType = MonsterType.Chest;
        stateMachine.Reset(this);
        stateMachine.AddState(ChestState.Idle, new IdleState());
        stateMachine.AddState(ChestState.Interaction, new Interaction());
        stateMachine.AddState(ChestState.Walk, new WalkState());
        stateMachine.AddState(ChestState.Atk, new AtkState());
        ChangeState(ChestState.Idle);
        Hp = 100;
        damage = 10f;
    }

    private void Update()
    {
        detector.FindTarget();
        detector.FindAtkTarget();
        detector.FindSubTarget();
        stateMachine.Update();
    }

    public void ChangeState(ChestState nextState)
    {
        chestState = nextState;
        stateMachine.ChangeState(nextState);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(atkPoint.position, attackRang);
    }
}
