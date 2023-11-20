using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerAnimator : MonoBehaviour
{
    public Animator playerAnim;
    [SerializeField] private Health health;
    private PlayerMovement playerMovement;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// В каждый фиксированный апдейт проверяет значения bool переменных isRunning, isDead, isHit. При истинных значениях запускает анимации из Animator игрока.
    /// </summary>
    private void FixedUpdate()
    {
        playerAnim.SetBool("isRunning", playerInput.isRunning);
        playerAnim.SetBool("isDead", health.isDead);
        playerAnim.SetBool("isJump", playerMovement.isJump);
        playerAnim.SetBool("isSecondJump", playerMovement.isSecondJump);
    }

    public void IsHurtTrigger()
    {
        playerAnim.SetTrigger("isHurtTrigger");
    }
}
