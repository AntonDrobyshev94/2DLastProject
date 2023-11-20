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
        collisionPlayerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); // кешируем экземпляр класса Health в переменную
        audioScript = GameObject.FindObjectOfType<AudioScript>(); // находим объект с компонентом AudioScript и кешируем экземпляр
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
    /// Публичная корутина attackTimer.Вызывается из скрипта AttackSkeleton в методе EndAttackAnimation событием AnimationEvent анимации 
    /// "AttackSkelet". В результате срабатывания корутины, объекту, с которым произошла коллизия в публичном событии OnTriggerEnter2D, 
    /// наносится урон.Урон зависит от переменной типа float damage (задается в инспекторе). Значение урона передается объекту коллизии 
    /// через скрипт Health(скрипт должен висеть на объекте коллизии), который в методе TakeDamage отнимает нанесенный урон от текущего 
    /// количества здоровья(currentHealth) и проверяет больше или меньше нуля полученное значение, возвращая значение bool переменной isDead 
    /// - false или true соотвественно.
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
                collisionGameObject.GetComponent<Health>().TakeDamage(damage); //на случай если не сработает в Awake
                audioScript.PlayEnemyAttackSound();
                yield return new WaitForSeconds(1);
                audioScript.StopEnemyAttackSound();
            }
        }
    }
}