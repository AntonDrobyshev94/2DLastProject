using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageDealler : MonoBehaviour
{
    [SerializeField] private float damage;

    /// <summary>
    /// �����, ������� ��������� ��������� � ������� �������� �������� � ������ Damageable � Trigger. ������ ������ ����� �� ������� Bullet, ������� �������� ������� �����������. ��� �������� � �������� � ����� Damageable ������ ������� ��������� ���� (�������� float damage - ������������� � ����������). �� ������� �������� ������ ������ ������ Health, ������� � ������ TakeDamage �������� ���������� �������� damage �� �������� �������� �������� curentHealth. ������ Bullet � ���������� �������� ������������. � ������, ���� ������ �������� ����� ��� Trigger - � �������� ������ �� ���������� � �� �������� ��������� ������ ������� (���������� ��� ����, ����� �������� �� �������� �������� � ������� �� ��������� ��� ������). ���� ������� �������� �� �������� ������, bullet ������ ������������ (��� �� �������� � �����������).
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
