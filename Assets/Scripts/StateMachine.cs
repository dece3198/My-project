using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T1,T2> where T2 : MonoBehaviour
{
    private T2 Monster;
    private BaseState<T2> curState;
    private Dictionary<T1, BaseState<T2>> states;

    public void Reset(T2 monster)
    {
        this.Monster = monster;
        curState = null;
        states = new Dictionary<T1, BaseState<T2>>();
    }

    public void Update()
    {
        curState.Update(Monster);
    }

    public void AddState(T1 state, BaseState<T2> baseState)
    {
        states.Add(state, baseState);
    }

    public void ChangeState(T1 state)
    {
        if(curState != null)
        {
            curState.Exit(Monster);
        }
        curState = states[state];
        curState.Enter(Monster);
    }
   


}
