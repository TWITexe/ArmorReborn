using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class Attack
{

    protected Animator animator;
    protected GameObject hero;
    protected int attackDamage = 20; // урон атаки
    protected float attackRange = 1.5f; // дальность атаки
    protected LayerMask targetLayer; // слой цели

    public Attack(Animator _animator, int _damage, GameObject _hero, LayerMask _targetLayer)
    {
        animator = _animator;
        attackDamage = _damage;
        hero = _hero;
        targetLayer = _targetLayer;
    }

    public virtual void Execute()
    {
        DealDamage();
    }
    protected void DealDamage()
    {
        if (hero == null) return;

        Vector2 attackPosition = hero.transform.position; // ерем позицию игрока
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(
            attackPosition,
            attackRange,
            targetLayer
        );

        foreach (Collider2D target in hitTargets)
        {
            Debug.Log($"Detected target: {target.gameObject.name}");

            if (target.gameObject == hero ) continue;

            Health enemyHealth = target.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hero.transform.position, attackRange);
    }
}
