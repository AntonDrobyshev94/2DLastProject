using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammers : MonoBehaviour
{
    [SerializeField] private GameObject playerDeadPanel;
    private AudioScript audioScript;
    private Health playerObjectHealth;
    [SerializeField] private float damage;
    private bool isDead;

    private void Awake()
    {
        playerObjectHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        audioScript = GameObject.FindAnyObjectByType<AudioScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerObjectHealth.TakeDamage(damage);
            if (playerObjectHealth.currentHealth <= 0)
            {
                StartCoroutine(GameOverCorutine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {


        }
    }

    public IEnumerator GameOverCorutine()
    {
        if (!isDead)
        {
            isDead = true;
            yield return new WaitForSeconds(2);
            audioScript.LooseGame();
            playerDeadPanel.SetActive(true);
        }
    }
}
