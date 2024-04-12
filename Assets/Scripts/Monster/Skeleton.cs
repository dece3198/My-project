using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum SkeletonState
{
    Idle, Walk, Atk, Die
}

public class SkeletonIdleState : BaseState<Skeleton>
{
    public override void Enter(Skeleton monster)
    {
        monster.nav.ResetPath();
        monster.animator.SetTrigger("Idle");
    }

    public override void Exit(Skeleton monster)
    {

    }

    public override void Update(Skeleton monster)
    {
        if(monster.detector.Target != null)
        {
            monster.ChangeState(SkeletonState.Walk);
        }
    }
}

public class SkeletonWalkState : BaseState<Skeleton>
{
    public override void Enter(Skeleton monster)
    {
        monster.transform.LookAt(monster.detector.SubTarget.transform);
        monster.animator.SetTrigger("Walk");
    }

    public override void Exit(Skeleton monster)
    {
        
    }

    public override void Update(Skeleton monster)
    {
        if(monster.detector.Target != null)
        {
            monster.nav.SetDestination(monster.detector.Target.transform.position);
        }
        else
        {
            monster.ChangeState(SkeletonState.Idle);
        }
        if(monster.detector.AtkTarget != null)
        {
            monster.nav.ResetPath();
            monster.ChangeState(SkeletonState.Atk);
        }
    }
}

public class SkeletonAtkState : BaseState<Skeleton>
{
    public override void Enter(Skeleton monster)
    {
        monster.animator.SetTrigger("Atk");
        monster.StartCoroutine(AttackCo(monster));
    }

    public override void Exit(Skeleton monster)
    {

    }

    public override void Update(Skeleton monster)
    {

    }

    private void Interaction(Skeleton monster)
    {
        Collider[] targets = Physics.OverlapBox(monster.atkPoint.position, monster.attackRang, monster.transform.rotation, monster.layerMask);
        if (targets.Length <= 0)
        {
            monster.interactable = null;
            monster.StartCoroutine(AttackCool(monster));
            return;
        }

        for (int i = 0; i < targets.Length; i++)
        {
            IInteractable target = targets[i].GetComponent<IInteractable>();

            if (monster.interactable != target)
            {
                monster.interactable = target;
            }
            monster.StartCoroutine(AttackCool(monster));
            monster.interactable?.TakeHit(monster.damage);
        }
    }

    private IEnumerator AttackCo(Skeleton skeleton)
    {
        yield return new WaitForSeconds(0.8f);
        Interaction(skeleton);
    }

    private IEnumerator AttackCool(Skeleton monster)
    {
        yield return new WaitForSeconds(2f);
        monster.ChangeState(SkeletonState.Idle);
    }
}

public class Skeleton : Monster
{
    [SerializeField] private Slider slider;
    private float maxHp;
    public Transform atkPoint;
    public Vector3 attackRang;

    public IInteractable interactable;
    public SkeletonState skeletonState;
    public MonsterType monsterType;
    private StateMachine<SkeletonState, Skeleton> skeletonStateMachine = new StateMachine<SkeletonState, Skeleton>();

    public LayerMask layerMask;

    public Animator animator;
    public NavMeshAgent nav;

    [SerializeField] private Camera cam;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        monsterType = MonsterType.Skeleton;
        skeletonStateMachine.Reset(this);
        skeletonStateMachine.AddState(SkeletonState.Idle, new SkeletonIdleState());
        skeletonStateMachine.AddState(SkeletonState.Walk, new SkeletonWalkState());
        skeletonStateMachine.AddState(SkeletonState.Atk, new SkeletonAtkState());
        ChangeState(SkeletonState.Idle);
        Hp = 50;
        maxHp = Hp;
        damage = 20f;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        detector.FindTarget();
        detector.FindAtkTarget();
        detector.FindSubTarget();
        skeletonStateMachine.Update();

        Quaternion q_hp = Quaternion.LookRotation(Canvas.position - cam.transform.position);
        Vector3 hp_angle = Quaternion.RotateTowards(Canvas.rotation, q_hp, 200).eulerAngles;
        Canvas.rotation = Quaternion.Euler(0, hp_angle.y, 0);
        slider.value = Hp / maxHp;
    }

    public void ChangeState(SkeletonState nextState)
    {
        skeletonState = nextState;
        skeletonStateMachine.ChangeState(nextState);
    }

    public override void TakeHit(float damage)
    {
        if(detector.SubTarget != null)
        {
            base.TakeHit(damage);
            ChangeState(SkeletonState.Walk);
            GameObject getText = textList.Pop();
            getText.SetActive(true);
            StartCoroutine(DestroyCo(getText));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(atkPoint.position, attackRang);
    }
}
