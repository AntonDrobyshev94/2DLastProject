using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject playerDeadPanel;
    private AudioScript audioScript;
    private Health playerObjectHealth;
    [SerializeField] private float damage;
    public bool isTriggerDamage;
    private bool isDead;
    private bool gasMaskEnabledBoolean;
    public delegate void MyDelegate();
    public static event MyDelegate GasClickButtonEvent;

    private void Awake()
    {
        playerObjectHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        audioScript = GameObject.FindAnyObjectByType<AudioScript>();
    } 

    /// <summary>
    /// Триггер вхождения объекта с тегом Player, который настраивается из инспектора. Если это тригер падения, то необходимо убить игрока (damage > 250) 
    /// и вызвать корутину проигрыша GameOverCorutine(), для этого в инспекторе выставляется isAFall = true. При этом, если gasMaskEnabledBoolean=true (т.е. 
    /// одет противогаз), то запускаем метод-событие GasClickButtonEvent из скрипта gasScript, который выключает противогаз. 
    /// Если isAFall = false, то это триггер газа, isTriggerDamage принимает значение true. Осуществляем проверку на одетый противогаз. Если противогаз
    /// gasMaskEnabledBoolean = true (надет), то ничего не делаем, если false (т.е. не надет), необходимо нанести урон на величину damage, для чего включается
    /// корутина TikDamage. Также запускается метод GasActive, который включает объект для отображения анимации газа.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gasMaskEnabledBoolean)
            {
                GasClickButtonEvent();
                playerObjectHealth.TakeDamage(damage);
                StartCoroutine(GameOverCoroutine());
            }
            else
            {
                isTriggerDamage = true;
                playerObjectHealth.TakeDamage(damage);
                StartCoroutine(GameOverCoroutine());
            }

        }
    }


    public IEnumerator GameOverCoroutine()
    {
        if(!isDead)
        {
            isDead = true;
            yield return new WaitForSeconds(2);
            audioScript.LooseGame();
            playerDeadPanel.SetActive(true);
        }
    }
}
