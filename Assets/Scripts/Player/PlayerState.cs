using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour, IInteractable
{
    [SerializeField]private float hp;
    public float Hp
    {
        get { return hp; }
        set 
        {
            hp = value; 

            if(hp >= 100)
            {
                hp = 100;
            }
        }
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

    public void HpUp(float _hp)
    {
        hp += _hp;
    }

}
