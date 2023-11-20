using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator enemyAnim;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private DamageDeallerEnemy damageDeallerEnemy;
    private Health health;

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    /// <summary>
    /// В каждый фиксированный апдейт проверяет значения bool переменных isAttack, isDead, isHit. При истинных значениях запускает анимации из Animator скелета. 
    /// </summary>
    private void FixedUpdate()
    {
        enemyAnim.SetBool("isAttack", damageDeallerEnemy.isAttack);
        enemyAnim.SetBool("isJump", !enemyController.isGrounded);
        enemyAnim.SetFloat("velocity", enemyController.rgb.velocity.magnitude);
        enemyAnim.SetBool("isDead", health.isDead);
    }

    public void IsHurtTrigger()
    {
        enemyAnim.SetTrigger("isHurtTrigger");
    }
}
