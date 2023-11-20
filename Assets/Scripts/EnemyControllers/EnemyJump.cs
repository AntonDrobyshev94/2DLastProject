using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    [SerializeField] private bool isRightEnemyJump; //������ � ����������, �������� �� ������� ������ ���������� ��������� ��� ����� (������ =true).

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Damageable")) 
        {
            if(collision.transform.position.x < transform.position.x && isRightEnemyJump)  // ��� ������� ��������� ���������
            {
                collision.GetComponentInChildren<EnemyController>().isGroundColliderExit = true;
            }
            else if (isRightEnemyJump)
            {

            }

            if (collision.transform.position.x > transform.position.x && !isRightEnemyJump)  // ��� ������ ��������� ���������
            {
                collision.GetComponentInChildren<EnemyController>().isGroundColliderExit = true;
            }  
            else if (!isRightEnemyJump)
            {

            }
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            collision.GetComponentInChildren<EnemyController>().isGroundColliderExit = false;
        }
    }
}
