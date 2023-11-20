using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Health health;
    [SerializeField, Range(1,3)] private float speedFire =1.5f;
    private AudioScript audioScript;

    private void Awake()
    {
        audioScript = GameObject.FindObjectOfType<AudioScript>(); // находим объект с компонентом AudioScript и кешируем экземпляр
    }

    /// <summary>
    /// Корутина, которая отрабатывает цикл с условием, пока bool переменные стрельбы isShooting =true и переменная isDead =false (объект не мёртв) 
    /// раз в speedFire секунд создает пулю и обращается к методу вызова звука стрельбы EnemyShootSound() в AudioSctiprt.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShootPlayerPosition()
    {
        while(enemyController.isShooting && !health.isDead)
        {
            CreateBullet();
            audioScript.EnemyShootSound();
            yield return new WaitForSeconds(speedFire);
        }
        yield break;
    }

   /// <summary>
   /// Метод создания пули (Движение пули задается скриптом, который висит на префабе пули).
   /// </summary>
    private void CreateBullet()
    {
        GameObject currentBullet = Instantiate(bullet, firePoint.position, transform.rotation);
    }
}
