using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttackController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    // время сброса комбо
    [SerializeField] private float comboResetTime = 1f;

    // массив атак для комбо
    private Attack[] comboAttacks;
    // текущая атака в комбо
    private int currentComboIndex = 0;
    // корутина для сброса комбо
    private Coroutine comboResetCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           
            PerformAttack();
        }
    }
    private void Start()
    {


        // определяем атаки в комбо (можно менять)
        comboAttacks = new Attack[]
        {
            new FirstAttack(animator, 20, this.gameObject, LayerMask.GetMask("Enemy")),
            new SecondAttack(animator, 30, this.gameObject, LayerMask.GetMask("Enemy"))
        };
    }

    public void PerformAttack()
    {
        animator.SetBool("endOfAttack", false);
        if (comboAttacks == null || comboAttacks.Length == 0) return;

        // выполняем текущую атаку из комбо
        comboAttacks[currentComboIndex].Execute();

        // увеличиваем индекс комбо
        currentComboIndex++;

        // если комбо закончилось, начинаем заново
        if (currentComboIndex >= comboAttacks.Length)
        {
            currentComboIndex = 0;
        }

        // сбрасываем таймер комбо (если уже идет - перезапускаем)
        if (comboResetCoroutine != null)
        {
            StopCoroutine(comboResetCoroutine);
        }
        comboResetCoroutine = StartCoroutine(ResetComboAfterDelay());
    }

    private IEnumerator ResetComboAfterDelay()
    {
       
        // сброс комбо
        yield return new WaitForSeconds(comboResetTime);
        animator.SetBool("endOfAttack", true);
        currentComboIndex = 0; 
    }
}
