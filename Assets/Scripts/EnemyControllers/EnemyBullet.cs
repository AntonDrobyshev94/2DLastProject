using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject playerCurrentObject;
    [SerializeField] private float speedBullet = 2.5f;
    private Rigidbody2D rgb;
    private Vector2 directionToPlayer;
    private PlayerMovement target;

    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerMovement>();
        directionToPlayer = (target.transform.position - transform.position).normalized * speedBullet;
        rgb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
