using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Chest
}

public abstract class BaseState<T>
{
    public abstract void Enter(T monster);
    public abstract void Update(T monster);
    public abstract void Exit(T monster);
}

public class Monster : MonoBehaviour,IInteractable
{
    public float damage;
    [SerializeField] private float hp;
    public ViewDetector detector;
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }


    public void Die()
    {

    }

    public void PlayAnimation()
    {

    }

    public void TakeHit(float damage)
    {
        Hp -= damage;
        Debug.Log(Hp);
    }
}
