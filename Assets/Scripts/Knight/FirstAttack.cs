using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAttack : Attack
{
    public FirstAttack(Animator animator, int gamage,GameObject _hero, LayerMask targetLayer) : base(animator, 15, _hero, targetLayer) { }

    public override void Execute()
    {
        base.Execute();
        animator.SetTrigger("FirstAttack");
        Debug.Log($"Сильный удар! Нанесено {attackDamage} урона.");
    }
}
