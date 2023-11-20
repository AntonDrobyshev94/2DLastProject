using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] private DamageDeallerEnemy damageDeallerEnemy;

    /// <summary>
    /// Публичный метод, который вызвается событием AnimationEvent анимации "AttackSkelet". Запускает корутину attackTimer в скрипте damageDeallerSkeleton;
    /// </summary>
    public void EndAttackAnimation()
    {
        StartCoroutine(damageDeallerEnemy.AttackTime());
    }
}
