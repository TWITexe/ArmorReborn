using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private Animator animator;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} получил урон! Осталось здоровья: {currentHealth}");

        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} погиб!");
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        Destroy(gameObject, 1.5f); // Удаляет объект
    }
}
