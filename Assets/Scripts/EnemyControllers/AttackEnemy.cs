using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] private DamageDeallerEnemy damageDeallerEnemy;

    /// <summary>
    /// ��������� �����, ������� ��������� �������� AnimationEvent �������� "AttackSkelet". ��������� �������� attackTimer � ������� damageDeallerSkeleton;
    /// </summary>
    public void EndAttackAnimation()
    {
        StartCoroutine(damageDeallerEnemy.AttackTime());
    }
}
