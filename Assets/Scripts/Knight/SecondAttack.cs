using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondAttack : Attack
{
    public SecondAttack(Animator animator, int gamage, GameObject _hero, LayerMask targetLayer) : base(animator, 20, _hero, targetLayer) { }

    public override void Execute()
    {
        base.Execute();
        animator.SetTrigger("SecondAttack");
        Debug.Log($"Сильный удар! Нанесено {attackDamage} урона.");
    }
}
