using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class DamageDeallerEnemy : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private EnemyController enemyController;
    public bool isAttack;
    private GameObject collisionGameObject;
    private Health collisionPlayerObject;
    private AudioScript audioScript;

    private void Awake()
    {
        collisionPlayerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); // �������� ��������� ������ Health � ����������
        audioScript = GameObject.FindObjectOfType<AudioScript>(); // ������� ������ � ����������� AudioScript � �������� ���������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collisionGameObject = collision.gameObject;
            if(enemyController.isShooting)
            {
                isAttack = false;
            }
            else
            {
                isAttack = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isAttack = false;
    }

    /// <summary>
    /// ��������� �������� attackTimer.���������� �� ������� AttackSkeleton � ������ EndAttackAnimation �������� AnimationEvent �������� 
    /// "AttackSkelet". � ���������� ������������ ��������, �������, � ������� ��������� �������� � ��������� ������� OnTriggerEnter2D, 
    /// ��������� ����.���� ������� �� ���������� ���� float damage (�������� � ����������). �������� ����� ���������� ������� �������� 
    /// ����� ������ Health(������ ������ ������ �� ������� ��������), ������� � ������ TakeDamage �������� ���������� ���� �� �������� 
    /// ���������� ��������(currentHealth) � ��������� ������ ��� ������ ���� ���������� ��������, ��������� �������� bool ���������� isDead 
    /// - false ��� true �������������.
    /// </summary>
    /// <returns></returns>
    public IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(0);
        if (isAttack)
        {
            if(collisionPlayerObject !=null)
            {
                collisionPlayerObject.TakeDamage(damage);
                audioScript.PlayEnemyAttackSound();
                yield return new WaitForSeconds(1);
                audioScript.StopEnemyAttackSound();
            }
            else
            {
                collisionGameObject.GetComponent<Health>().TakeDamage(damage); //�� ������ ���� �� ��������� � Awake
                audioScript.PlayEnemyAttackSound();
                yield return new WaitForSeconds(1);
                audioScript.StopEnemyAttackSound();
            }
        }
    }
}