using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHeal : MonoBehaviour
{
    [SerializeField] private GameObject particleHealObject;
    [SerializeField, Range(50, 200)] private float heal;
    private Health health;
    private HealthBar healthBar;
    private AudioScript audioScript;

    /// <summary>
    /// При старте сцены находим объект BodyCollider для записи в переменную GameObject
    /// </summary>
    private void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); 
        healthBar = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
        audioScript = GameObject.FindObjectOfType<AudioScript>();
    }

    /// <summary>
    /// Проверка коллизии с тегом Player, при которой объекту со скриптом Health в переменную currentHealth прибавляется здоровье.
    /// Конечное здоровье не должно превышать 100 единиц.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health.currentHealth += heal;
            healthBar.HealthBarUpdate(health.maxHealth, health.currentHealth);
            audioScript.BonusHeal();
            Destroy(gameObject);
            particleHealObject.SetActive(true);
            if (health.currentHealth > 100f)
                health.currentHealth = 100f;
        }
    }
}
