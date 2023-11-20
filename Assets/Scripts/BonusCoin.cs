using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCoin : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int coin = 1; //���������� ����� � 1 ������.
    [SerializeField] private GameObject particleCoinObject;
    private FinalScores finalScore;
    private CoinCounter coinCounter;
    private AudioScript audioScript;

    /// <summary>
    /// �� ������ ����� ������� ������ Finish � CoinCounterText � ���������� � ���������� GameObject
    /// </summary>
    private void Awake()
    {
        audioScript = GameObject.FindObjectOfType<AudioScript>(); // ������� ������ ������ AudioScript � �������� ��������� ������.
        finalScore = GameObject.FindObjectOfType<FinalScores>(); // ����� �������� ��������� ������.
        finalScore.maxCoinScore += 1; // ������ ������ �� ����� ���������� +1 � ������������� ���������� �����.
        coinCounter = GameObject.FindObjectOfType<CoinCounter>(); // ����� �������� ��������� ������.
    }

    /// <summary>
    /// �������� �������� � ����� Player, ��� ������� � ������ FinalScores �������� currentCoin ������������ �������� �������� ����� coin ��� ������ �� ��������� �����. 
    /// � ������ CoinCounter �������� coinCount ������������ �������� coin ��� ������ �� �����. ��� ���� �������� ������ �������� � ������� particle.
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
