using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChestOnOffState
{
    Open, Close
}

public class ChestOpen : BaseState<ChestController>
{
    public override void Enter(ChestController monster)
    {
        monster.animator.SetTrigger("Open");
    }

    public override void Exit(ChestController monster)
    {

    }

    public override void Update(ChestController monster)
    {

    }
}

public class ChestClose : BaseState<ChestController>
{
    public override void Enter(ChestController monster)
    {
        monster.animator.SetTrigger("Close");
    }

    public override void Exit(ChestController monster)
    {

    }

    public override void Update(ChestController monster)
    {

    }
}


public class ChestController : MonoBehaviour
{
    public ChestOnOffState state;
    public Animator animator;

    private StateMachine<ChestOnOffState, ChestController> stateMachine = new StateMachine<ChestOnOffState, ChestController>();

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine.Reset(this);
        stateMachine.AddState(ChestOnOffState.Open, new ChestOpen());
        stateMachine.AddState(ChestOnOffState.Close, new ChestClose());
        ChangeState(ChestOnOffState.Close);
    }

    public void ChangeState(ChestOnOffState nextState)
    {
        state = nextState;
        stateMachine.ChangeState(nextState);
    }
}
