using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bullet; //количество пуль, которые тратятся при выстреле
    [SerializeField] private BulletsChecking bulletsChecking;
    [SerializeField] private Health health;
    public GameObject bulletObject;
    public GameObject[] bulletsArray;
   
    private void Awake()
    {
    }
    /// <summary>
    /// Метод логики стрельбы, при условии наличия стрел isHaveArrows = true и isDead = false создает GameObject currentBullet из GameObject bullet 
    /// из Transform позиции firePoint. Метод принимает значение Scale Transform по координате X материнского объекта 
    /// (AdventurerPlayer), в зависимости от которой задает направление созданной пуле currentBulletVelocity. Если 
    /// playerTransform.localScale > 0 (игрок повернут направо), то значение вектора по координате x положительное (пуля полетит 
    /// направо). При playerTransform.localScale < 0 (игрок повернут налево), значение вектора по координате x отрицательное (пуля 
    /// полетит налево). При выстреле отнимется 1 стрела (float значение arrow передается в скрипт ArrowsCounter).
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
