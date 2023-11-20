using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScores : MonoBehaviour
{
    [SerializeField] private GameObject coinOneImageObject;
    [SerializeField] private GameObject coinTwoImageObject;
    [SerializeField] private GameObject coinThreeImageObject;
    [SerializeField] private GameObject coinFourImageObject;
    [SerializeField] private GameObject coinFiveImageObject;
    [SerializeField] private Text finishStatisticText;

    [SerializeField] private GameObject finishStatisticInformTextObject;

    [SerializeField] private GameObject finishStatisticTextObject;

    [SerializeField] private GameObject nextLevelButtonObject;

    [SerializeField] private GameObject backToMainMenuButtonObject;

    [SerializeField] private GameObject pointObject;

    [SerializeField] private GameObject pointsCountTextObject;

    [SerializeField] private Text pointsCountText;

    [SerializeField] private GameObject coinsCountTextInformationObject;

    [SerializeField] private GameObject finishPanel;

    [SerializeField] private GameObject playerObject;

    private AudioScript audioScript;
    private PointsCounter pointsCounter;
    private GasMaskScript gasMaskScript;

    public float currentCoin;
    public float currentKilledEnemies;
    public int maxCoinScore;

    private float finalScore;
    private float floatTimer;
    private int timer;

    private void Awake()
    {
        audioScript = GameObject.FindObjectOfType<AudioScript>();
        pointsCounter = GameObject.FindObjectOfType<PointsCounter>();
        gasMaskScript = GameObject.FindObjectOfType<GasMaskScript>();
    }

    private void Update()
    {
        floatTimer += Time.deltaTime;
    }

    /// <summary>
    /// Метод вхождения в триггер финиша, при котором активируется финиш панель, выключается GameObject игрока, включается корутина, выводящая на экран
    /// статистику, подсчитывается финальный счет finalScore в процентах и в зависимости от процента выдается финальный результат от 0 до 5 монет.
    /// Выводится затраченное время на прохождение.
    /// </summary>
    /// <param name="finish"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(gasMaskScript.isGasMaskEnabled) //проверка, одета ли маска на игроке
            {
                gasMaskScript.ButtonEvent();
            }

            finishPanel.SetActive(true);
            finalScore = 100 * currentCoin / maxCoinScore;
            playerObject.SetActive(false);
            audioScript.WinGame();
            StartCoroutine(FinalScoreCorutine());
            timer = Convert.ToInt32(floatTimer);
            finishStatisticText.text = currentKilledEnemies + "\n\n" + timer + "" + " секунд(ы)";
            pointsCountText.text = pointsCounter.currentPoints + "";
        }
    }

    private IEnumerator FinalScoreCorutine()
    {
        yield return new WaitForSeconds(1);
        finishStatisticInformTextObject.SetActive(true);

        yield return new WaitForSeconds(1);
        finishStatisticTextObject.SetActive(true);

        yield return new WaitForSeconds(1);
        pointObject.SetActive(true);

        yield return new WaitForSeconds(1);
        pointsCountTextObject.SetActive(true);

        yield return new WaitForSeconds(1);
        coinsCountTextInformationObject.SetActive(true); 

        yield return new WaitForSeconds(1);
        if (finalScore >= 10f)
        {
            coinOneImageObject.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            StopCoroutine(FinalScoreCorutine());
            nextLevelButtonObject.SetActive(true);
            backToMainMenuButtonObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);
        if (finalScore >= 20f)
        {
            coinTwoImageObject.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            StopCoroutine(FinalScoreCorutine());
            nextLevelButtonObject.SetActive(true);
            backToMainMenuButtonObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);
        if (finalScore >= 50f)
        {
            coinThreeImageObject.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            StopCoroutine(FinalScoreCorutine());
            nextLevelButtonObject.SetActive(true);
            backToMainMenuButtonObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);
        if (finalScore >= 70f)
        {
            coinFourImageObject.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            StopCoroutine(FinalScoreCorutine());
            nextLevelButtonObject.SetActive(true);
            backToMainMenuButtonObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);
        if (finalScore >= 90f)
        {
            coinFiveImageObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            nextLevelButtonObject.SetActive(true);
            backToMainMenuButtonObject.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            StopCoroutine(FinalScoreCorutine());
            nextLevelButtonObject.SetActive(true);
            backToMainMenuButtonObject.SetActive(true);
        }
    }

}
