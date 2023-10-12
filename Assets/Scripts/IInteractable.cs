using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void PlayAnimation();
    void TakeHit(float damage);
    void Die();
}
