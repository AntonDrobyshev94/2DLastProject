using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageDealler : MonoBehaviour
{
    [SerializeField] private float damage;

    /// <summary>
    /// Метод, который проверяет вхождение в триггер объектов коллизии с тегами Damageable и Trigger. Данный скрипт висит на объекте Bullet, который обладает триггер коллайдером. При коллизии с объектом с тегом Damageable такому объекту наносится урон (значение float damage - настраивается в инспекторе). На объекте коллизии должен висеть скрипт Health, который в методе TakeDamage отнимает полученное значение damage от текущего значения здоровья curentHealth. Объект Bullet в результате коллизии уничтожается. В случае, если объект коллизии имеет тег Trigger - с объектом ничего не происходит и он свободно пролетает сквозь триггер (необходимо для того, чтобы триггеры не вызывали коллизию и объекты не пропадали без причин). Если объекты коллизии не обладают тегами, bullet просто уничтожается (как бы ударяясь в препятствие).
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        //else if (collision.CompareTag("Trigger"))
        //{
        //}
        else if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
