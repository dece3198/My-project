using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum DoorOnOffState
{
    Open, Close
}

public class DoorOpen : BaseState<Door>
{
    public override void Enter(Door door)
    {
        door.animator.SetTrigger("Open");
        door.SetColor(0.5f);
    }

    public override void Exit(Door door)
    {

    }

    public override void Update(Door door)
    {

    }
}

public class DoorClose : BaseState<Door>
{
    public override void Enter(Door door)
    {
        door.animator.SetTrigger("Close");
        door.SetColor(1);
    }

    public override void Exit(Door door)
    {

    }

    public override void Update(Door door)
    {

    }
}


public class Door : MonoBehaviour
{
    public DoorOnOffState state;
    public Animator animator;

    public Renderer meshRenderer;
    public Renderer parentmeshRenderer;
    public float col = 130f;
    private StateMachine<DoorOnOffState, Door> stateMachine = new StateMachine<DoorOnOffState, Door>();

    private void Awake()
    {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<Renderer>();
        stateMachine.Reset(this);
        stateMachine.AddState(DoorOnOffState.Open, new DoorOpen());
        stateMachine.AddState(DoorOnOffState.Close, new DoorClose());
        ChangeState(DoorOnOffState.Close);
    }

    public void SetColor(float alpha)
    {
        parentmeshRenderer.material.color = new Color(parentmeshRenderer.material.color.r, parentmeshRenderer.material.color.g, parentmeshRenderer.material.color.b, alpha);
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, alpha);
    }

    public void ChangeState(DoorOnOffState nextState)
    {
        state = nextState;
        stateMachine.ChangeState(nextState);
    }
}
