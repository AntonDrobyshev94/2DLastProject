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
        audioScript = GameObject.FindObjectOfType<AudioScript>(); // ������� ������ � ����������� AudioScript � �������� ���������
    }

    /// <summary>
    /// ��������, ������� ������������ ���� � ��������, ���� bool ���������� �������� isShooting =true � ���������� isDead =false (������ �� ����) 
    /// ��� � speedFire ������ ������� ���� � ���������� � ������ ������ ����� �������� EnemyShootSound() � AudioSctiprt.
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
   /// ����� �������� ���� (�������� ���� �������� ��������, ������� ����� �� ������� ����).
   /// </summary>
    private void CreateBullet()
    {
        GameObject currentBullet = Instantiate(bullet, firePoint.position, transform.rotation);
    }
}
