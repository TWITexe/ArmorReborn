using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float attackCooldown = 2f; // перезарядка атаки
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int attackDamage = 15;
    [SerializeField] private LayerMask playerLayer; // слой игрока
    Vector2 attackDirection;

    private Attack[] comboAttacks;
    private int currentComboIndex = 0;
    private float lastAttackTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        comboAttacks = new Attack[]
        {
            new FirstAttack(animator, attackDamage, gameObject, LayerMask.GetMask("Player")),
        };
    }

    private void Update()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        attackDirection = (transform.localScale.x > 0) ? Vector2.left : Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, attackRange, playerLayer);

        if (hit.collider != null)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }

    private void PerformAttack()
    {
        animator.SetBool("endOfAttack", false);

        if (comboAttacks == null || comboAttacks.Length == 0) return;

        comboAttacks[currentComboIndex].Execute();

        currentComboIndex++;

        if (currentComboIndex >= comboAttacks.Length)
        {
            currentComboIndex = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)attackDirection * attackRange);
    }
}
