using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCoin : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int coin = 1; //количество монет в 1 монете.
    [SerializeField] private GameObject particleCoinObject;
    private FinalScores finalScore;
    private CoinCounter coinCounter;
    private AudioScript audioScript;

    /// <summary>
    /// На старте сцены находим объект Finish и CoinCounterText и записываем в переменные GameObject
    /// </summary>
    private void Awake()
    {
        audioScript = GameObject.FindObjectOfType<AudioScript>(); // находим объект класса AudioScript и кешируем экземпляр класса.
        finalScore = GameObject.FindObjectOfType<FinalScores>(); // также кешируем экземпляр класса.
        finalScore.maxCoinScore += 1; // каждая монета на сцене прибавляет +1 к максимальному количеству монет.
        coinCounter = GameObject.FindObjectOfType<CoinCounter>(); // также кешируем экземпляр класса.
    }

    /// <summary>
    /// Проверка коллизии с тегом Player, при которой в скрипт FinalScores значению currentCoin прибавляется значение бонусных монет coin для вывода на финальный экран. 
    /// В скрипт CoinCounter значению coinCount прибавляется значение coin для вывода на экран. При этом бонусный объект исчезает с эфектом particle.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            finalScore.currentCoin += coin;
            coinCounter.coinCount += coin;
            audioScript.BonusCoin();
            Destroy(gameObject);
            particleCoinObject.SetActive(true);
        }
    }
}
