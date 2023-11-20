using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bullet; //���������� ����, ������� �������� ��� ��������
    [SerializeField] private BulletsChecking bulletsChecking;
    [SerializeField] private Health health;
    public GameObject bulletObject;
    public GameObject[] bulletsArray;
   
    private void Awake()
    {
    }
    /// <summary>
    /// ����� ������ ��������, ��� ������� ������� ����� isHaveArrows = true � isDead = false ������� GameObject currentBullet �� GameObject bullet 
    /// �� Transform ������� firePoint. ����� ��������� �������� Scale Transform �� ���������� X ������������ ������� 
    /// (AdventurerPlayer), � ����������� �� ������� ������ ����������� ��������� ���� currentBulletVelocity. ���� 
    /// playerTransform.localScale > 0 (����� �������� �������), �� �������� ������� �� ���������� x ������������� (���� ������� 
    /// �������). ��� playerTransform.localScale < 0 (����� �������� ������), �������� ������� �� ���������� x ������������� (���� 
    /// ������� ������). ��� �������� ��������� 1 ������ (float �������� arrow ���������� � ������ ArrowsCounter).
    /// </summary>
    /// <param name="direction"></param>
    public void Shoot()
    {
        if(bulletsChecking.isHaveBullets && !health.isDead)
        {
            GameObject currentBullet = Instantiate(bulletObject, firePoint.position, transform.rotation);
            bulletsChecking.BulletsCount(bullet);
        }
    }
}
