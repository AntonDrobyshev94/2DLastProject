using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMessage : MonoBehaviour
{
    private Text deathMessageText;
    private string deathText;

    /// <summary>
    /// �����, ������� ��� ������� ����� ����������� ���������� ���� ������ deathText �������� deathMessageText.text (��������� �������� ������, ����������� � ���������� � ������). �� ������ ������� ��������� ���� �������� ������.
    /// </summary>
    private void Awake()
    {
        deathMessageText = GetComponent<Text>();
        deathText = deathMessageText.text;
        deathMessageText.text = "";
    }

    /// <summary>
    /// ��������� ��������, ������� ���������� �������� Health � ������ CheckIsAlive ��� �������� isDead - true. ������� ����, ������� ������� �������� �������� �� ������ deathText � ��������� ���� deathMessageText.text �� �������, �� 1 ������� ��� � 0.1 �������. ����� 5 ������ ������� ������� �� ��������� ����������.
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
