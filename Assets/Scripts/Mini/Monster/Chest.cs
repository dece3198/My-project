using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum ChestState
{
    Idle, Interaction, Walk, Atk
}



public class ChestIdleState : BaseState<Chest>
{
    public override void Enter(Chest monster)
    {
        monster.Canvas.gameObject.SetActive(false);
        monster.animator.SetTrigger("Idle");
    }

    public override void Exit(Chest monster)
    {

    }

    public override void Update(Chest monster)
    {
    }
}

public class ChestInteraction : BaseState<Chest>
{
    public override void Enter(Chest monster)
    {
        monster.Canvas.gameObject.SetActive(true);
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


public class ChestWalkState : BaseState<Chest>
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

public class ChestAtkState : BaseState<Chest>
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
    [SerializeField] private Slider slider;
    private float maxHp;
    public Transform atkPoint;
    public Vector3 attackRang;

    public IInteractable interactable;
    public ChestState chestState;
    public MonsterType monsterType;
    private StateMachine<ChestState, Chest> stateMachine = new StateMachine<ChestState, Chest>();

    public LayerMask layerMask;

    public Animator animator;
    public NavMeshAgent nav;

    [SerializeField] private Camera cam;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        monsterType = MonsterType.Chest;
        stateMachine.Reset(this);
        stateMachine.AddState(ChestState.Idle, new ChestIdleState());
        stateMachine.AddState(ChestState.Interaction, new ChestInteraction());
        stateMachine.AddState(ChestState.Walk, new ChestWalkState());
        stateMachine.AddState(ChestState.Atk, new ChestAtkState());
        ChangeState(ChestState.Idle);
        DamageText();
        Hp = 50;
        maxHp = Hp;
        damage = 10f;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        detector.FindTarget();
        detector.FindAtkTarget();
        detector.FindSubTarget();
        stateMachine.Update();

        Quaternion q_hp = Quaternion.LookRotation(Canvas.position - cam.transform.position);
        Vector3 hp_angle = Quaternion.RotateTowards(Canvas.rotation, q_hp, 200).eulerAngles;
        Canvas.rotation = Quaternion.Euler(0, hp_angle.y, 0);
        slider.value = Hp / maxHp;
    }

    public void ChangeState(ChestState nextState)
    {
        chestState = nextState;
        stateMachine.ChangeState(nextState);
    }

    public override void Die()
    {
        gameObject.SetActive(false);
        ItemStorage.instance.ItemDropA(gameObject);
    }
    public override void TakeHit(float damage)
    {
        if(chestState != ChestState.Idle)
        {
            base.TakeHit(damage);
            GameObject getText = textList.Pop();
            if (chestState != ChestState.Atk)
            {
                ChangeState(ChestState.Walk);
            }
            getText.SetActive(true);
            if(hp > 0)
            {
                StartCoroutine(DestroyCo(getText));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(atkPoint.position, attackRang);
    }
}
