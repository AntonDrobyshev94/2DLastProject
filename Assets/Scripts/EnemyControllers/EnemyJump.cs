using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    [SerializeField] private bool isRightEnemyJump; //Задать в инспекторе, является ли триггер правым окончанием платформы или левым (правым =true).

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Damageable")) 
        {
            if(collision.transform.position.x < transform.position.x && isRightEnemyJump)  // Для правого окончания платформы
            {
                collision.GetComponentInChildren<EnemyController>().isGroundColliderExit = true;
            }
            else if (isRightEnemyJump)
            {

            }

            if (collision.transform.position.x > transform.position.x && !isRightEnemyJump)  // Для левого окончания платформы
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
