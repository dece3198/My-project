using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour, IInteractable
{
    [SerializeField]private float hp;
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    private void Awake()
    {
        Hp = 100f;
    }

    public void Die()
    {
    }

    public void PlayAnimation()
    {

    }

    public void TakeHit(float damage)
    {
        hp -= damage;
    }

}
