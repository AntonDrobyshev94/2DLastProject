using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMessage : MonoBehaviour
{
    private Text deathMessageText;
    private string deathText;

    /// <summary>
    /// Метод, который при запуске сцены присваивает переменной типа строка deathText значение deathMessageText.text (переводит значение текста, написанного в инспекторе в строку). На момент запуска текстовое поле остается пустым.
    /// </summary>
    private void Awake()
    {
        deathMessageText = GetComponent<Text>();
        deathText = deathMessageText.text;
        deathMessageText.text = "";
    }

    /// <summary>
    /// Публичная корутина, которая вызывается скриптом Health в методе CheckIsAlive при значении isDead - true. Создает цикл, который выводит значения символов из строки deathText в текстовое поле deathMessageText.text на канвасе, по 1 символу раз в 0.1 секунды. Через 5 секунд стирает символы из текстовой переменной.
    /// </summary>
    /// <returns></returns>
    public IEnumerator DeathTextCoroutine()
    {
        foreach (char a in deathText)
        {
            deathMessageText.text += a;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(5);
        deathMessageText.text = "";
    }
}
